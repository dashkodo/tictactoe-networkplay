using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;


namespace JsonLib
{
    [DataContract]
    public class MyPacket
    {
        [DataMember(IsRequired = true,Order = 0)]
        public string Type;
        [DataMember(IsRequired = true,Order=1)]
        public string Data;
    }

    [DataContract]
    public class Person
    {
        [DataMember(IsRequired = true,Order= 0)]
        public string name;
        [DataMember(IsRequired = true,Order=1)]
        public int wins;
        public Person(string name):this(name,0)
        {           
        }
        public Person(string name,int win)
        {
            this.name = name;
            this.wins = win;
        }
    }
}
