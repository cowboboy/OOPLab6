﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal abstract class Figure
    {
        public bool isActive { get; set; }
        protected Pen selectedPen, standartPen;
        public Color currentColor { get; set; } = Color.White;
        public abstract void myPaint(in Graphics g);
        public abstract bool intersects(Point coords);
        public abstract void changeColor(Color newColor);
        public abstract void move(Point direction);
    }
}
