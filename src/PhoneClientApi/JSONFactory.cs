using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;


namespace JsonLib
{
    public class JsonFactory
    {
        public static string GetJsonPacket(string type, string data)
        {

            DataContractJsonSerializer sObj = new DataContractJsonSerializer(typeof(MyPacket));
            MyPacket p = new MyPacket();
            p.Type = type;
            p.Data = data;
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            sObj.WriteObject(s, p);
            s.Position = 0;
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            string obj = sr.ReadToEnd();
            return obj;
        }
        public static MyPacket GetObjPacket(string input)
        {
            Stream m = new MemoryStream();
            //input = input.Substring(0,input.IndexOf("\0"));
            m.Write(Encoding.UTF8.GetBytes(input), 0, input.Length);
            return GetObjPacket(m);
        }
        public static MyPacket GetObjPacket(Stream input)
        {
            input.Position = 0;
            DataContractJsonSerializer sObj = new DataContractJsonSerializer(typeof(MyPacket));
            MyPacket ret = null;
            try
            {
                ret = sObj.ReadObject(input) as MyPacket;
            }
            catch { }
            return ret;
        }

        public static string GetJsonPList(List<Person> p)
        {

            DataContractJsonSerializer sObj = new DataContractJsonSerializer(typeof(List<Person>));
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            sObj.WriteObject(s, p);
            s.Position = 0;
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            string obj = sr.ReadToEnd();
            return obj;
        }

        public static List<Person> GetObjPList(string input)
        {
            Stream m = new MemoryStream();
            m.Write(Encoding.UTF8.GetBytes(input), 0, input.Length);
            return GetObjPList(m);
        }
        public static List<Person> GetObjPList(Stream input)
        {
            input.Position = 0;
            DataContractJsonSerializer sObj = new DataContractJsonSerializer(typeof(List<Person>));
            List<Person> ret = sObj.ReadObject(input) as List<Person>;
            return ret;
        }
    }


    [DataContract]
    public class MyPacket
    {
        [DataMember(IsRequired = true)]
        public string Type;
        [DataMember(IsRequired = false)]
        public string Data;
    }

    [DataContract]
    public class Person
    {
        [DataMember(IsRequired = true)]
        public string name;
        [DataMember(IsRequired = true)]
        public int wins;
        public Person(string name)
            : this(name, 0)
        {
        }
        public Person(string name, int win)
        {
            this.name = name;
            this.wins = win;
        }
    }
}
