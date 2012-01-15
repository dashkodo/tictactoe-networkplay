using ClientApi;
using NewServerApi;
using System;

namespace TestTicTacToe.Mock
{
    class ClientSocketMock:ClientSocket
    {
        public string inString;
        public JsonLib.MyPacket JSONinString;
        public override void SendMsg(string s) { inString = s; JSONinString = JsonLib.JsonFactory.GetObjPacket(s); }

     }
    class ClientMock : Client
    {
        public string input;
        public ClientMock() { }
        public ClientMock(System.Net.Sockets.TcpClient p, string s)
        {
            this.Login = s;
        }
        public override void ThreadStateChange()
        {
            //base.ThreadStateChange();
        }
        public override void SendMessage(string Message)
        {
            input = Message;
            //base.SendMessage(Message);
        }

        public override bool Check()
        {
            input = "input";
            return false;
            //base.SendMessage(Message);
        }
        
    }
}
