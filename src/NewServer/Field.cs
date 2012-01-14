using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewServerApi
{
   
    public class Field
    {
        int[] f = new int[9]; // game field
        //-1  -пустая клетка
        //0  - фигура 1 клиента, т.е. твой
        //1  - фигура 2 клиента
        public Field()
        {
            for (int i = 0; i < 9;i++ )
                f[i] = -1;
        }
        public int CheckWinInt()
        {
            /*return //проверка всех возможных выгрышных комбинаций
                (f[0] == f[1] && f[1] == f[2] && f[2] != -1) ||
                (f[3] == f[4] && f[4] == f[5] && f[5] != -1) ||
                (f[6] == f[7] && f[7] == f[8] && f[8] != -1) ||
                (f[0] == f[3] && f[3] == f[6] && f[0] != -1) ||
                (f[1] == f[4] && f[4] == f[7] && f[1] != -1) ||
                (f[2] == f[5] && f[5] == f[8] && f[2] != -1) ||
                (f[0] == f[4] && f[4] == f[8] && f[4] != -1) ||
                (f[2] == f[4] && f[4] == f[6] && f[4] != -1);
        
             */
            if (f[0] == f[1] && f[1] == f[2] && f[2] != -1) return 1;
            if (f[3] == f[4] && f[4] == f[5] && f[5] != -1) return 2;
            if (f[6] == f[7] && f[7] == f[8] && f[8] != -1) return 3;
            if (f[0] == f[3] && f[3] == f[6] && f[0] != -1) return 4;
            if (f[1] == f[4] && f[4] == f[7] && f[1] != -1) return 5;
            if (f[2] == f[5] && f[5] == f[8] && f[2] != -1) return 6;
            if (f[0] == f[4] && f[4] == f[8] && f[4] != -1) return 7;
            if (f[2] == f[4] && f[4] == f[6] && f[4] != -1) return 8;
            return 0;
        }
        public bool CheckWinBool()
        {
            return //проверка всех возможных выгрышных комбинаций
                (f[0] == f[1] && f[1] == f[2] && f[2] != -1) ||
                (f[3] == f[4] && f[4] == f[5] && f[5] != -1) ||
                (f[6] == f[7] && f[7] == f[8] && f[8] != -1) ||
                (f[0] == f[3] && f[3] == f[6] && f[0] != -1) ||
                (f[1] == f[4] && f[4] == f[7] && f[1] != -1) ||
                (f[2] == f[5] && f[5] == f[8] && f[2] != -1) ||
                (f[0] == f[4] && f[4] == f[8] && f[4] != -1) ||
                (f[2] == f[4] && f[4] == f[6] && f[4] != -1);
        
       
        }
        public bool MakeMove(int id)
        {
            if (f[id] == -1)
            {
                f[id] = 0;
                return true;
            }
            return false;
        }

        public bool MakeMovePair(int id)
        {
            if (f[id] == -1)
            {
                f[id] = 1;
                return true;
            }
            return false;
        }
        public int Get(int id)
        {
            return f[id];
        }

    }
}
