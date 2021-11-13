using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphDrawer
{
    public class HookViewModel : BaseViewModel
    {
        private double absX;
        private double absY;

        // TODO: these change when we need to move the hook in the reference system of node canvas
        public double X { get; init; }
        public double Y { get; init; }

        public double AbsX
        {
            get { return absX; }
            set { SetProperty(ref absX, value); }
        }
        public double AbsY
        {
            get { return absY; }
            set { SetProperty(ref absY, value); }
        }

        // input = -1, output = 1
        public int Direction { get; set; }

        public string Type { get; init; }

        public HookViewModel(string type, double x, double y, int direction, Point origin)
        {
            Type = type;
            X = x;
            Y = y;
            Direction = direction;
            AbsX = x + origin.X;
            AbsY = y + origin.Y;
        }

        internal void Translate(double translateX, double translateY)
        {
            AbsX += translateX;
            AbsY += translateY;
        }
    }
}
