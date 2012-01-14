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
namespace PhoneClient
{
    public partial class SelectUserZone : UserControl
    {
        ClientApi.IO ef = IO.Instance;
        public SelectUserZone()
        {
            InitializeComponent();
           ef.UserListEvent += new EventHandler(Instance_UserListEvent);
        }
        List<String> ListOfUsers = new List<string>();
        void Instance_UserListEvent(object sender, EventArgs e)
        {
            
            Deployment.Current.Dispatcher.BeginInvoke(() => listBox1.Items.Clear());//lambda
            this.ListOfUsers = new List<string>(ClientParser.plistParser(ef.Content.Data));
            foreach (var item in ListOfUsers)
            {
                var tmpItem = item;
                Deployment.Current.Dispatcher.BeginInvoke(() => listBox1.Items.Add(tmpItem));//lambda
            }
        }
        public event EventHandler PlayerSelected;
        private void select_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                SelectedPlayer = (string)listBox1.Items[listBox1.SelectedIndex];

                //if (!ClientInfo.Instance.inGame)
                {
              //      ClientInfo.Instance.inGame = true;
                    IO.Instance.RequestGame(SelectedPlayer);
                    var h = PlayerSelected;//event notification
                    h(null, new EventArgs());
                }
            }
        }
        public String SelectedPlayer; 
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            IO.Instance.GetUserList();
        }
    }
}
