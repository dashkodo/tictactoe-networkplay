using NewServerApi;
using NUnit.Framework;
using System;
namespace TestTicTacToe
{
    
    
    /// <summary>
    ///Это класс теста для FieldTest, в котором должны
    ///находиться все модульные тесты FieldTest
    ///</summary>
    [TestFixture]
    public class FieldTest
    {




        /// <summary>
        ///Тест для Конструктор Field
        ///</summary>
        [Test]
        public void FieldConstructorTest()
        {
            Field target = new Field();
            Assert.IsTrue(target != null);
        }

        /// <summary>
        ///Тест для CheckWinBool
        ///</summary>
        [Test]
        public void CheckWinBoolTestTrue()
        {
            Field target = new Field();
            target.MakeMove(0);
            target.MakeMove(1);
            target.MakeMove(2);
            Assert.AreEqual(true, target.CheckWinBool());
        }
        /// <summary>
        ///Тест для CheckWinBool
        ///</summary>
        [Test]
        public void CheckWinBoolTestFalse()
        {
            Assert.AreEqual(false, (new Field()).CheckWinBool());

        }

        /// <summary>
        ///Тест для CheckWinInt
        ///</summary>
        [Test]
        public void CheckWinIntTest()
        {
            Assert.AreEqual(0, (new Field()).CheckWinInt());
        }

        /// <summary>
        ///Тест для Get
        ///</summary>
        [Test]
        public void GetTest()
        {
            Field target = new Field();
            target.MakeMove(1);
            target.MakeMovePair(2);
            Assert.AreEqual(-1, target.Get(0));
            Assert.AreEqual(0, target.Get(1));
            Assert.AreEqual(1, target.Get(2));
        }

    }
}
