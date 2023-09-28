using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Security;

namespace Fractals
{
    public partial class Form1 : Form
    {
        public float fractalDimension = Convert.ToSingle(Math.Log(4) * Math.Log(3));
        public long generation;
        public int lx;
        public int ly;
        public long centre;
        public bool viewFractal;
        public int N;
        public long numberOfSplits;
        public byte[,] Arr1;
        public float dx;
        public float summ;
        public long rndx;
        public long rndy;
        public long rndxx;
        public long rndyy;
        public float s;
        public int nn;
        public long[] particlex;
        public long[] particley;
        public int particleCount;
        public long particleI;
        public long iterCount;
        public Bitmap Bitmap1;
        public Graphics g;
        public Pen Pen1 = new Pen(Brushes.Black);
        public Brush Brush1;
        public float db;
        public Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lx = pictureBox1.DisplayRectangle.Width;
            db = pictureBox1.Width - lx;
            ly = lx;
            Bitmap1 = new Bitmap(lx, ly);
            g = Graphics.FromImage(Bitmap1);
        }//void

        private void button1_Click(object sender, EventArgs e)
        {
            generation = Convert.ToInt64(textBox1.Text);
            PointF P1 = new PointF(0, 0);//начальные координаты или вершины треугольников
            PointF P2 = new PointF(1, 0);
            PointF P3 = new PointF(1, 0);
            Color color = Color.White;
            g.Clear(color);
            if (radioButton1.Checked)
            {
                Kochh koch = new Kochh();
                koch.Koh(P1, P2, 1);
            }
            else if (radioButton2.Checked)
            {
                Serpenskij serp = new Serpenskij();
                P2.X = (float)0.5;
                P2.Y = (float)Math.Sqrt(0.75);
                serp.Serp(P1, P2, P3, generation);
            }
            else
            {
                Vitten vit = new Vitten();
                vit.Vittenn();
            }
            pictureBox1.Image = Bitmap1;
        }//void

        private void button2_Click(object sender, EventArgs e)
        {
            Color color = Color.White;
            g.Clear(color);
            timer1.Enabled = true;
        }//void

        private void button5_Click(object sender, EventArgs e)
        {
            if (viewFractal = true)
            {
                viewFractal = false;
            }
            else
            {
                viewFractal = true;
                RefreshPic();
            }
        }//void

        private void button3_Click(object sender, EventArgs e)
        {
            long I; long J;
            float sngi; float sngj;
            double rmax; double r; double rgir; double df;
            long newn; long new1; long partCount;
            partCount = 0;
            rmax = 0;
            for (I = 1; I <= N; I++)
            {
                sngi = Convert.ToSingle(I);
                for (J = 1; J <= N; J++)
                {
                    sngj = Convert.ToSingle(J);
                    if (Arr1[I, J] > 8)
                    {
                        r = Math.Sqrt((sngj - centre) * (sngj - centre) + (sngi - centre) * (sngi - centre));
                        if (r > rmax)
                        {
                            rmax = r;
                        }
                        partCount++;

                    }//if
                }//for
            }//for
            if (rmax > N / 2) ; rmax = N / 2;
            if (rmax < 1) ; rmax = N / 2;
            rgir = 0;
            newn = centre + (long)rmax - 1;
            new1 = centre - (long)rmax + 1;
            partCount = 0;
            for (I = new1; I <= newn; I++)
            {
                sngi = Convert.ToSingle(I);
                for (J = new1; J <= newn; J++)
                {
                    sngj = Convert.ToSingle(J);
                    if (Arr1[I, J] > 4)
                    {
                        rgir += (sngj - centre) * (sngj - centre) + (sngi - centre) * (sngi - centre);
                        partCount++;
                    }//if
                }//for
            }//for
            rgir = Math.Sqrt(rgir / partCount);
            df = Math.Log(Convert.ToDouble(partCount)) / Math.Log(Convert.ToDouble(2 * rmax));
            textBox5.Text = Convert.ToString(rgir);
            textBox4.Text = Convert.ToString(df);
            textBox6.Text = Convert.ToString(partCount / (newn - new1) ^ 2);
        }//void

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }//void

        private void timer1_Tick(object sender, EventArgs e)
        {
            long particleI; long J;
            for (particleI = 1; particleI <= particleCount; particleI++)
            {
                rndx = particlex[particleI];
                rndy = particley[particleI];
                s = Arr1[rndx - 1, rndy] + Arr1[rndx + 1, rndy] + Arr1[rndx, rndy - 1] + Arr1[rndx, rndy + 1];
                if (s > 4 && rndx < N && rndx > 1 && rndy < N && rndy > 1)
                {
                    Arr1[rndx, rndy] = 5;
                    particleCount = particleCount - 1;
                    for (J = particleI; J <= particleCount; J++)
                    {
                        particlex[J] = particlex[J + 1];
                        particley[J] = particley[J + 1];
                    }
                    if (viewFractal = true) ; PaintCell(rndx, rndy);
                    if (particleCount == 0)
                    {
                        timer1.Enabled = false;
                        return;
                    }
                }
                else
                {
                    Random rnd = new Random();
                    rndx = particlex[particleI];
                    rndy = particley[particleI];
                    rndxx = rndx;
                    rndyy = rndy;
                    nn = Convert.ToInt32(4 * rnd.Next());
                    if (nn == 0) { rndx--; }
                    else
                    {
                        if (nn == 1) { rndx++; }
                        else
                        {
                            if (nn == 2) { rndy--; }
                            else { if (nn == 3) { rndy++; } }
                        }
                    }
                    if (rndx == N + 1) ; rndx = 1;
                    if (rndx == 0) ; rndx = N;
                    if (rndy == N + 1) ; rndy = 1;
                    if (rndy == 0) ; rndy = N;
                    Arr1[rndxx, rndyy] = 0;
                    PaintCell(rndxx, rndyy);
                    Arr1[rndx, rndy] = 1;
                    PaintCell(rndx, rndy);
                    particlex[particleI] = rndx;
                    particley[particleI] = rndy;
                }
            }//for
            Color color = Color.White;
            if (viewFractal = true) { pictureBox1.Image = Bitmap1; }
            else g.Clear(color);
        }//void

        internal void RefreshPic()
        {
            long i; long j;
            for (i = 1; i <= N; i++)
            {
                for (j = 1; j <= N; j++)
                {
                    PaintCell(i, j);
                }
            }
            pictureBox1.Image = Bitmap1;
        } //void

        internal void PaintCell(long indx, long indy)
        {
            if (Arr1[indx, indy] == 1) { Brush1 = Brushes.DarkOliveGreen; }
            else if (Arr1[indx, indy] == 5) { Brush1 = Brushes.DarkMagenta; }
            else { Brush1 = Brushes.White; }
            g.FillRectangle(Brush1, (indx - 1) * dx, (indy - 1) * dx, dx, dx);
        }//void
    }
}