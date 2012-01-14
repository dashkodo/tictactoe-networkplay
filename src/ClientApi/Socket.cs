using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.ComponentModel;
using System.Threading;
namespace ClientApi
{
 
    public class ClientSocket
    {
        //public static ClientSocket Instance = new ClientSocket();
        Thread T;
        string content;
        public string Content
        {
            get { return content; }
            set { content = value; this.OnPropertyChanged("Content"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public virtual void SendMsg(String input)
        {
            if (ns == null) Start();

            Thread.Sleep(500);
            if (input == "exit")
                CloseConnection();
            //ns.Write(Encoding.UTF8.GetBytes(input), 0, input.Length);
            ns.Write(Encoding.UTF8.GetBytes(input+"\n"), 0, input.Length+1);
            ns.Flush();

        }
        TcpClient server;
        NetworkStream ns;
        public void Start()
        {
            try
            {
             //   server = new TcpClient("195.182.194.146", 2020);
                server = new TcpClient("192.168.56.1", 1111);

            }
            catch (SocketException)
            {
                return;
            }
            ns = server.GetStream();
            flag = true;
            this.T=new Thread(new ThreadStart(Listen));
            T.Start();


        }
        ~ClientSocket()
        {
            CloseConnection(); 
        }
        public void CloseConnection()
        {
            flag = false;

            ns.Close();
            server.Close();
        }
        bool flag = true; 
        void Listen()

        {
            while (flag)
            {
                if (server.Available > 0)
                {
                    int size = server.Available;
                    byte[] arr = new byte[size];
                    ns.Read(arr, 0, size);
                    Content = Encoding.UTF8.GetString(arr);
                    ns.Flush();
                }
                Thread.Sleep(500);
               
            }
        }


    }
 
}

