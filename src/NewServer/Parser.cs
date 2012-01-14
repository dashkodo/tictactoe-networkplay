using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JsonLib;

namespace NewServerApi
{
    public class Parser
    {
     public static bool ParseGameCommand(MyPacket tmp,Client ic,Game g)
        {
         
            bool ret = true;
            if (tmp != null)
                switch (tmp.Type)
                {
                    case "Run":
                        {
                            int value = int.Parse(tmp.Data);
                            g.Move(value, ic.Login);
                            break;
                        }
                    case "Answer":
                        {
                            string msg = tmp.Data;
                            string login = msg.Substring(0, msg.IndexOf("#"));
                            string answer = msg.Substring(msg.IndexOf("#") + 1);
                            if (answer.Equals("yes"))
                            {
                                g.yesAns++;
                                if (g.yesAns == 2) g.Init();
                            }
                            else
                            {
                                g.EndGame();
                            }
                            break;
                        }
                    default:
                        {
                         
                            break;
                        }
                }
            else
                ret = false;
            return ret;
        }

   
    
        public static bool ParseSysCommand(String msg,Client ic,AbsClientList icl)
        {
            MyPacket tmp = JsonFactory.GetObjPacket(msg);
            bool ret = true;
            if (tmp != null)
                switch (tmp.Type)
                {
                    case "Auth":
                        {
                            ic.Login = tmp.Data;
                            break;
                        }
                    case "GetUsers":
                        {
                            List<Person> plist = new List<Person>();
                            for (int i = 0; i < icl.ClientList.Count(); i++)
                            {
                                Client p = icl.ClientList[i];
                                if (!p.Login.Equals(ic.Login)) plist.Add(new Person(p.Login));
                            } 
                            string tmpMsg = JsonFactory.GetJsonPList(plist);
                            ic.SendMessage(JsonFactory.GetJsonPacket("Users", tmpMsg));


                            break;
                        }
                    case "SetPartner":
                        {

                            if (!ic.Login.Equals(tmp.Data) && !ic.IsNotFree)
                            {
                                icl.SendMessage(JsonFactory.GetJsonPacket(tmp.Type + "Request", ic.Login), tmp.Data);
                                ic.IsNotFree = true;
                            }
                            break;

                        }

                    case "Answer":
                        {
                            msg = tmp.Data;
                            string login = msg.Substring(0, msg.IndexOf("#"));
                            string answer = msg.Substring(msg.IndexOf("#") + 1);
                            if (answer.Equals("yes"))
                            {
                                icl.StartGame(ic.Login, login);
                            }
                            else
                            {
                       icl.SendMessage(JsonFactory.GetJsonPacket(tmp.Type, answer), login);
                                ic.IsNotFree = false;
                            }
                            break;
                        }

                    default:
                        {
                            //ret = ParseGameCommand(tmp, ic);
                           // return ret;
                            break;
                        }
                }
            else
                ret = false;
            return ret;
        }

    }
} 

/*        public static void WebClientParse(string input,IClient ic)
        {
            int s = input.Length;
            int s2 = "Hello Web Socket!".Length;
            string msg;
            if (input.StartsWith("GET"))
                msg = WebSocket.HandShake76(input);
            else
            {
                Thread.Sleep(500);
                WebSocket.DecodingRFC(input);
                msg ="Unknown Command"   
            }
           ic.SendMessage((msg));
             
        }*/

