using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;
namespace NewServerApi
{
    public class ClientListEachThread : AbsClientList
    {

         List<Thread> _ThreadList = new List<Thread>();
        public override void AddClient(Client cl)
        {
            
            cl.ReceiveMessage += new EventHandler(ReceiveMessage);
            _ClientList.Add(cl);
            var th = new Thread(new ThreadStart(cl.ThreadStateChange));
            _ThreadList.Add(th);
            th.Start();

        }

     
        
       
        
    }
}
