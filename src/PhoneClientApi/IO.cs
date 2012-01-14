using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using JsonLib;
using System.Threading;

namespace ClientApi
{
    public class IO
    {
        static public IO Instance = new IO();
        TcpClient Connection = new TcpClient();
        IO()
        {
            Connection.ReceivedMessage += new EventHandler(p_ReceivedMessage); 
        }

        void p_ReceivedMessage(object sender, EventArgs e)
        {
            string msg = Connection.Content;
            Content = JsonFactory.GetObjPacket(msg);
        }
        MyPacket msg;
        void p_PropertyChanged(object sender, EventArgs e)
        {
           // string msg  = ((TcpClient.MessageReceivedEvent)e).Message;
         //   Content = JsonFactory.GetObjPacket(msg);

        }

        public MyPacket Content
        {
            get { return msg; }
            set { msg = value; this.OnPropertyChanged(msg); }
        }


        public event EventHandler MoveEvent;
        public event EventHandler StartGameEvent;
        public event EventHandler WinStatusEvent;
        public event EventHandler RequestGameEvent;
        public event EventHandler AnswerEvent;
        public event EventHandler UserListEvent;
        public event EventHandler OtherEvent;
        protected void OnPropertyChanged(MyPacket s)
        {

            EventHandler handler = null;//= PropertyChanged;
            if (s!=null)
            switch (s.Type)
            {
               
                case "Run": handler = MoveEvent; break;
                case "Start": handler = StartGameEvent; break;
                case "Users": handler = UserListEvent; break;
                case "Status": handler = WinStatusEvent; break;
                case "SetPartnerRequest": handler = RequestGameEvent; break;
                case "Answer": handler = AnswerEvent; break;

                default:  break;
            }
            else handler = OtherEvent;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
        #region Send
        static string login = "Auth";//:login /// setup login
        static string usList = "GetUsers";/// that's all; =)
        static string move = "Run";//move:value
        static string request = "SetPartner";//MyPacket:user
        static string answer = "Answer";//answer:user;yes/no


        static string myLogin;
        public void Login(string msg)
        {
            String s = JsonFactory.GetJsonPacket(login, msg);
            Connection.SendMsg(s);
            Thread.Sleep(200);

        }
        public void GetUserList()
        {

            String s = JsonFactory.GetJsonPacket(usList, myLogin);
            Connection.SendMsg(s);


        }
        public  void RequestGame(string user)
        {

            String s = JsonFactory.GetJsonPacket(request, user);
            Connection.SendMsg(s);


        }

        public  void AnswerGame(string user, string ans)
        {
            String s = JsonFactory.GetJsonPacket(answer, user + "#" + ans);
            Connection.SendMsg(s);


        }

        public  void Move(string step)
        {
            String s = JsonFactory.GetJsonPacket(move, step);

            Connection.SendMsg(s);

        }
        #endregion
    }
}

