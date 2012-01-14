using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientApi;
namespace ConsoleClient
{
    class Program
    {
        static ClientSocket p = IO.Instance.Connection;
        static void Main()
        {
            p.Start();
            IO.Instance.Connection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(p_PropertyChanged);
            while (true) p.SendMsg(Console.ReadLine());
        }

        static void p_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine(p.Content);
        }
    }
}
