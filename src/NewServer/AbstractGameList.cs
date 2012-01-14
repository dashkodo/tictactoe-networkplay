using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewServerApi
{
    public abstract class AbsGameList
    {
        protected List<Game> _gameList = new List<Game>();
        public List<Game> GameList 
        {
            get
            {
                return _gameList;
            }
        }

        public void Add(Game p)
        {
            _gameList.Add(p);
        }
        public void Remove(Game p)
        {
            _gameList.Remove(p);
        }
        public void Move(int val, String login)
        {
            for (int i = 0; i<_gameList.Count;i++)
            {
                Game p = _gameList[i];
                if ((p.player1.Login.Equals(login) || p.player2.Login.Equals(login)))
                {
                    p.Move(val, login);
                    return;
                }
            }
        }
        protected void EndInsGame(GameEventArgs e,Game p)
        {
            this._gameList.Remove(p);
            EndGameEvent(e);
        }
        public abstract void StartGame(GameEventArgs e);
        public event GameEventHandler EndGameEvent;
    }
}
