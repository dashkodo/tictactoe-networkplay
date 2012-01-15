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
            ClientMock a = new ClientMock();
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
           
            obj.AddClient(a);
            obj.AddClient(b);
            obj.AddClient(c);
            Assert.AreEqual(a, obj.ClientList[0]);
            Assert.AreEqual(b, obj.ClientList[1]);
            Assert.AreEqual(c, obj.ClientList[2]);

        }
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest1(AbsClientList obj)
        {
            ClientMock p = new ClientMock();
            obj.AddClient(p);
            Assert.AreEqual(p, obj.ClientList[0]);

        }
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest0(AbsClientList obj)
        {
            AbsClientList target = obj;
            Assert.AreEqual(0, obj.ClientList.Count);

        }
        
        [Test, TestCaseSource("CaseSource")]
        public void RemoveClientTest(AbsClientList obj)
        {
            ClientMock a = new ClientMock();
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
            
            obj.AddClient(a);
            obj.AddClient(b);
            obj.AddClient(c);
            Assert.AreEqual(3, obj.ClientList.Count);
            obj.RemoveClient(b);
            Assert.AreEqual(2, obj.ClientList.Count);
            Assert.AreEqual(a, obj.ClientList[0]);
            Assert.AreEqual(c, obj.ClientList[1]);

            

        }

        [Test, TestCaseSource("CaseSource")]
        public void RemoveClientTestTcp(AbsClientList obj)
        {
            TcpClient a = new TcpClient("127.0.0.1",80);
           
            obj.AddClient(a);
            Assert.AreEqual(1, obj.ClientList.Count);
            obj.RemoveClient(a);
            Assert.AreEqual(0, obj.ClientList.Count);
            


        }

       
       
        [Test, TestCaseSource("CaseSource")]
        public void StartGameFalseTest(AbsClientList obj)
        {
            obj.AddClient(new ClientMock(null, "p1"));
            obj.AddClient(new ClientMock(null, "p2"));
                     Assert.IsFalse(obj.StartGame("p1", "p3"));
        }
        [Test, TestCaseSource("CaseSource")]
        public void StartGameTrueTest(AbsClientList obj)
        {
            obj.AddClient(new ClientMock(null, "p1"));
            obj.AddClient(new ClientMock(null, "p2"));
            obj.StartGameEvent += new GameEventHandler((GameEventArgs e) => { });
            Assert.IsTrue(obj.StartGame("p1", "p2"));
        }
      
       

        [Test, TestCaseSource("CaseSource")]
        public void SendMessageTest(AbsClientList obj)
        {
            ClientMock p1 = new ClientMock(null, "p1");
            ClientMock p2 = new ClientMock(null, "p2");
            obj.AddClient(p1);
            obj.AddClient(p2);
            
            Assert.IsTrue(obj.SendMessage("m1", "p1"));
             Assert.IsTrue(obj.SendMessage("m2", "p2"));

            Assert.AreEqual("m1", p1.input);
            Assert.AreEqual("m2", p2.input);
        }


        [Test, TestCaseSource("CaseSource")]
        public void SendMessageFalseTest(AbsClientList obj)
        {
            ClientMock p1 = new ClientMock(null, "p1");
            obj.AddClient(p1);
         
            Assert.IsFalse(obj.SendMessage("m2", "p2"));
       }

        [Test, TestCaseSource("CaseSource")]
        public void ReturnUserTest(AbsClientList obj)
        {
            ClientMock p1 = new ClientMock(null, "p1");
            ClientMock p2 = new ClientMock(null, "p2");
            Assert.AreEqual(0, obj.ClientList.Count);
         
            obj.ReturnUser(new GameEventArgs(p1,p2));
            Assert.AreEqual(2,obj.ClientList.Count);
        }
       


    }
}
