using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
namespace NewServerApi
{
    public class ClientListSingleThread:AbsClientList
    {
        
        Thread th;// =new Thread();
        public ClientListSingleThread()
        {
            var th = new Thread(new ThreadStart(Check));
            th.Start();

        }
        
        public override void AddClient(Client cl)
        {
            cl.ReceiveMessage += new EventHandler(ReceiveMessage);
            _ClientList.Add(cl);
            //      
        }
        void Check()
        {
            while (true)
            {
                for(int i =0; i<_ClientList.Count; i++)
                {
                    _ClientList[i].Check();
                }
                Thread.Sleep(200);
            }
        }
        
     
       
       
        
    }
    
}
