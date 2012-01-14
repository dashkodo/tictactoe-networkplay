using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using System.Net.Sockets;
using ClientApi;
namespace WebProxy
{
    class Program
    {
        static Dictionary<IWebSocketConnection, ClientSocket> allSockets = new Dictionary<IWebSocketConnection, ClientSocket>();
        static void Main(string[] args)
        {
            
            var server = new WebSocketServer("ws://localhost:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    var p = new ClientApi.ClientSocket();
                    p.Start();
                    p.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(p_PropertyChanged);
                    allSockets.Add(socket,p);
                    
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine("WebS => S: "+message);
                    allSockets[socket].SendMsg(message);
                };
            });

            Console.ReadLine();
        }

        static void p_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
                    
            ClientSocket s = (ClientSocket)sender;
        //    var sm = allSockets[s];
            foreach(var p in allSockets)
               if (p.Value == s)
                {
                    p.Key.Send(s.Content);
                    Console.WriteLine("S => WebS: "+s.Content);
           return;
                }






        }
    }
}
