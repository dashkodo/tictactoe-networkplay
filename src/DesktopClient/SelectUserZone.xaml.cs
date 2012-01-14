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
    /// Логика взаимодействия для SelectUserZone.xaml
    /// </summary>
    public partial class SelectUserZone : UserControl
    {
        IO ef = IO.Instance;
        public SelectUserZone()
        {
            InitializeComponent();
            ef.UserListEvent += new EventHandler(Instance_UserListEvent);
        }
        List<String> ListOfUsers = new List<string>();
        void Instance_UserListEvent(object sender, EventArgs e)
        {
                Dispatcher.Invoke((Action)(() =>  listBox1.Items.Clear()));//lambda
            this.ListOfUsers = new List<string>(Parser.plistParser(ef.Content.Data));
            foreach (var item in ListOfUsers)
            {
                var tmpItem = item;
                Dispatcher.Invoke((Action)(() =>        listBox1.Items.Add(tmpItem)));//lambda
         
         
            }
        }
        public event EventHandler PlayerSelected;
        private void select_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                SelectedPlayer = (string)listBox1.Items[listBox1.SelectedIndex];
              
                    ClientApi.IO.Instance.RequestGame(SelectedPlayer);
                    
                    var h = PlayerSelected;//event notification
                    h(null, new EventArgs());
            }
        }
        public String SelectedPlayer;
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            ClientApi.IO.Instance.GetUserList();
        }
    }
}
