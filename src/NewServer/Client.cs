using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
namespace NewServerApi
{

    
    public class Client
    {
        public event EventHandler ReceiveMessage;
        public String Login { get; set; }
        public bool IsNotFree { get; set; }
        protected TcpClient _tcpClient;
        List<string> _content = new List<string>();
        public Thread thread;

        public string Content
        {
            get;
            private set;
        }
        protected NetworkStream _tcpClientStream;
        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            private set { }
        }
        public Client()
          
        { }
        
        public Client(TcpClient tc): this(tc, "LoggedInTime" + System.DateTime.UtcNow.ToString())
        {}
        public Client(TcpClient tc, String Nick)
        {
            IsNotFree = false;
            _tcpClient = tc;
            Login = Nick;
            _tcpClientStream = _tcpClient.GetStream();
            thread = new Thread(new ThreadStart(()=>
            {
                while(true)
                {
                    Check();
                    Thread.Sleep(500);
                }
            }));
        }
        public virtual void ThreadStateChange()
        {
            if (thread.ThreadState == System.Threading.ThreadState.Unstarted)
                thread.Start();
            else
            if (thread.ThreadState == System.Threading.ThreadState.Running)
                thread.Suspend();
            else thread.Resume();
        }
        public void LoopCheck()
        {
                thread.Start();
        
        }
        public virtual void SendMessage(String Message)
        {
            byte[] bt;
            bt = Encoding.UTF8.GetBytes(Message+"\n");
            Thread.Sleep(200);
            _tcpClient.Client.Send(bt);
            Trace.WriteLine("SendMsgTo " + Login + ":" + Message);
            
        }
        public  bool Check()
        {     if (_tcpClient.Available > 0)
                {
                    int size = _tcpClient.Available;
                    byte[] arr = new byte[size];
                    _tcpClientStream.Read(arr, 0, size);
                    Content = Encoding.UTF8.GetString(arr);
                    Trace.WriteLine("SendMsgFrom " + Login + ":" + Content);
                   
                    _tcpClientStream.Flush();
                    ReceiveMessage(this, null);
                    return true;
                }
               return false;
        }


      
    }
  //   */
}
