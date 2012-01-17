using ClientApi;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestTicTacToe
{
    
    
    /// <summary>
    ///Это класс теста для ClientParserTest, в котором должны
    ///находиться все модульные тесты ClientParserTest
    ///</summary>
    [TestFixture]
    public class ClientParserTest
    {

    
        /// <summary>
        ///Тест для plistParser
        ///</summary>
        [Test]
        public void plistParserTest()
        {
            string input = "[{\"name\":\"d\",\"wins\":2},{\"name\":\"2\",\"wins\":2}]";
            List<string> actual = Parser.plistParser(input);
            Assert.AreEqual("d", actual[0]);
            Assert.AreEqual("2", actual[1]);
        }
    }
}
