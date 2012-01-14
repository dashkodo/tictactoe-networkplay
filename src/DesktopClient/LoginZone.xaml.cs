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

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для LoginZone.xaml
    /// </summary>
    public partial class LoginZone : UserControl
    {
        public LoginZone()
        {
            InitializeComponent();
        }
        public event EventHandler Logged;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                ClientInfo.Instance.UserName = textBox1.Text;
                ClientApi.IO.Instance.Login(ClientInfo.Instance.UserName);
                var h = Logged;//event notification
                h(null, new EventArgs());
            }
        }
    }
}
