using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Threading;

namespace L20200830_Bouncing_Circle_1
{
    public partial class Form1 : Form
    {

        private int x, y;
        private int velocityX, velocityY;
        private int radius;
        private bool go;


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           // go = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
                go = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {
                const int rightMargin = 21;
                const int bottomMargin = 39;
                while (this.go)
                {

                    x += velocityX;
                    y += velocityY;
                    if (x + radius + rightMargin > this.Width)
                    {
                        x = this.Width - radius - rightMargin;
                        velocityX *= -1;
                    }
                    else if (x - radius < 0)
                    {
                        x = this.radius;
                        velocityX *= -1;
                    }
                    if (y + radius + bottomMargin > this.Height)
                    {
                        y = this.Height - radius - bottomMargin;
                        velocityY *= -1;
                    }
                    else if (y - radius < 0)
                    {
                        y = this.radius;
                        velocityY *= -1;
                    }

                    Action action = () => {
                        this.Invalidate();
                        
                    };
                    Thread.Sleep(10);

                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                            //  if (go)
                            action();
                    });

                }

            });
            thread.Start();
        }

        public Form1()
        {
            InitializeComponent();
            x = 90; y = 50;
            velocityX = 7;
            velocityY = 3;
            radius = 10;
            go = true;
            
        }

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillCircle(Brushes.Black, x, y, radius);
            

        }
    }


    

    public static class MyExtensions
    {
        public static void FillCircle(this Graphics g, Brush brush, int x, int y, int radius)
        {
            g.FillPie(brush, x - radius, y - radius, radius * 2, radius * 2, 0, 360);
        }
    }

    
}
