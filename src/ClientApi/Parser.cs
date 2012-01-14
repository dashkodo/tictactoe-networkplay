using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonLib;
namespace ClientApi
{
    public class Parser
    {
        public static  List<string> plistParser(String input) //userlist:user1;user2;etc...;
        {
            
            List<Person> plist = JsonFactory.GetObjPList(input);
            
            List<String> p = new List<string>() ;
            foreach (var i in plist)
            {
                p.Add(i.name);
            }
            return p;
        }
      /* static string RequestParser(MyPacket input)//MyPacket:login
        {
            return input.Data;
        }
       
       */ 
    }
}
