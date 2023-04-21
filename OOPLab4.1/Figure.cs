using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal abstract class Figure
    {
        public bool isActive { get; set; }
        public abstract void myPaint(in Graphics g);
        public abstract bool intersects(Point coords);
    }
}
