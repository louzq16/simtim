using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace simTim
{
    public partial class Form2 : Form
    {
        BLL.user2user user2user = new BLL.user2user();
        BLL.User2Server u2s = new BLL.User2Server();

        int port = 23455;

        Thread lssth = null;
        private SynchronizationContext mainth;

        Dictionary<string, int> friends = new Dictionary<string, int> { };

        //Dictionary<int, Thread> clientrecvths = new Dictionary<int, Thread> { };
        //Dictionary<int, Socket> clientitems = new Dictionary<int, Socket> { };

        
            
        public Form2()
        {
            InitializeComponent();
            //u2s.userconnect();
            this.DialogResult = DialogResult.None;
            this.textBox_IP.Text = "此处将显示好友IP";
            this.label_welcome.Text = this.label_welcome.Text + common.user.username;
            //this.dataGridView_one.Rows[0].Frozen = true;
            this.port = getport(common.user.username);

            listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listensocket.Bind(new IPEndPoint(IPAddress.Any, port));
            //this.startListen();
            mainth = SynchronizationContext.Current;//记录主线程
            lssth = new Thread(startListen);
            lssth.IsBackground = true;
            lssth.Start();
        }

        private int getport(string name)
        {
            int pt = 0;
            int.TryParse(name, out pt);
            pt = pt % 1000;
            pt = pt + 20000;
            return pt;
        }

        private bool ValidateIP(string str)
        {
            Regex validipregex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            return (str != "" && validipregex.IsMatch(str.Trim())) ? true : false;
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.stoplisten();
            shutcon();
            //this.listensocket.Shutdown(SocketShutdown.Both);
            //this.listensocket.Close();
            this.Close();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            shutcon();
            this.Close();
        }

        private void button_check_Click(object sender, EventArgs e)
        {

            string msg =this.textBox_friendid.Text.Trim().ToString();
            string restr = u2s.userlog("q"+msg,1);
            bool isindict = !friends.ContainsKey(msg);
            int index=0;
            //MessageBox.Show(restr);
            if (restr == "n")
            {
                this.textBox_IP.Text = "离线";
                MessageBox.Show("您的好友离线");
                index = isindict ? this.dataGridView_one.Rows.Add() : friends[msg];
                this.dataGridView_one.Rows[index].Cells[0].Value = msg;
                this.dataGridView_one.Rows[index].Cells[1].Value = "离线";
                this.dataGridView_one.Rows[index].Cells[2].Value = "None";
                if (isindict) friends.Add(msg, index);
            }
            else
            if(ValidateIP(restr))
            {
                this.textBox_IP.Text = restr;
                index = isindict ? this.dataGridView_one.Rows.Add() : friends[msg];
                this.dataGridView_one.Rows[index].Cells[0].Value = msg;
                this.dataGridView_one.Rows[index].Cells[1].Value = "在线";
                this.dataGridView_one.Rows[index].Cells[2].Value = restr;
                if (isindict) friends.Add(msg, index);

            }
            else
            {
                MessageBox.Show("查询输入有误");
            }
            this.dataGridView_one.ClearSelection();
        }
        #region p2p server

       
        int maxclient = 9;
        int num = 0;
        //IPAddress localserverIP = null;
        Socket listensocket = null;
        byte[] rcfmsgbuffer = new byte[50*1024];
        string recevieID = null;
        bool islisten = true;

        private void onconnect(object state)
        {
            Socket newclient = (Socket)state;

            int length = newclient.Receive(rcfmsgbuffer);
            string rcfmsg = Encoding.UTF8.GetString(rcfmsgbuffer, 0, length);
            string label = rcfmsg.Substring(0, 4);
            switch (label)
            {
                case "*//*":
                    string temchaername = rcfmsg.Substring(4);
                    common.charts[temchaername].startfiletrans(newclient);
                    break;
                case "-**-":
                    newclient.Close();
                    this.listensocket.Close();
                    break;
                default:
                    int index = rcfmsg.IndexOf("*++");
                    recevieID = rcfmsg.Substring(0, index);
                    Console.WriteLine(recevieID);
                    Console.WriteLine(rcfmsg);
                    rcfmsg = rcfmsg.Substring(index + 3);
                    if (common.charts.ContainsKey(recevieID))
                    {
                        common.charts[recevieID].changesockets(newclient, rcfmsg);
                    }
                    else
                    {
                        chart temchart = new chart(recevieID, newclient, rcfmsg);
                        temchart.Show();
                        common.charts.Add(recevieID, temchart);
                    }
                    break;
            }
        }
        public void startListen()
        {
            listensocket.Listen(maxclient);
            //listensocket.BeginAccept(new AsyncCallback(Accept), listensocket);
            while(islisten)
            {
                Socket newclient = listensocket.Accept();
                //IPEndPoint temclip = (IPEndPoint)newclient.RemoteEndPoint;
                mainth.Send(new SendOrPostCallback(onconnect), (object)newclient);
                //int pt = ((IPEndPoint)newclient.RemoteEndPoint).Port;
                //chart temchart = new chart(num.ToString(), temclip, newclient, pt);
                //temchart.Show();
            }
        }
        private  void stoplisten()
        {
            islisten = false;
            Socket temp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            temp.Connect(IPAddress.Parse("127.0.0.1"), this.port);
            temp.Send(Encoding.UTF8.GetBytes("-**-"));
            temp.Shutdown(SocketShutdown.Both);
            temp.Close();
        }

        //private void Accept(IAsyncResult lsocket)
        //{
        //    Socket lss = (Socket)lsocket;
        //    Socket newclient = lss.EndAccept(lsocket);
        //    IPAddress temclip = ((IPEndPoint)lss.RemoteEndPoint).Address;
        //    int pt = ((IPEndPoint)lss.RemoteEndPoint).Port;
        //    chart temchart = new chart(num.ToString(), temclip, newclient,pt);
        //    temchart.Show();
        //    common.charts.Add(num.ToString(), temchart);
        //    num++;
        //    //clientitems.Add(num, newclient);
        //    //ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
        //    //Thread clth = new Thread(pts);
        //    //clth.Start(newclient);
            
        //    lss.BeginAccept(new AsyncCallback(Accept), listensocket);
        //}

        #endregion

        private void button_chart_Click(object sender, EventArgs e)
        {
            int index = dataGridView_one.CurrentRow.Index;
            string IP = (string)dataGridView_one.Rows[index].Cells[2].Value;
            string state= (string)dataGridView_one.Rows[index].Cells[1].Value;
            string ID = (string)dataGridView_one.Rows[index].Cells[0].Value;
            if (state=="在线")
            {
                if(common.charts.ContainsKey(ID))
                {
                    common.charts[ID].Show();
                }
                else
                {
                    Console.WriteLine(getport(ID));
                    Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    chart temchart = new chart(ID, IPAddress.Parse(IP), newclient, getport(ID));
                    temchart.Show();
                    temchart.Text = ID;
                    common.charts.Add(ID, temchart);
                    num++;
                }
            }          
        }
        private void Form2_FormClosing(object sender, EventArgs e)
        {
            shutcon();
        }

        public void shutcon()
        {
            chart temchart = null;
            foreach(var value in common.charts)
            {
                temchart = value.Value;
                temchart.shutcon(false);
                temchart.Dispose();
            }
            common.charts.Clear();
        }

}
}
