using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace BLL
{
    public class User2Server
    {
        //服务器ip地址
        private readonly IPAddress ServerIP = IPAddress.Parse("166.111.140.14");
        private readonly int port = 8000;
       
        Socket user2server;

        //接收缓冲区大小
        private int buffer_size = 40;
  
        public void userconnect()
        {
            IPEndPoint ipEp = new IPEndPoint(ServerIP, port);
            //初始化TCP连接
            user2server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                user2server.Connect(ipEp);
            }
            catch (Exception e)
            {
                MessageBox.Show("连接错误:" + e.Message);
            }
        }
        public string userlog(string message,int yes)
        {
            byte[] sendbuffer = Encoding.UTF8.GetBytes(message);
            byte[] recievebuffer = new byte[buffer_size];
            if (yes==1)
            {
               
                IPEndPoint ipEp = new IPEndPoint(ServerIP, port);
                //初始化TCP连接
                user2server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    user2server.Connect(ipEp);
                }
                catch (Exception e )
                {
                    MessageBox.Show("连接失败，尝试重连:\n" + e.Message);
                    return "";

                }
                try
                {
                    user2server.Send(sendbuffer);
                    user2server.Receive(recievebuffer);
                    //user2serverstream.Write(sendbuffer, 0, sendbuffer.Length);
                    //user2serverstream.Read(recievebuffer, 0, buffer_size);
                }
                catch (Exception e)
                {
                    MessageBox.Show("传输流错误:" + e.Message);
                }

 
                user2server.Shutdown(SocketShutdown.Both);
                user2server.Close();
                //返回接收的消息
                string restr = Encoding.UTF8.GetString(recievebuffer);
                restr = restr.Trim('\0');
                return restr;
            }
            else
            {
                try
                {
                    user2server.Send(sendbuffer);
                    user2server.Receive(recievebuffer);
                    //user2serverstream.Write(sendbuffer, 0, sendbuffer.Length);
                    //user2serverstream.Read(recievebuffer, 0, buffer_size);
                }
                catch (Exception e)
                {
                    MessageBox.Show("传输流错误:" + e.Message);
                }
                //返回接收的消息
                string restr = Encoding.UTF8.GetString(recievebuffer);
                restr = restr.Trim('\0');
                return restr;
            }

            //发送与接收字节
            
        }
    }
}
