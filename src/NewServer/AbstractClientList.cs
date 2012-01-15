using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;

namespace NewServerApi
{
 
    public abstract class AbsClientList
    {
        #region Protected
        protected void ReceiveMessage(object sender, EventArgs e)
        {
            Client client = (Client)sender;
            string message = client.Content;
            Parser.ParseSysCommand(message, client, this);
        }
        protected List<Client> _ClientList = new List<Client>();
        public abstract void AddClient(Client cl);
        #endregion

        public event GameEventHandler StartGameEvent;

        public List<Client> ClientList
        {
            get { return _ClientList; }
          //  private set { }
        }

        public void AddClient(TcpClient c)
        {
            AddClient(new Client(c));
        }

        
        public bool StartGame(string p1, string p2)
        {
            Trace.WriteLine("ICLientList.StartGame(" + p1 + "," + p2 + ");");

            Client p1cc = null, p2cc = null;
            for (int i = 0; i < this._ClientList.Count; i++)
            {
                var p = _ClientList[i];
                /// TODO: Add Check BusyState
                ///if (!busyclientArray.Contains(p)) 
                {
                    if (p.Login.Equals(p1))
                    {
                        p1cc = p;
                    }
                    if (p.Login.Equals(p2))
                    { p2cc = p; }
                }
            }
            if (p1cc != null && p2cc != null)
            {

                Trace.WriteLine("IClientList.StartGame(" + p1 + "," + p2 + "); Finded Both users");
                if(StartGameEvent!=null) StartGameEvent(new GameEventArgs(p1cc, p2cc));
                Trace.WriteLine("IClientList.StartGame(" + p1 + "," + p2 + "); EventStartGame");
                return true;
            } return false;

        }
        public void RemoveClient(TcpClient c)
        {
            foreach (var p in _ClientList)
                if (p.TcpClient == c)
                {
                    _ClientList.Remove(p);
                    return;
                }
        }

        public void RemoveClient(Client c)
        {
            foreach (var p in _ClientList)
                if (p == c)
                {
                    _ClientList.Remove(p);
                    return;
                }
        }
        public bool SendMessage(string Msg, string Login)
        {
            foreach (Client p in _ClientList)
                if (p.Login.Equals(Login))
                {
                    p.SendMessage(Msg);
                    return true;
                }
            return false;
        }


        public void ReturnUser(GameEventArgs e)
        {
            this.ClientList.Add(e.p1);
            this.ClientList.Add(e.p2);

        }
    }


    

}

