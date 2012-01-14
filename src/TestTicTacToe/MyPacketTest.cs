using JsonLib;
using NUnit.Framework;

namespace TestTicTacToe
{
    
    
    /// <summary>
    ///Это класс теста для MyPacketTest, в котором должны
    ///находиться все модульные тесты MyPacketTest
    ///</summary>
    [TestFixture]
    public class MyPacketTest
    {

        [Test]
        public void MyPacketConstructorTest()
        {

            string v1 = "person NAme"; // инициализация подходящего значения
            string v2 = "person NAme"; // инициализация подходящего значения
            MyPacket target = new MyPacket();
            target.Data = v1;
            target.Type = v2;

            Assert.AreEqual(v1, target.Data);
            Assert.AreEqual(v2, target.Type);
          
        }
    }
}
