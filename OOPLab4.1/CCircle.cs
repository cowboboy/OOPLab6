using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class CCircle : Figure
    {
        public int x { get; set; }
        public int y { get; set; }
        public int radius { get; set; }

        Pen selectedPen, standartPen;

        public CCircle(int x, int y, int radius = 50)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            standartPen = new Pen(Color.Black);
            selectedPen = new Pen(Color.Red);
            isActive = true;
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawEllipse(selectedPen, x - radius, y - radius, radius * 2, radius * 2);
            } else
            {
                g.DrawEllipse(standartPen, x - radius, y - radius, radius * 2, radius * 2);
            }
        }

        public override bool intersects(Point coords)
        {
            if ((coords.X - x) * (coords.X - x) + (coords.Y - y) * (coords.Y - y) <= radius * radius)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
