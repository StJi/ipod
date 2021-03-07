using System;
using System.Drawing;
using System.Windows.Forms;

namespace ipod
{
    class Ring
    {
        Font drawFont = new Font("San Francisco", 9, FontStyle.Bold);
        private Graphics g;
        private int bar, stBar;        // Управление эмуляцией трека
        private int style;             // Управление стилем меню
        int selectIndex;
        int x, xs, dx;                // Координаты x 
        int y, ys, dy;                // Координаты y
        int h, hs, dw, dh, h1;        // Размеры
        int angle;            // Угол начала отсчёта для рисования кнопки
        int r, rs;             // Радиус круга
        Point center;         // Координаты центра окружности
        Point reload;         // Координаты указателя мыши относительно центра окружности
        private int[] strings = { 30, 45, 60, 75, 90, 105, 120, 135, 150, 165 };
        private string[] main = new string[] { "Музыка", "Настройки", "Выход" };
        private string[] setting = { "Назад", "Стиль 1", "Стиль 2" };
        private string[,] songs =                   // Двумерный массив композиций, думал о возможности добавления меню авторов песен, в которых содержутся только их треки 
        {
            {"Linkin Park", "Numb", "In the End","Faint" },
            {"Motionless in White", "Another Life", "Voices" ,"catharsis"},
            {"My Chemical Romance", "Helena", "The Sharpest Lives", "House of Wolves" },
            {"The Rasmus", "Paradise", "In The Shadows", "Night After Night"}
        };
        private string[] songsMenu = new string[13]; // одномерный массив композиций
        private string[] selectList;   // Открытое меню
        private string menus;      // Переменная открытого меню
        private int size; // Количество элементов выбранного меню
        private void SongsMenuVoid() // Функция перевода двумерного массива в одномерный, которая подписывает каждый трек исполнителем
        {
            int l = 0;
            songsMenu[0] = "Назад";
            string temp;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    l++;
                    temp = songs[i, 0] + " - " + songs[i, j];
                    songsMenu[l] = temp;

                }

            }
        }


        public Ring(int p5, int p6, int p7, int p8, int p1, int p2, int p3, int p4, Graphics gr)
        {
            Timer timer1 = new Timer();                   // Таймер для эмуляции плеера
            timer1.Enabled = true;
            timer1.Interval = 300;
            timer1.Tick += new EventHandler(Bar);
            x = p1;
            y = p2;
            h = p3;
            angle = p4;
            xs = x + 63;
            ys = y + 63;
            hs = h / 4 + h / 8;
            rs = hs / 2;
            r = h / 2;
            center.X = x + r;
            center.Y = y + r;
            dx = p5;
            dy = p6;
            dh = p7;
            dw = p8;
            h1 = 15;
            g = gr;
            selectList = main;
            menus = "main";
            size = 3;
            selectIndex = 0;
            style = 0;
            stBar = 1;
        }

        internal void Drawp()
        {
            g.FillEllipse(Brushes.Silver, x, y, h, h);
            g.DrawEllipse(Pens.Black, x, y, h, h);
            for (angle = -135; angle < 135; angle += 90)
            {
                g.DrawPie(Pens.Black, x, y, h, h, angle, 90);
            }
            g.FillEllipse(Brushes.Silver, xs, ys, hs, hs);
            g.DrawEllipse(Pens.Black, xs, ys, hs, hs);
            Text();
            DrawD();
            DrawList(main);
        }


        public bool StateC(Point click, int radius)
        {
            reload.X = Math.Abs(click.X - center.X);
            reload.Y = Math.Abs(click.Y - center.Y);
            if (Math.Sqrt(Math.Pow(reload.X, 2) + Math.Pow(reload.Y, 2)) < radius) return true; // проверка нахождения курсора в окружности
            return false;
        }

        public string State(Point click) // проверка нахождения курсора в областях кнопок
        {
            reload.X = click.X - center.X;
            reload.Y = click.Y - center.Y;
            if (reload.X > -reload.Y && -reload.X > -reload.Y) return "Play";
            if (reload.X > reload.Y && reload.X > -reload.Y) return "Next";
            if (reload.X > reload.Y && -reload.X > reload.Y) return "Menu";
            if (-reload.X > -reload.Y && -reload.X > reload.Y) return "Early";

            return "";
        }

        internal void Text() // подписи кнопок
        {
            g.DrawString("Next", SystemFonts.DefaultFont, Brushes.Black, x + 155, y + 95);
            g.DrawString("Menu", SystemFonts.DefaultFont, Brushes.Black, x + 84, y + 25);
            g.DrawString("Early", SystemFonts.DefaultFont, Brushes.Black, x + 20, y + 95);
            g.DrawString("Play/Pause", SystemFonts.DefaultFont, Brushes.Black, x + 75, y + 160);

        }

        internal bool Buttons(Point click)
        {

            if (StateC(click, r))
            {
                if (StateC(click, rs))
                {
                    switch (menus)
                    {

                        case "main":
                            switch (selectIndex)
                            {
                                case 0:
                                    selectIndex = 0;
                                    size = 13;
                                    SongsMenuVoid();
                                    menus = "songsMenu";
                                    DrawList(songsMenu);
                                    break;
                                case 1:
                                    size = 3;
                                    selectIndex = 0;
                                    menus = "setting";
                                    DrawList(setting);
                                    break;
                                case 2:
                                    return true;
                            }
                            break;
                        case "songsMenu":
                            switch (selectIndex)
                            {
                                case 0:
                                    size = 3;
                                    selectIndex = 0;
                                    menus = "main";
                                    DrawList(main);
                                    break;
                                default:
                                    size = 13;
                                    menus = "player";
                                    Player(selectIndex);
                                    break;
                            }
                            break;
                        case "setting":
                            switch (selectIndex)
                            {
                                case 0:
                                    size = 3;
                                    selectIndex = 0;
                                    menus = "main";
                                    DrawList(main);
                                    break;
                                case 1:
                                    size = 3;
                                    style = 1;
                                    selectIndex = 0;
                                    menus = "main";
                                    DrawList(main);
                                    break;
                                case 2:
                                    size = 3;
                                    style = 2;
                                    selectIndex = 0;
                                    menus = "main";
                                    DrawList(main);
                                    break;
                            }
                            break;
                        case "player":
                            selectIndex = 0;
                            size = 13;
                            SongsMenuVoid();
                            menus = "songsMenu";
                            DrawList(songsMenu);
                            break;
                    }
                }
                else
                {
                    switch (State(click))
                    {
                        case "Play":
                            if (menus == "player")
                            {
                                stBar = stBar == 0 ? 1 : 0;
                            }

                            break;
                        case "Next":
                            selectIndex++;
                            if (selectIndex == size) selectIndex = 0;
                            if (menus == "player")
                            {
                                if (selectIndex == 0) selectIndex = 1;
                                Player(selectIndex);
                            }
                            else DrawList(selectList);
                            break;
                        case "Menu":
                            stBar = 1;
                            size = 3;
                            menus = "main";
                            selectIndex = 0;
                            DrawList(main);
                            break;
                        case "Early":
                            selectIndex--;
                            if (selectIndex == -1) selectIndex = size - 1;
                            if (menus == "player")
                            {
                                if (selectIndex == 0) selectIndex = size - 1;
                                Player(selectIndex);
                            }
                            else DrawList(selectList);
                            break;
                    }
                }
            }

            return false;
        }

        private void Player(int song)
        {
            g.FillRectangle(Brushes.Gray, dx + 1, 30, dw - 1, dh - 17);
            Rectangle rect2 = new Rectangle(40, 70, 200, 30);
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            TextRenderer.DrawText(g, songsMenu[song], drawFont, rect2, Color.Black, flags);
            bar = 50;
            stBar = 0;
        }

        private void Bar(object sender, EventArgs e)
        {
            if (stBar == 0)
            {
                if (bar <= 225)
                {
                    bar++;
                    g.DrawLine(Pens.DarkBlue, bar, 130, bar + 1, 130);
                    g.DrawLine(Pens.DarkBlue, bar, 131, bar + 1, 131);
                }
            }
        }

        internal void DrawD()                      // Создание верхнего меню
        {

            g.FillRectangle(Brushes.DimGray, dx + 1, 13, dw - 1, h1 + 2);
            g.DrawRectangle(Pens.DimGray, dx, dy, dw, dh + 1);
            g.DrawString("iPod", drawFont, Brushes.Black, dx + 115, dy + 2);
        }

        public void DrawList(string[] list)           // Фон меню и само меню
        {
            selectList = list;
            int i = 45;
            g.FillRectangle(Brushes.WhiteSmoke, dx + 1, 30, dw - 1, dh - 2 - h1);
            if (style == 1)
            {
                if (selectIndex % 2 != 0 && selectIndex > 8) i = 30;
                for (; i <= 150; i += 30)
                {
                    g.FillRectangle(Brushes.DarkGray, dx + 1, i, dw - 1, h1);
                }
            }
            g.FillRectangle(Brushes.WhiteSmoke, dx + 1, 150, dw - 1, h1);


            int str = 0;
            if (selectIndex > 8) str = (selectIndex % 8);
            for (int k = 0; k < 9; k++, str++)
            {
                if (str < size)
                {
                    g.DrawString(list[str], drawFont, Brushes.Black, dx + 1, strings[k] - 1);
                }
            }
            Cursor(list);
        }

        public void Cursor(string[] list)                                      // Курсор
        {
            int str = selectIndex;
            if (selectIndex > 8) str = 8;
            g.FillRectangle(Brushes.DarkBlue, dx + 1, strings[str], dw - 1, h1);
            g.DrawString(">", drawFont, Brushes.WhiteSmoke, 255, strings[str] - 1);
            g.DrawString(list[selectIndex], drawFont, Brushes.WhiteSmoke, dx + 2, strings[str] - 1);
        }
    }
}

