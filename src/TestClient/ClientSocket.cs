using NewServerApi;
using ClientApi;
using NUnit.Framework;
using System;
using System.Net.Sockets;

namespace TestTicTacToe
{


    //[TestFixture]
    public class ClientSocketTest
    {
        ServerGeneral p = ServerGeneral.Instance;


        [Test]
        public void ClientConstructorTest()
        {
            
            ClientSocket tc = new ClientSocket();
            Assert.IsNotNull(tc);
        }
     
        [Test]
        public void ClientSendMsgTest()
        {           
            ClientSocket tc = new ClientSocket();
            tc.SendMsg("msg");
            Assert.Pass();
        }
        [Test]
        public void ClientContentTest()
        {
            ClientSocket tc = new ClientSocket();
            tc.Content = "ms";
            Assert.AreEqual(tc.Content,"ms");
        }
        [Test]
        public void CloseConnTest()
        {
            p.StopServer();
        }
    }
}
