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
                IO.Instance.Login(ClientInfo.Instance.UserName);
                var h = Logged;//event notification
                h(null, new EventArgs());
            }
        }
    }
}
