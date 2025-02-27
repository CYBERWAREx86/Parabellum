using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Parabellum
{
    public partial class AppDefault : Form
    {
        private bool isPTBR = false;
        private int red1, green1, blue1, red2, green2, blue2;
        private List<Star> stars;
        private Random rand;

        public AppDefault()
        {
            InitializeComponent();
            InitializeGradientColors();
            InitializeStars();
        }

        private void InitializeGradientColors()
        {
            rand = new Random();
            red1 = rand.Next(256);
            green1 = rand.Next(256);
            blue1 = rand.Next(256);

            red2 = rand.Next(256);
            green2 = rand.Next(256);
            blue2 = rand.Next(256);
        }

        private void InitializeStars()
        {
            stars = new List<Star>();
            for (int i = 0; i < 2200; i++)
            {
                stars.Add(new Star(rand.Next(ClientSize.Width), rand.Next(ClientSize.Height)));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(red1, green1, blue1),
                Color.FromArgb(red2, green2, blue2),
                45F))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            foreach (var star in stars)
            {
                star.Draw(e.Graphics);
            }
        }

        private void RgbTraductor_Tick(object sender, EventArgs e)
        {
            Button button = buttonTraductor;
            button.BackColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            button.ForeColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }

        private void UpdateGradiantColor_Tick(object sender, EventArgs e)
        {
            red1 = rand.Next(256);
            green1 = rand.Next(256);
            blue1 = rand.Next(256);

            red2 = rand.Next(256);
            green2 = rand.Next(256);
            blue2 = rand.Next(256);

            this.Invalidate();
        }

        private void buttonTraductor_Click(object sender, EventArgs e)
        {
            if (isPTBR)
            {
                buttonExitApp.Text = "NO. EXIT!!";
                buttonYesOps.Text = "YES";
                textBoxWarn.Text = "ATTENTION!\r\nTHIS SOFTWARE IS AN EMAIL WORM, THAT IS, IT CAN\r\nSPREAD THROUGH YOUR EMAIL, OVERWRITE THE\r\nBOOT SECTOR AND COMPLETELY DESTROY YOUR MACHINE!\r\n\r\nDO YOU WANT TO CONTINUE WITH EXECUTION?";
                this.Text = "GDI-EmailWorm.Win32.Parabellum.exe  -  Created by CYBERWARE";
            }
            else
            {
                buttonExitApp.Text = "NÃO. SAIR!!";
                buttonYesOps.Text = "SIM";
                textBoxWarn.Text = "ATENÇÃO!\r\nESTE SOFTWARE É UM WORM DE E-MAIL, OU SEJA,\r\nELE PODE SE ESPALHAR PELO SEU E-MAIL, E PODE\r\nSOBRESCREVER O SETOR DE BOOT ALEM DE DESTRUIR\r\nCOMPLETAMENTE SUA MÁQUINA!\r\n\r\nVOCÊ QUER CONTINUAR COM A EXECUÇÃO?";
                this.Text = "GDI-EmailWorm.Win32.Parabellum.exe  -  Criado por CYBERWARE";
            }

            isPTBR = !isPTBR;
        }

        private void buttonExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void buttonYesOps_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonYesOps_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Font = new Font(button.Font.FontFamily, button.Font.Size + 6, FontStyle.Bold);
        }

        private void buttonYesOps_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Font = new Font(button.Font.FontFamily, button.Font.Size - 6, FontStyle.Bold);
        }

        private void buttonExitApp_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Font = new Font(button.Font.FontFamily, button.Font.Size + 6, FontStyle.Bold);
        }

        private void Starss_Tick(object sender, EventArgs e)
        {
            foreach (var star in stars)
            {
                star.Move(rand);
            }
            Invalidate();
        }

        private void buttonExitApp_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Font = new Font(button.Font.FontFamily, button.Font.Size - 6, FontStyle.Bold);
        }

        private void textBoxWarn_MouseEnter(object sender, EventArgs e)
        {
            Label text = sender as Label;
            text.Font = new Font(text.Font.FontFamily, text.Font.Size + 2, FontStyle.Bold);
        }

        private void textBoxWarn_MouseLeave(object sender, EventArgs e)
        {
            Label text = sender as Label;
            text.Font = new Font(text.Font.FontFamily, text.Font.Size - 2, FontStyle.Bold);
        }

        private void AppDefault_Load(object sender, EventArgs e)
        {

        }

        public class Star
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Size { get; set; }
            public int SpeedX { get; set; }
            public int SpeedY { get; set; }

            public Star(int x, int y)
            {
                X = x;
                Y = y;
                Size = 4;
                SpeedX = 10;
                SpeedY = 10;
            }

            public void Draw(Graphics g)
            {
                g.FillEllipse(Brushes.White, X, Y, Size, Size);
            }

            public void Move(Random rand)
            {
                X += SpeedX;
                Y += SpeedY;

                if (X > 800) X = 0;
                if (Y > 600) Y = 0;
                if (X < 0) X = 800;
                if (Y < 0) Y = 600;
            }
        }
    }
}
