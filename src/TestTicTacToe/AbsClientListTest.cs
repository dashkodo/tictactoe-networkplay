using NewServerApi;
using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Collections.Generic;
using TestTicTacToe.Mock;
namespace TestTicTacToe
{


    [TestFixture]
    public class AbsClientListTest
    {

        AbsClientList[] CaseSource = new AbsClientList[]
        {
          new ClientListEachThread(),
          new ClientListSingleThread()
        };
    
        
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest3(AbsClientList obj)
        {
            AbsClientList target = obj;
            ClientMock a = new ClientMock();
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
           
            target.AddClient(a);
            target.AddClient(b);
            target.AddClient(c);
            Assert.AreEqual(a, target.ClientList[0]);
            Assert.AreEqual(b, target.ClientList[1]);
            Assert.AreEqual(c, target.ClientList[2]);

        }
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest1(AbsClientList obj)
        {
            AbsClientList target = obj;
            ClientMock p = new ClientMock();
            target.AddClient(p);
            Assert.AreEqual(p, target.ClientList[0]);

        }
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest0(AbsClientList obj)
        {
            AbsClientList target = obj;
            Assert.AreEqual(0, target.ClientList.Count);

        }
        
        [Test, TestCaseSource("CaseSource")]
        public void RemoveClientTest(AbsClientList obj)
        {
            AbsClientList target = obj;
            ClientMock a = new ClientMock();
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
            
            target.AddClient(a);
            target.AddClient(b);
            target.AddClient(c);
            Assert.AreEqual(3, target.ClientList.Count);
            target.RemoveClient(b);
            Assert.AreEqual(2, target.ClientList.Count);
            Assert.AreEqual(a, target.ClientList[0]);
            Assert.AreEqual(c, target.ClientList[1]);

            

        }

        [Test, TestCaseSource("CaseSource")]
        public void RemoveClientTestTcp(AbsClientList obj)
        {
            AbsClientList target = obj;
            TcpClient a = new TcpClient("127.0.0.1",80);
           
            target.AddClient(a);
            Assert.AreEqual(1, target.ClientList.Count);
            target.RemoveClient(a);
            Assert.AreEqual(0, target.ClientList.Count);
            


        }

       
        [ExpectedException(typeof(IgnoreException))]
        [Test, TestCaseSource("CaseSource")]
       public void StartGameTest(AbsClientList obj)
        {
            obj.AddClient(new ClientMock(null,"p1"));
            obj.AddClient(new ClientMock(null,"p2"));
            obj.StartGameEvent += new GameEventHandler(obj_StartGameEvent);
            obj.StartGame("p1","p2");
        }

        void obj_StartGameEvent(GameEventArgs e)
        {
            throw new IgnoreException("",null);
        }

       
    }
}
