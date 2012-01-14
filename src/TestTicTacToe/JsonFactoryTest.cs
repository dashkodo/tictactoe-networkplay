using JsonLib;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestTicTacToe
{
    
    
    /// <summary>
    ///Это класс теста для JsonFactoryTest, в котором должны
    ///находиться все модульные тесты JsonFactoryTest
    ///</summary>
    [TestFixture]
    public class JsonFactoryTest
    {


        /// <summary>
        ///Тест для GetJsonPList проверка на NULL
        ///</summary>
        [Test]
        public void GetJsonPListTestNull()
        {
            List<Person> p = null; //  инициализация подходящего значения
            string actual = JsonFactory.GetJsonPList(p);
            Assert.AreEqual(null, JsonFactory.GetObjPList (actual));
        }
        /// <summary>
        ///Тест для GetJsonPList проверка пустого списка
        ///</summary>
        [Test]
        public void GetJsonPListTestEmpty()
        {
            List<Person> p = new List<Person>(); //  инициализация подходящего значения
            var plist=  JsonFactory.GetObjPList(JsonFactory.GetJsonPList(p));
            Assert.AreEqual(0,plist.Count);
        }
        /// <summary>
        ///Тест для GetJsonPList проверка 1 обьектa
        ///</summary>
        [Test]
        public void GetJsonPListTestOne()
        {
            List<Person> p = new List<Person>(); //  инициализация подходящего значения
            p.Add(new Person("n", 44));
            
            var plist = JsonFactory.GetObjPList(JsonFactory.GetJsonPList(p));

                Assert.AreEqual(p[0].name, plist[0].name);
                Assert.AreEqual(p[0].wins, plist[0].wins);
         }
        
        /// <summary>
        ///Тест для GetJsonPList проверка 3 обьектa
        ///</summary>
        [Test]
        public void GetJsonPListTestThree()
        {
            List<Person> p = new List<Person>(); //  инициализация подходящего значения
            p.Add(new Person("n", 44));
            p.Add(new Person("Connection", 42));
            p.Add(new Person("Connection", 41));
            
            var plist = JsonFactory.GetObjPList(JsonFactory.GetJsonPList(p));
            
            for (int i = 0; i <= 2; i++)
            {
                Assert.AreEqual(p[i].name, plist[i].name);
                Assert.AreEqual(p[i].wins, plist[i].wins);
            }
        }
        
        /// <summary>
        ///Тест для GetJsonPacket
        ///</summary>
        [Test]
        public void GetJsonPacketTest()
        {
            string type = "tp";
            string data = "dt";
            var p = JsonFactory.GetObjPacket(JsonFactory.GetJsonPacket(type, data));
            Assert.AreEqual(type, p.Type);
            Assert.AreEqual(data, p.Data);
        }
        
    }
}
