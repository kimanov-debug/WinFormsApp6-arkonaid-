using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int speedX = 6;
        int speedY = 6;
        int score = 0;
        bool gameOver = false;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            score = 0;
            gameOver = false;
            speedX = rnd.Next(0, 2) == 0 ? 6 : -6;
            speedY = 6;

            pictureBox1.Left = (ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = 40;
            panel1.Left = (ClientSize.Width - panel1.Width) / 2;

            lblScore.Text = "Xal: 0";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += speedX;
            pictureBox1.Top += speedY;

            if (pictureBox1.Left <= 0)
            {
                pictureBox1.Left = 0;
                speedX = Math.Abs(speedX);
            }
            if (pictureBox1.Right >= ClientSize.Width)
            {
                pictureBox1.Left = ClientSize.Width - pictureBox1.Width;
                speedX = -Math.Abs(speedX);
            }
            if (pictureBox1.Top <= 0)
            {
                pictureBox1.Top = 0;
                speedY = Math.Abs(speedY);
            }

            if (speedY > 0 && pictureBox1.Bounds.IntersectsWith(panel1.Bounds))
            {
                pictureBox1.Top = panel1.Top - pictureBox1.Height;
                speedY = -Math.Abs(speedY);

                int ballCenter = pictureBox1.Left + pictureBox1.Width / 2;
                int panelCenter = panel1.Left + panel1.Width / 2;
                speedX = (ballCenter - panelCenter) / 8;
                if (speedX == 0) speedX = rnd.Next(0, 2) == 0 ? 2 : -2;

                score++;
                lblScore.Text = "Xal: " + score;

                if (score % 5 == 0)
                    speedY -= 1;
            }

            if (pictureBox1.Top > ClientSize.Height)
            {
                timer1.Stop();
                gameOver = true;
                lblScore.Text = "Oyun bitdi! Xal: " + score + "  (R - yenidən)";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                panel1.Left -= 20;
                if (panel1.Left < 0) panel1.Left = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                panel1.Left += 20;
                if (panel1.Right > ClientSize.Width)
                    panel1.Left = ClientSize.Width - panel1.Width;
            }
            if (e.KeyCode == Keys.R && gameOver)
            {
                StartGame();
            }
        }
    }
}