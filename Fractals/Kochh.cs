using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    internal class Kochh : Form1
    {
        public void Koh(PointF P1, PointF P2, long generationDef)
        {
            PointF Pg1; PointF Pg2 = new PointF(); PointF Pg3 = new PointF(); PointF Pg4 = new PointF(); PointF Pg5; PointF ScrCoord1 = new PointF(); PointF ScrCoord2 = new PointF();
            float lenZveno = (1 / 3) ^ generationDef;
            if (generationDef > generation)
            {
                Pg1 = P1;
                Pg2.X = (P2.X - P1.X) / 3 + P1.X;
                Pg2.Y = (P2.Y - P1.Y) / 3 + P1.Y;
                Pg3.X = (P2.X + P1.X) / 2 - lenZveno * (float)Math.Sqrt(0.75) * (P2.Y - P1.Y) / ((1 / 3) ^ (generationDef - 1));
                Pg3.Y = (P2.Y + P1.Y) / 2 + lenZveno * (float)Math.Sqrt(0.75) * (P2.X - P1.X) / ((1 / 3) ^ (generationDef - 1));
                Pg4.X = 2 * (P2.X - P1.X) / 3 + P1.X;
                Pg4.Y = 2 * (P2.Y - P1.Y) / 3 + P1.Y;
                Pg5 = P2;
                Koh(Pg1, Pg2, generationDef - 1);
                Koh(Pg2, Pg3, generationDef - 1);
                Koh(Pg3, Pg4, generationDef - 1);
                Koh(Pg4, Pg5, generationDef - 1);
            }
            else
            {
                ScrCoord1.X = P1.X * lx;
                ScrCoord1.Y = lx / 2 - P1.Y * lx;
                ScrCoord2.X = P2.X * lx;
                ScrCoord2.Y = lx / 2 - P2.Y * lx;
                g.DrawLine(Pen1, ScrCoord1, ScrCoord2);
            }
        }//Koch
    }
}
