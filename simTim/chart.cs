using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;



namespace simTim
{
    public partial class chart : Form
    {
        Socket clrecvsk = null;
        public  string chartname = null;
        IPAddress friendsIP = null;
        int port = 23456;
        int rcfsize=512*1024;
        private SynchronizationContext chartth;
        string time;
        bool issubclose = false;
        bool isonline = false;
        int richboxrows = 0;

        public chart(string cn,Socket clskt,string premsg)
        {
            InitializeComponent();
            this.clrecvsk = clskt;
            this.chartname = cn;
            this.Text = cn;
            //this.port = pt;
            startrecv();
            friendsIP = ((IPEndPoint)clrecvsk.RemoteEndPoint).Address;
            port = getport(cn);
            this.rcv_add_rtb(premsg.Substring(17), premsg.Substring(0, 16));
            this.label_hint.Text = "对方在线";
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        public chart(string cn, IPAddress IP,Socket clskt,int pt)
        {
            InitializeComponent();
            this.clrecvsk = clskt;
            this.chartname = cn;
            this.friendsIP = IP;
            this.port = pt;
            this.label_hint.Text = "对方离线";
            Control.CheckForIllegalCrossThreadCalls = false;//可以跨线程使用控件
        }

        public void changesockets(Socket nclskt,string premsg)
        {//若在本地已经建立的情况下 需要更新socket IP等
            this.clrecvsk = nclskt;
            startrecv();
            this.rcv_add_rtb(premsg.Substring(17), premsg.Substring(0, 16));
        }

        private void startrecv()
        {
            chartth = SynchronizationContext.Current;
            Thread clth = new Thread(recv);
            clth.IsBackground = true;
            clth.Start();
        }

        #region 
        private void onrecv(object state)
        {
            string rcfmsg = (string)state;
            int length = rcfmsg.Length;
            string label = rcfmsg.Substring(0, 3);
            rcfmsg = rcfmsg.Substring(3);
            switch (label)
            {
                //case "*+*":
                //    //表示自己的姓名
                //    int index = rcfmsg.IndexOf("*++");
                //    this.chartname = rcfmsg.Substring(0,index);
                //    this.Text = this.chartname;
                //    rcfmsg = rcfmsg.Substring(index + 3);
                //    goto case "*++";
                case "*++":
                    this.rcv_add_rtb(rcfmsg.Substring(17), rcfmsg.Substring(0, 16));
                    break;
                case "*//":
                    if(rcfmsg.Substring(0,4)=="want")
                    {
                        send("*//recv");
                    }
                    else
                    {
                        fileconnection();
                        Thread sendth = new Thread(sendfiletrans);
                        sendth.IsBackground = true;
                        sendth.Start();
                    }
                    break;
                case "*--":
                    this.label_hint.Text = "对方已离线";
                    if(!issubclose)
                    {
                        send("*--*");
                        this.clrecvsk.Shutdown(SocketShutdown.Both);
                    }
                    this.clrecvsk.Close();
                    this.clrecvsk= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    break;

            }

        }

        private void recv()
        {
            bool isrecv=true;
            while(isrecv)
            {
                byte[] rcfmsgbuffer = new byte[rcfsize];
                string rcfmsg;
                try
                {
                    int length = clrecvsk.Receive(rcfmsgbuffer);
                    rcfmsg = Encoding.UTF8.GetString(rcfmsgbuffer, 0, length);
                    if (rcfmsg == "*--*")
                        isrecv = false;
                    chartth.Send(new SendOrPostCallback(onrecv), (object)rcfmsg);

                    //length = rcfmsg.Length;
                    //if(length>=2 && rcfmsg.Substring(0,1)=="*+*")
                    //{
                    //    lock(chartname)
                    //    {
                    //        chartname = rcfmsg.Substring(2,length - 2);
                    //    }           
                    //}
                    //else
                    //{
                    //    lock (this.richTextBox_chart)
                    //    {
                    //        this.richTextBox_chart.Text = rcfmsg;
                    //    }
                    //}
                    
                   
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    clrecvsk.Close();
                }
            }
        }

        public void send(string s)
        {
            byte[] smsg = Encoding.UTF8.GetBytes(s);
            try
            {
                clrecvsk.Send(smsg);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        #endregion
   

        private void button_send_Click(object sender, EventArgs e)
        {
            time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string sendmsg = this.textBox_input.Text.Trim().ToString();
            this.textBox_input.Text = "";
            if (!clrecvsk.Connected)
            {
                clrecvsk.Connect(friendsIP, port);
                this.label_hint.Text = "对方离线";
                startrecv();
                send(common.user.username+"*++"+time+"\n"+sendmsg);//发送自己的用户名  
            }
            else
            {
                send("*++" +time +"\n" + sendmsg);
            }
            send_add_rtb(sendmsg, time);
           
         }

        private void send_add_rtb(string sendmsg, string time)
        {
            string username = common.user.username;
            this.richTextBox_chart.SelectionAlignment = HorizontalAlignment.Right;
            this.richTextBox_chart.SelectionBackColor = Color.Yellow;
            this.richTextBox_chart.AppendText(common.user.username);
           // this.richTextBox_chart.SelectionBackColor = Color.Black;
            this.richTextBox_chart.AppendText(Environment.NewLine);
            this.richTextBox_chart.AppendText(sendmsg);

            this.richTextBox_chart.AppendText(Environment.NewLine);
            this.richTextBox_chart.SelectionStart = this.richTextBox_chart.Text.Length;
            this.richTextBox_chart.ScrollToCaret();
            //richboxrows += 2;
            //this.richTextBox_chart.Select(richboxrows - 2, common.user.username.Length);

        }
        private void rcv_add_rtb(string rcvmsg, string time)
        {
            string username = this.chartname;
            this.richTextBox_chart.SelectionAlignment = HorizontalAlignment.Left;
            this.richTextBox_chart.SelectionBackColor= Color.LightBlue;
            this.richTextBox_chart.AppendText(this.chartname);
           // this.richTextBox_chart.SelectionColor = Color.White;
            this.richTextBox_chart.AppendText(Environment.NewLine);
            this.richTextBox_chart.AppendText(rcvmsg);
           
            this.richTextBox_chart.AppendText(Environment.NewLine);
            this.richTextBox_chart.SelectionStart = this.richTextBox_chart.Text.Length;
            this.richTextBox_chart.ScrollToCaret();
            //richboxrows += 2;
            //this.richTextBox_chart.Select(richboxrows - 2, common.user.username.Length);

        }

        private void button_leavechart_Click(object sender, EventArgs e)
        {
            shutcon(true);
            this.Dispose();
        }

        private void chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            shutcon(true);
            this.Dispose();
        }

        public void shutcon(bool isremove)
        {
            if(this.clrecvsk.Connected)
            {
                send("*--*");
                issubclose = true;
            }         
            //this.clrecvsk.Shutdown(SocketShutdown.Both);
            //this.clrecvsk.Close();
            if(isremove)
                common.charts.Remove(this.chartname);
        }

        private int getport(string name)
        {
            int pt = 0;
            int.TryParse(name, out pt);
            pt = pt % 1000;
            pt = pt + 20000;
            return pt;
        }

        #region 发送文件
        OpenFileDialog ofd = new OpenFileDialog();
        string filepath = null;
        string filename = null;
        int filebuffer_size = 1024 * 300;
        Socket fileskt = null;

        private void fileconnection()
        {
            fileskt= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                fileskt.Connect(friendsIP, this.port);
                send(fileskt,"*//*" + common.user.username);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void startfiletrans(Socket flskt)
        {
            this.fileskt = flskt;
            Thread recvth = new Thread(recvfiletrans);
            recvth.IsBackground = true;
            recvth.Start();

        }
        private void recvfiletrans()
        {
            lock(this.progressBar_recvfile)
            {
                byte[] controlrecv = new byte[100];
                int length = 0;
                string rcfmsg = null;
                try
                {
                    length = this.fileskt.Receive(controlrecv);
                    rcfmsg = Encoding.UTF8.GetString(controlrecv, 0, length);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                if (rcfmsg.Substring(0, 4) == "want")
                {
                    int sizeeach = 128;
                    string[] tem = rcfmsg.Split('?');
                    // byte[] recv = new byte[long.Parse(tem[2])];
                    FileStream fs = new FileStream(tem[1], FileMode.Create);
                    this.fileskt.Send(Encoding.UTF8.GetBytes("OK"));
                    byte[] temrecv = new byte[300 * 1024];
                    long number = long.Parse(tem[2]) / (sizeeach * 1024);
                    Console.WriteLine(number);
                    this.progressBar_recvfile.Maximum = (int)number + 1;
                    for (long i = 0; i < number; i++)
                    {
                        re: length = this.fileskt.Receive(temrecv);
                        if(length== sizeeach * 1024)//包长判断
                        {
                            Console.WriteLine(length + " " + Convert.ToString(i));
                            fs.Write(temrecv, 0, sizeeach * 1024);
                            this.fileskt.Send(Encoding.ASCII.GetBytes("OK" + Convert.ToString(i)));
                            this.progressBar_recvfile.Value++;
                        }
                        else
                        {
                            this.fileskt.Send(Encoding.ASCII.GetBytes("NO" + Convert.ToString(i)));
                            goto re;
                        }
                       
                    }
                    number = long.Parse(tem[2]) % (sizeeach * 1024);
                    Array.Clear(temrecv, 0, temrecv.Length);
                    int recvnumber;
                    switch (number)
                    {
                        case 1:
                            while (true)
                            {
                                recvnumber = this.fileskt.Receive(temrecv);
                                if (recvnumber > 1)
                                {
                                    fs.Close();
                                    this.fileskt.Close();
                                    Console.WriteLine("recv ok");
                                    break;
                                }
                                else
                                {
                                    fs.Write(temrecv, 0, 1);
                                    this.fileskt.Send(Encoding.UTF8.GetBytes("OK"));
                                    Console.WriteLine(recvnumber );
                                }
                            }
                            break;
                        default:
                            while (true)
                            {
                                recvnumber = this.fileskt.Receive(temrecv);
                                if (recvnumber == 1)
                                {
                                    fs.Close();
                                    this.fileskt.Close();
                                    Console.WriteLine("recv ok");
                                    break;
                                }
                                else
                                {
                                    fs.Write(temrecv, 0, (int)number);
                                    this.fileskt.Send(Encoding.UTF8.GetBytes("OK"));
                                    Console.WriteLine(recvnumber);
                                }
                            }
                            break;
                    }

                }
                else
                {
                    this.fileskt.Send(Encoding.UTF8.GetBytes("NO"));
                    this.fileskt.Shutdown(SocketShutdown.Both);
                }
                this.progressBar_recvfile.Value = 0;
            }
            
        }
        private  void sendfiletrans()
        {
            int sizeeach = 128;
            lock(this.progressBar_file)
            {
                System.IO.FileInfo fileinfo = new System.IO.FileInfo(filepath);
                long filesize = fileinfo.Length;
                byte[] controlrecv = new byte[100];
                this.progressBar_file.Maximum = (int)(filesize / (sizeeach * 1024))+1;
                lock (this.progressBar_file)
                {

                }
                string rcvmsg = null;
                int length;
                this.fileskt.Send(Encoding.UTF8.GetBytes("want?" + this.filename + "?" + filesize));
                length = this.fileskt.Receive(controlrecv);
                rcvmsg = Encoding.UTF8.GetString(controlrecv, 0, length);
                if (rcvmsg == "OK")
                {
                    FileStream fread = new FileStream(filepath, FileMode.Open);
                    long number = filesize / (sizeeach * 1024);
                    Console.WriteLine(number);
                    long offset = 0;
                    byte[] readbyte = new byte[sizeeach * 1024];
                    for (long i = 0; i < number; i++)
                    {
                        fread.Read(readbyte, 0, sizeeach * 1024);

                        resend: this.fileskt.Send(readbyte);
                        this.fileskt.Receive(controlrecv);
                        if(Encoding.ASCII.GetString(controlrecv, 0, 2)=="OK")//ACK判断
                        {
                            Console.WriteLine(Encoding.ASCII.GetString(controlrecv, 0, 10));
                            this.progressBar_file.Value++;
                        }
                       else
                        {
                            Console.WriteLine(Encoding.ASCII.GetString(controlrecv, 0, 10));
                            goto resend;
                        }
                    }
                    number = filesize % (sizeeach * 1024);
                    switch (number)
                    {
                        case 0:
                            this.fileskt.Send(Encoding.ASCII.GetBytes("@"));
                            this.fileskt.Shutdown(SocketShutdown.Both);
                            this.fileskt.Close();
                            fread.Close();
                            break;
                        case 1:
                            byte[] a = new byte[1];
                            fread.Read(a, 0, 1);
                            this.fileskt.Send(a);
                            this.fileskt.Receive(controlrecv);
                            this.fileskt.Send(Encoding.ASCII.GetBytes("@@"));
                            this.fileskt.Shutdown(SocketShutdown.Both);
                            this.fileskt.Close();
                            fread.Close();
                            break;
                        default:
                            byte[] b = new byte[number];
                            fread.Read(b, 0, (int)number);
                            this.fileskt.Send(b);
                            this.fileskt.Receive(controlrecv);
                            goto case 0;
                    }
                }
                else
                {
                }
                this.progressBar_file.Value = 0;
            }
            
           
        }

        private void send(Socket s,string ss)
        {
            byte[] smsg = Encoding.UTF8.GetBytes(ss);
            try
            {
                s.Send(smsg);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button_sendfile_Click(object sender, EventArgs e)
        {
            if(this.clrecvsk.Connected)
            {
                //ofd = new OpenFileDialog();
                ofd.InitialDirectory = Application.StartupPath;
                if(ofd.ShowDialog()==DialogResult.OK)
                {
                    filepath = ofd.FileName;
                    filename = System.IO.Path.GetFileName(filepath);
                    send("*//want" + filename);
                    //Console.WriteLine(filepath);
                }
            }
            else
            {
                MessageBox.Show("对方离线，无法传输文件");
            }
        }

        #endregion
    }
}
