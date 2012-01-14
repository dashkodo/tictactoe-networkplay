using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneClient
{
    public class ClientInfo
    {
        public static ClientInfo Instance = new ClientInfo();
        public String UserName { set; get; }
        //public Boolean inGame { get; set; }
        ClientInfo() { }

    }
}
