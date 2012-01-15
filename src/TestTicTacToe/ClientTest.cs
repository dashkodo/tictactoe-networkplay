using NewServerApi;
using NUnit.Framework;
using System;
using System.Net.Sockets;

namespace TestTicTacToe
{


    [TestFixture]
    public class ClientTest
    {
        ServerGeneral p = ServerGeneral.Instance;


        [Test]
        public void ClientConstructorTest()
        {
            Client tc = new Client(new TcpClient("192.168.56.1", 1111));
            Assert.IsNotNull(tc);
        }

        [Test]
        public void ClientConstructorTest1()
        {
            Client tc = new Client(new TcpClient("192.168.56.1", 1111), "nick");
            Assert.IsNotNull(tc);

        }

        [Test]
        public void ClientConstructorTest2()
        {
            Client tc = new Client();
            Assert.IsNotNull(tc);

        }

        [Test]
        public void CheckTest()
        {
            Client target = new Client(new TcpClient("192.168.56.1", 1111));
            Assert.False(target.Check());
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoopCheckTest()
        {
            Client target = new Client(new TcpClient("192.168.56.1", 1111));
            target.LoopCheck();
        }

        [Test]
        public void SendMessageTest()
        {
            Client target = new Client(new TcpClient("192.168.56.1", 1111));
            target.SendMessage("msg");

        }

    
        [Test]
        public void IsNotFreeTest()
        {
            Client target = new Client();
            bool expected = false;
            target.IsNotFree = expected;
            //actual = target.IsNotFree;
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoginTest()
        {
            //Client target = new Client(new TcpClient("192.168.56.1", 1111));
            Client target = new Client();
            target.Login = "p";
            Assert.AreEqual("p", target.Login);
        }
        [Test]
        public void ZTearDown()
        {
            p.StopServer();
        }
    }
}
