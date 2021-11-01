using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphDrawer
{
    public class EdgeViewModel : BaseViewModel
    {
        public Point Start { get; init; }

        public Point End { get; init; }

        public EdgeViewModel(Point start, Point end)
        {
            (Start, End) = (start, end);
        }
    }
}
