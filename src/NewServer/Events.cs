using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewServerApi
{
    public delegate void GameEventHandler(GameEventArgs e);
    public class GameEventArgs : EventArgs
    {
        public Client p1, p2;
        public GameEventArgs(Client p1, Client p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
