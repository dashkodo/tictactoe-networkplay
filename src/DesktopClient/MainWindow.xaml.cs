using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientApi;
namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginZone lz = new LoginZone();
        SelectUserZone suz = new SelectUserZone();
        GameZone gz;
        IO ef = IO.Instance;
        // Конструктор
        public MainWindow()
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
           
            MessageBox.Show("You" + ef.Content.Data);
          
        }

        void AnswerEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Вам Отказано!");
            
        }
    
        void RequestGameEvent(object sender, EventArgs e)
        {

            string opponentuser = ef.Content.Data;
            // MessageBoxResult mb=MessageBoxResult.None;
        //    if (!ClientInfo.Instance.inGame)
            {
                MessageBoxResult mb = MessageBox.Show("Cыграть с " + opponentuser + "?", "Предложение", MessageBoxButton.OKCancel);

                {
                    if (mb == MessageBoxResult.OK)
                    {
                 //       ClientInfo.Instance.inGame = true;
                          ClientApi.IO.Instance.AnswerGame(opponentuser, "yes");
                    }
                    else
                    {
                        ClientApi.IO.Instance.AnswerGame(opponentuser, "no");
                    }
                }
            }

        }
        void StartGameEvent(object sender, EventArgs e)
        {
            Dispatcher.Invoke(
            (Action)(() =>
                 {
                     ContentPanel.Children.Clear();
                     this.PageTitle.Text = "Game";
                     gz = new GameZone(ef.Content.Data);
                     gz.Init(ef.Content.Data);
                     ContentPanel.Children.Add(gz);
                 }));
        }

        void suz_PlayerSelected(object sender, EventArgs e)
        {
            Dispatcher.Invoke(
            (Action)(() =>
            {
                ContentPanel.Children.Remove(suz);
           //       ClientApi.IO.Instance.RequestGame(suz.SelectedPlayer);
            }));
        }
         //      

        void lz_Logged(object sender, EventArgs e)
        {
            Dispatcher.Invoke(
            (Action)(() =>
            {
                ContentPanel.Children.Remove(lz);
                this.PageTitle.Text = "Select Pair";
                ContentPanel.Children.Add(suz);
            }));
              ClientApi.IO.Instance.GetUserList();
        }

    }
}
