using NewServerApi;
using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Collections.Generic;
using TestTicTacToe.Mock;
namespace TestTicTacToe
{

    
    [TestFixture]
    public class AbsGameListTest
    {

        AbsGameList[] CaseSource = new AbsGameList[]
        {
          new GameListEachThread(),
          new GameListSingleThread()
        };
    
        
        [Test, TestCaseSource("CaseSource")]
        public void AddGameTest3(AbsGameList obj)
        {
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
            Game a = new Game(b,c);
     
            ClientMock b1 = new ClientMock();
            ClientMock c1 = new ClientMock();
            Game a1 = new Game(b1,c1);
            obj.Add(a);
            obj.Add(a1);
            Assert.AreEqual(2,obj.GameList.Count);
            Assert.AreEqual(a1,obj.GameList[1]);
            Assert.AreEqual(a,obj.GameList[0]);
            
        }

        
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest1(AbsGameList obj)
        {
            ClientMock p2 = new ClientMock();
            ClientMock p1 = new ClientMock();
            
            obj.Add(new Game(p1,p2));
            Assert.AreEqual(1, obj.GameList.Count);

        }
        [Test, TestCaseSource("CaseSource")]
        public void AddClientTest0(AbsGameList obj)
        {
            Assert.AreEqual(0, obj.GameList.Count);

        }
        
        [Test, TestCaseSource("CaseSource")]
        public void RemoveClientTest(AbsGameList obj)
        {
            ClientMock a = new ClientMock();
            ClientMock b = new ClientMock();
            ClientMock c = new ClientMock();
            Game a1 = new Game(a, b);
            Game b1 = new Game(a, c);
            Game c1 = new Game(c, b);
            obj.Add(a1);
            obj.Add(b1);
            obj.Add(c1);
            Assert.AreEqual(3, obj.GameList.Count);
            obj.Remove(b1);
            Assert.AreEqual(2, obj.GameList.Count);
            Assert.AreEqual(a1, obj.GameList[0]);
            Assert.AreEqual(c1, obj.GameList[1]);

        }

        [Test, TestCaseSource("CaseSource")]
        public void MoveTest(AbsGameList obj)
        {
            ClientMock a = new ClientMock(null, "s");
            ClientMock b = new ClientMock(null, "s2");
            Game a1 = new Game(a, b);
            obj.Add(a1);
            obj.Move(1, "s2");
            Assert.Pass();

        }
        [Test, TestCaseSource("CaseSource")]
        public void StartTest(AbsGameList obj)
        {
            ClientMock a = new ClientMock(null, "s");
            ClientMock b = new ClientMock(null, "s2");
            obj.StartGame(new GameEventArgs(a, b));
            Assert.AreEqual(1,obj.GameList.Count);

        }
           
    }

}
