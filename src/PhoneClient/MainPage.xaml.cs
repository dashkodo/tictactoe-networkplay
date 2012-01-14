using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ClientApi;
using Microsoft.Phone.Controls;

namespace PhoneClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        LoginZone lz = new LoginZone();
        SelectUserZone suz = new SelectUserZone();
        GameZone gz;
        IO ef = IO.Instance;
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            lz.Logged += new EventHandler(lz_Logged);
            suz.PlayerSelected += new EventHandler(suz_PlayerSelected);
            ef.WinStatusEvent += new EventHandler(ef_WinStatusEvent);
            ef.StartGameEvent += new EventHandler(StartGameEvent);
            ef.RequestGameEvent += new EventHandler(RequestGameEvent);
            ef.AnswerEvent += new EventHandler(AnswerEvent);
            ContentPanel.Children.Add(lz);
        }

        void ef_WinStatusEvent(object sender, EventArgs e)
        {
        //    ClientInfo.Instance.inGame = false;
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("You" + ef.Content.Data));

            
        }

        void AnswerEvent(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Вам Отказано!"));
            
        }
       
        void RequestGameEvent(object sender, EventArgs e)
        {

            string opponentuser = ef.Content.Data;
           // MessageBoxResult mb=MessageBoxResult.None;
         //   if (!ClientInfo.Instance.inGame)
            {
                
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        var mb = MessageBox.Show("Cыграть с " + opponentuser + "?", "Предложение", MessageBoxButton.OKCancel);
                        if (mb == MessageBoxResult.OK)
                        {
                            IO.Instance.AnswerGame(opponentuser, "yes");
                         //   ClientInfo.Instance.inGame = true;
                        }
                        else
                        {
                            IO.Instance.AnswerGame(opponentuser, "no");
                        }
                    });
            }

        }
        void StartGameEvent(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () =>
                 {
                     ContentPanel.Children.Clear();
                     this.PageTitle.Text = "Game";
                     gz = new GameZone(ef.Content.Data);
                     ContentPanel.Children.Add(gz);
                 });
        }

        void suz_PlayerSelected(object sender, EventArgs e)
        {
              Deployment.Current.Dispatcher.BeginInvoke(
                () =>
                 {
            ContentPanel.Children.Remove(suz);
           // IO.Instance.RequestGame(suz.SelectedPlayer);
                 });
        }
         //      

        void lz_Logged(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                   () =>
                   {
            ContentPanel.Children.Remove(lz);
            this.PageTitle.Text = "Select Pair";
            ContentPanel.Children.Add(suz);
             });
            IO.Instance.GetUserList();
        }


    }
}