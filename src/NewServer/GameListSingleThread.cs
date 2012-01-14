using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NewServerApi
{
    class GameListSingleThread:AbsGameList
    {
         public override void StartGame(GameEventArgs e)
        {
            Game p = new Game(e.p1, e.p2);
            p.EndGameInstanceEvent += new GameInstanceEventHandler(EndInsGame);
            this._gameList.Add(p);
        }
         Thread t;
         public GameListSingleThread()
         {
             Thread t = new Thread(new ThreadStart(
                () =>
                {
                    while (true)
                    {
                        for (int i = 0; i < _gameList.Count;i++ )
                            _gameList[i].checkClient();
                        //Thread.Sleep(200);
                    }
                }
                ));
             t.Start();

         }

    }
}
