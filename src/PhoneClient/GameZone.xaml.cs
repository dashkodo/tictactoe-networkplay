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
    public partial class GameZone : UserControl
    {
        public GameZone()
        {
            InitializeComponent();

        }

        public GameZone(string s)
        {
            InitializeComponent();
            Init(s);
            IO.Instance.MoveEvent += new EventHandler(MoveEvent);
        }
        public bool oFlag = false;
        public bool userFigureIsO = false;
        bool drawSuccess = false;
        public void Draw(Canvas c)
        {
            if (oFlag)
                DrawO(c);
            else
                DrawX(c);

            drawSuccess = true;

        }
        public void DrawUserFigure(Canvas c)
        {
            if (userFigureIsO) DrawO(c);
            else DrawX(c);
        }

        public void XOInit(string s)
        {
            ///3 11 U MOVE U O 
            ///0 00 OP MOVE U X 
            ///2 10 U MOVE U X
            ///1 01 OP MOVE U O
            switch (s)
            {
                case "3": oFlag = true; break;
                case "0": oFlag = true; break;
                default: oFlag = false; break;

            }
            switch (s)
            {
                case "3": userFigureIsO = true; break;
                case "1": userFigureIsO = true; break;
                default: userFigureIsO = false; break;

            }

        }

        public void DrawO(Canvas c)
        {
            Ellipse el = new Ellipse();
            el.Width = c.Width;
            el.Height = c.Height;
            el.StrokeThickness = 3;
            el.Stroke = new SolidColorBrush(Color.FromArgb(125, 0, 0, 125));
            c.Children.Add(el);
        }
        public void DrawX(Canvas c)
        {

            Line line1 = new Line();
            Line line2 = new Line();
            line1.StrokeThickness = line2.StrokeThickness = 3;
            line1.Stroke = line2.Stroke = new SolidColorBrush(Color.FromArgb(125, 255, 0, 125));

            line1.X1 = 0;
            line1.Y1 = 0;
            line1.X2 = c.Width;
            line1.Y2 = c.Height;
            line2.X1 = c.Width;
            line2.Y1 = 0;
            line2.X2 = 0;
            line2.Y2 = c.Height;
            c.Children.Add(line1);
            c.Children.Add(line2);


        }
        
        delegate void mydeleg(Canvas c);
        void MoveEvent(object sender, EventArgs e)
        {
            int value = int.Parse(IO.Instance.Content.Data);
            // if (game.Move(value))t

            mydeleg md = Draw;
            arrControl[value].Dispatcher.BeginInvoke( md, arrControl[value]);
            if (drawSuccess)
            {
                oFlag = !oFlag;
                drawSuccess = false;
            }
        }

        //ClientGame game = new ClientGame();
        Canvas[] arrControl = new Canvas[10];
        void Init(string s)
        {
            arrControl[0] = cell1;
            arrControl[1] = cell2;
            arrControl[2] = cell3;
            arrControl[3] = cell4;
            arrControl[4] = cell5;
            arrControl[5] = cell6;
            arrControl[6] = cell7;
            arrControl[7] = cell8;
            arrControl[8] = cell9;
            arrControl[9] = userFigure;
            XOInit(s);
            //game.Init(s);
            if (s.Equals("2") || s.Equals("3")) MessageBox.Show("Ваш первый ход");
            else MessageBox.Show("Первый ход оппонента");
            //DrawXO.userFigureIsO;// = !game.canMove;
            //oFlag ^= s.Equals("0") || s.Equals("1");

            DrawUserFigure(userFigure);
        }

      
        
        private void cellMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int moveId;
            switch ((sender as Canvas).Name)
            {

                case "cell1": moveId = 0; break;
                case "cell2": moveId = 1; break;
                case "cell3": moveId = 2; break;
                case "cell4": moveId = 3; break;
                case "cell5": moveId = 4; break;
                case "cell6": moveId = 5; break;
                case "cell7": moveId = 6; break;
                case "cell8": moveId = 7; break;
                case "cell9": moveId = 8; break;
                default: moveId = -1; break;
            }

            IO.Instance.Move(moveId.ToString());
        }
    }
}

