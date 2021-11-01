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
        public HookViewModel Start { get; init; }

        public HookViewModel End { get; init; }

        public EdgeViewModel(HookViewModel start, HookViewModel end)
        {
            (Start, End) = (start, end);
        }
    }
}
