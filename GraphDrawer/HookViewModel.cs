using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class HookViewModel : BaseViewModel
    {
        public string Type { get; init; }

        public HookViewModel(string type)
        {
            Type = type;
        }
    }
}
