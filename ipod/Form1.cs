using System;
using System.Drawing;
using System.Windows.Forms;
namespace ipod
{
    public partial class Form1 : Form
    {
        Point click;                                // Позиция клика мыши
        private Ring enter;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            Graphics g = this.CreateGraphics();
            enter = new Ring(12, 12, 152, 260, 40, 180, 200, 90, g);
        }


        private void Form1_Paint(object sender, PaintEventArgs e) // Создание элементов
        {
            enter.Drawp();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e) // Нажатие мыши
        {
            click = e.Location;
            if (enter.Buttons(click)) Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}