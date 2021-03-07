using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipod
{
    class Display
    {
        private int x, y, h, w,h1;
        private Graphics gr;
        private int[] strings = {30, 45, 60, 75, 90, 105, 120, 135, 150};
        Font drawFont = new Font("San Francisco", 9, FontStyle.Bold);
        public Display(int p1, int p2, int p3, int p4, Graphics g)
        {
            x = p1;
            y = p2;
            h = p3;
            w = p4;
            h1 = 15;
            gr = g;
        }


        internal void DrawD()
        { 
            
            gr.FillRectangle(Brushes.DimGray, x + 1, 13, w - 1, h1+2);
            gr.DrawRectangle(Pens.DimGray, x, y, w, h+1);
            gr.DrawString("iPod",drawFont , Brushes.Black,x+115,y+2);
        }

        public void DrawList(string[] list)
        {
            for (int i = 45, j = 30,k=0; i <= 150; i += 30, j += 30,k+=2)
            {
                gr.FillRectangle(Brushes.DarkGray, x + 1, i, w - 1, h1);
                gr.FillRectangle(Brushes.WhiteSmoke, x + 1, j, w - 1, h1);
                gr.DrawString(list[k], drawFont, Brushes.Black, x + 1, j-1);
                gr.DrawString(list[k+1], drawFont, Brushes.Black, x + 1, i-1);
            }
            gr.FillRectangle(Brushes.WhiteSmoke, x + 1, 150, w - 1, h1);
        }

        public void Cursor(int select, string[] list)                                      // Работает
        {
            DrawList(list);
            gr.FillRectangle(Brushes.DarkBlue,x+1,strings[select],w-1,h1);
            gr.DrawString(">", drawFont, Brushes.WhiteSmoke, 255, strings[select]-1);
            gr.DrawString(list[select], drawFont, Brushes.WhiteSmoke, x+2, strings[select]-1);
        }

        public void ExitMenu()
        {
            
        }

        public void MainMenu()
        {

        }
    }
}
