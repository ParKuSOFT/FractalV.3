using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals
{
    internal class Vitten : Form1
    {
        public void Vittenn()
        {
            long I; long J;
            N = Convert.ToInt32(textBox2.Text);
            particleCount = Convert.ToInt32(textBox3.Text);
            Array.Resize(ref particlex, particleCount);
            Array.Resize(ref particley, particleCount);
            viewFractal = true;



            if ((N % 2) == 0)
            {
                N++;
                textBox2.Text = Convert.ToString(N);
            }
            Arr1 = new byte[N + 1, N + 1];
            for (I = 0; I <= (N + 1); I++)
            {
                for (J = 0; J <= (N + 1); J++)
                {
                    Arr1[I, J] = 0;
                }
            }
            dx = Convert.ToInt32(pictureBox1.DisplayRectangle.Width / N);
            for (I = 0; I <= N; I++)
            {
                g.DrawLine(Pen1, I * dx, 0, I * dx, lx);
                g.DrawLine(Pen1, 0, I * dx, lx, I * dx);
            }
            centre = N / 2 + 1;
            Arr1[centre, centre] = 5;
            PaintCell(centre, centre);
            for (I = 1; I <= particleCount; I++)
            {
                iterCount = 0;
                do
                {
                    rndx = Convert.ToInt32(N * rnd.Next()) + 1;
                    rndx = Convert.ToInt32(N * rnd.Next()) + 1;
                    iterCount++;
                }
                while (Arr1[rndx, rndy] != 0 || iterCount <= 0);
                particlex[I] = rndx;
                particley[I] = rndy;
                Arr1[rndx, rndy] = 1;
                PaintCell(rndx, rndy);
            }
            pictureBox1.Image = Bitmap1;
        }//Vitten
    }
}
