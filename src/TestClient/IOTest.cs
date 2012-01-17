using ClientApi;
using NUnit.Framework;
using JsonLib;
using System.Reflection;
namespace TestTicTacToe
{
    [TestFixture]
    public class IOTest
    {
            Mock.ClientSocketMock p = new Mock.ClientSocketMock();
       
        [Test]
        public void IOConstructorTest()
        {
            Assert.IsTrue(IO.Instance != null);
        }
        [Test]
        public void AnswerGameTest()
        {
            IO.Instance.Connection = p;
          
            IO.Instance.AnswerGame("s","n");
            Assert.AreEqual(p.JSONinString.Data, "s#n");
            Assert.AreEqual(p.JSONinString.Type, "Answer");
        }
        [Test]
        public void GetUserListTest()
        {
            IO.Instance.Connection = p;
        
           
            IO.Instance.GetUserList();
            Assert.AreEqual(p.JSONinString.Type, "GetUsers");
        }
        [Test]
        public void LoginTest()
        {
            IO.Instance.Connection = p;
        
            IO.Instance.Login("n");
            Assert.AreEqual(p.JSONinString.Data, "n");
            Assert.AreEqual(p.JSONinString.Type, "Auth");
        }
        [Test]
        public void MoveTest()
        {
            IO.Instance.Connection = p;
        
            IO.Instance.Move("2");
            Assert.AreEqual(p.JSONinString.Data, "2");
            Assert.AreEqual(p.JSONinString.Type, "Run");
        }
        
        [Test]
        public void RequestGameTest()
        {
            IO.Instance.Connection = p;
        
            IO.Instance.RequestGame("2");
            Assert.AreEqual(p.JSONinString.Data, "2");
            Assert.AreEqual(p.JSONinString.Type, "SetPartner");
        }
        [Test]
        public void ContentTest()
        {
            IO.Instance.Connection = p;
        
            IO.Instance.Content = JsonFactory.GetObjPacket(JsonFactory.GetJsonPacket("_t","_d"));
            Assert.AreEqual(IO.Instance.Content.Data,"_d");
            Assert.AreEqual(IO.Instance.Content.Type,"_t");
        }
        [Test]
        public void p_PropertyChangedTest()
        {

            IO.Instance.Connection = p;
            p.Content = JsonLib.JsonFactory.GetJsonPacket("_t","_d");
            object[] param = new object[2]{null, null};

            var m = typeof(IO).GetMethod("p_PropertyChanged", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(m);
            m.Invoke(IO.Instance,param );
            Assert.AreEqual(IO.Instance.Content.Data, "_d");
            Assert.AreEqual(IO.Instance.Content.Type, "_t");
        }
      
     }
}
