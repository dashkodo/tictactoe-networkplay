#define TRACE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewServerApi;
using System.Threading;
using System.Diagnostics;
using JsonLib; 
namespace NewServerApi
{
    public delegate void GameInstanceEventHandler(GameEventArgs p, Game inst);
    public class Game
    {

        public GameInstanceEventHandler EndGameInstanceEvent;
        public int ID { get; set; }
        public int yesAns=0;
        Field gamefield;
        int moves = 9;
        public Game(Client p1,Client p2)
        {
            player1 = p1;
            player2 = p2;
            Init();
           
        }
        public void Init()
        {
            //
            Trace.WriteLine("game.Init();");
            //
            
            gamefield = new Field();
            Random r = new Random();
            figureP1isRound = (r.Next(0, 2));
            moveP1 = (r.Next(0, 2));
            moves = 9;
            startMsg();
            yesAns = 0;
            //
            Trace.WriteLine("game.Init();InitSucsess");
            //
          
        }
        public void EndGame()
        {
            EndGameInstanceEvent(new GameEventArgs(this.player1,this.player2),this);
        }
        void startMsg()
        {
            int p1 = moveP1*2 + figureP1isRound;//(moveP1+figureP1isRound).ToString
            int p2=3-p1;//((moveP1 + 1) % 2) + ((figureP1isRound + 1) % 2)
            Thread.Sleep(1000);
            player1.SendMessage(JsonFactory.GetJsonPacket("Start", p1.ToString()));
            player2.SendMessage(JsonFactory.GetJsonPacket("Start", p2.ToString()));


        }

        public void checkClientLoop()
        {
            Thread t = new Thread(new ThreadStart(
                () =>
                {
                    while (true)
                    {
                        checkClient();
                        //Thread.Sleep(200);
                    }
                }
                ));
            t.Start();
            
        }
        
        public void checkClient()
        {
            if (player1.Check()) Parser.ParseGameCommand(JsonFactory.GetObjPacket(player1.Content), player1,this);
            if (player2.Check()) Parser.ParseGameCommand(JsonFactory.GetObjPacket(player2.Content), player2, this);
        }
        public void Move(int val,string login)
        {
            bool flag = false;
            if (moveP1 == 0 && login.Equals(player2.Login))
            {
                
                if (gamefield.MakeMove(val)) flag = true;
                }
            else if (moveP1 == 1 && login.Equals(player1.Login))            //&&login.Equals(player2.Login)
            {
                if (gamefield.MakeMovePair(val)) flag = true;
            }

            if (flag == false) return;
            moves--;
          //
            Trace.WriteLine("game.Move(" + val + "," + login + "); CorrectMove");
          //
            player1.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Run", val.ToString()));
            player2.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Run", val.ToString()));
            if (gamefield.CheckWinBool())
            {
                if (moveP1 == 1)
                {
                    player1.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status","win"));
                    player2.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status","lose"));
                }
                else
                {
                    player2.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status","win"));
                    player1.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status","lose"));
                }
                
                ReInit();
            }
            if (moves == 0)
            {
                player2.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status", "make draw"));
                player1.SendMessage(JsonLib.JsonFactory.GetJsonPacket("Status", "make draw"));
                ReInit();
            }
            moveP1 += 1;
            moveP1 %= 2;
        }
        
        
        void ReInit()
        {
            Thread.Sleep(1500);
            yesAns = 0;
            player1.SendMessage(JsonLib.JsonFactory.GetJsonPacket("SetPartnerRequest", "Again"));
            player2.SendMessage(JsonLib.JsonFactory.GetJsonPacket("SetPartnerRequest", "Again"));
          
        }

        public Client player1, player2;
        int moveP1;
        int figureP1isRound;
    }
}
