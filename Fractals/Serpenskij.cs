using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    internal class Serpenskij : Form1
    {
        public void Serp(PointF P1, PointF P2, PointF P3, long generation)
        {
            PointF Pg1 = new PointF(); PointF Pg2 = new PointF(); PointF Pg3 = new PointF();
            PointF[] Triangle = new PointF[2];
            if (generation > 0)
            {
                Pg1.X = (P1.X + P2.X) / 2;
                Pg1.Y = (P1.Y + P2.Y) / 2;
                Pg2.X = (P2.X + P3.X) / 2;
                Pg2.Y = (P2.Y + P3.Y) / 2;
                Pg3.X = (P3.X + P1.X) / 2;
                Pg3.Y = (P3.Y + P1.Y) / 2;
                Serp(P1, Pg1, Pg3, generation - 1);
                Serp(Pg1, P2, Pg2, generation - 1);
                Serp(Pg3, Pg2, P3, generation - 1);
            }
            else
            {
                Triangle[0].X = P1.X * lx;
                Triangle[0].Y = lx - P1.Y * lx;
                Triangle[1].X = P2.X * lx;
                Triangle[1].Y = lx - P2.Y * lx;
                Triangle[2].X = P3.X * lx;
                Triangle[2].Y = lx - P3.Y * lx;
                g.DrawPolygon(Pen1, Triangle);
            }
        }//Serp
    }
}
