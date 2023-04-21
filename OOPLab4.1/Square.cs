using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class Square : Figure
    {
        public int x { get; set; }
        public int y { get; set; }
        public int sideLength { get; set; }

        Pen selectedPen, standartPen;

        public Square(int x, int y, int sideLength = 50)
        {
            this.x = x;
            this.y = y;
            this.sideLength = sideLength;
            standartPen = new Pen(Color.Black);
            selectedPen = new Pen(Color.Red);
            isActive = true;
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawRectangle(selectedPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
            }
            else
            {
                g.DrawRectangle(standartPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
            }
        }

        public override bool intersects(Point coords)
        {
            if (coords.X >= x && coords.X <= x + sideLength && coords.Y >= y && coords.Y <= y + sideLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
