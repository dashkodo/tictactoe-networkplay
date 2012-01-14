using JsonLib;
using NUnit.Framework;

namespace TestTicTacToe
{
    
    
    /// <summary>
    ///Это класс теста для PersonTest, в котором должны
    ///находиться все модульные тесты PersonTest
    ///</summary>
    [TestFixture]
    public class PersonTest
    {


        
        /// <summary>
        ///Тест для Конструктор Person
        ///</summary>
        [Test]
        public void PersonConstructorTest()
        {
            string name = "person NAme"; // инициализация подходящего значения
            int win = 23; // инициализация подходящего значения
            Person target = new Person(name, win);
            Assert.AreEqual(win, target.wins);
            Assert.IsTrue(target.name.Equals(name)); 
        }

        /// <summary>
        ///Тест для Конструктор Person
        ///</summary>
        [Test]
        public void PersonConstructorTest1()
        {
            string name = "person NAme";
            Person target = new Person(name);
            Assert.AreEqual(0, target.wins);
            Assert.IsTrue(target.name.Equals(name)); 
        }
    }
}
