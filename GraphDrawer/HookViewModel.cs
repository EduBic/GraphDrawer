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
        public double X { get; set; }
        public double Y { get; set; }

        public double AbsX { get; set; }
        public double AbsY { get; set; }

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
    }
}
