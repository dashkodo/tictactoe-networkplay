using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

using System.Threading;
namespace ClientApi
{
     public class TcpClient
    {
        Socket client = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        //Requster client = new Requster("http://192.168.56.1",1111);
        public event EventHandler ReceivedMessage;
        string content;
        public string Content
        {
            get { return content; }
            set { content = value; ReceivedMessage(this, null); }
        }

       public TcpClient()
        {
            SocketAsyncEventArgs socketEventArgs = new SocketAsyncEventArgs();
            socketEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"),1111);
            socketEventArgs.Completed +=new EventHandler<SocketAsyncEventArgs>(socketEventArgs_Completed);
            client.ConnectAsync(socketEventArgs);
        }

        void  socketEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            new Thread(new ThreadStart(Listen)).Start();
        }
        public void SendMsg(string data)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = client.RemoteEndPoint;
         //   socketEventArg.UserToken = null;
                byte[] payload = Encoding.UTF8.GetBytes(data);
                socketEventArg.SetBuffer(payload, 0, payload.Length);
                client.SendAsync(socketEventArg);

        }
         public void Listen()
        {
            while (true)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = client.RemoteEndPoint;

                string response;
                socketEventArg.SetBuffer(new Byte[2048], 0, 2048);
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    //                    response = response.Remove('\0');
                    Content = response;
                });
                client.ReceiveAsync(socketEventArg);
            }
        }

        /// <summary>
        /// Closes the Socket connection and releases all associated resources
        /// </summary>
        public void Close()
        {
            if (client != null)
            {
                client.Close();
            }
        }

     }

    /*public class TcpClient2
    {
        public static TcpClient Instance = new TcpClient();
        Client client = new Client();
        //Requster client = new Requster("http://192.168.56.1",1111);
        public event EventHandler ReceivedMessage;

        public String Content;
        TcpClient()
        {
            client.Connected += new Client.ConnectArgs(client_Connected);
            client.Recieved += new Client.RecievedArgs(instance_Recieved);
            client.ConnectAsync("192.168.56.1", 1111);

        }

        void client_Connected(string s)
        {
            client.StartListening();

        }

        void instance_Recieved(byte[] result)
        {
            Content = Encoding.UTF8.GetString(result, 0, result.Length);
            Content.Trim();
            var handler = ReceivedMessage;
            if (handler != null) handler(null, new EventArgs());
        }
        public void SendMsg(string s)
        {
            client.SendAsync(s);
            Thread.Sleep(500);

        }

    }
     */ 
}