using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewServerApi
{
    public class GameListEachThread:AbsGameList
    {
        public override void StartGame(GameEventArgs e)
        {
            Game p = new Game(e.p1, e.p2);
            p.checkClientLoop();
            p.EndGameInstanceEvent += new GameInstanceEventHandler(EndInsGame);
            this._gameList.Add(p);
        }

    }
}
