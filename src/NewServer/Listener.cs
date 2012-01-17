#define TRACE
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.IO;
namespace NewServerApi
{
    public class ServerGeneral
    {
        public static ServerGeneral Instance = new ServerGeneral();
        private TcpListener tcpServer;
        //private TcpClient tcpClient;
        private Thread th;
        AbsClientList clientArray;// = new ClientListSingleThread();
        AbsGameList gameArray;// = new GameListEachThread();
        public int Port = 1111;
        public TraceSwitch tracelevel = new TraceSwitch("mySwitch", "mydesc");

        //ServerGeneral():this(new ClientListEachThread(),new GameListSingleThread()){}
        //ServerGeneral():this(new ClientListSingleThread(),new GameListEachThread()){}
        //ServerGeneral():this(new ClientListEachThread(),new GameListEachThread()){}
        ServerGeneral():this(new ClientListSingleThread(),new GameListSingleThread()){}
        ServerGeneral(AbsClientList cl, AbsGameList gl)
        {
            this.clientArray = cl;
            this.gameArray = gl;
            Trace.Listeners.Add(new ConsoleTraceListener());
            tracelevel.Level = TraceLevel.Info;//TODO: Setup Tracelevel
            Trace.WriteLineIf(tracelevel.TraceInfo, "Creating new thread for startserver");
            th = new Thread(new ThreadStart(StartListen)); //запускаем поток листенера
            th.Start();
            Trace.WriteLineIf(tracelevel.TraceInfo, "Server Started");
            gameArray.EndGameEvent += new GameEventHandler(clientArray.ReturnUser);
            clientArray.StartGameEvent += new GameEventHandler(gameArray.StartGame);

        }
        bool flag = true;
        #region Server Start/Stop
        public void StartListen() //cлушаем порт в потоке для приема входящх подключений
        {
            Trace.WriteLine("ServerGeneral.StartListen(); Starting loop for TCP listener");

            IPAddress localAddr = IPAddress.Parse("192.168.56.1");
            //      Port = 2020; IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            tcpServer = new TcpListener(localAddr, Port);
            tcpServer.Start();
            Trace.WriteLineIf(tracelevel.TraceInfo, "Starting loop for TCP listener");
            flag = true;
            while (flag)
            {
                var p = tcpServer.AcceptTcpClient();
                if (p != null)
                    this.clientArray.AddClient(p);
            }

        }
        public void StopServer()
        {
            Trace.WriteLineIf(tracelevel.TraceInfo, "StopServer()");
            flag = false;
            if (tcpServer != null)
            {
                tcpServer.Stop();
            }
            th.Abort();
        }
        #endregion


    }
}
