using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public interface ICanvasState
    {
        public void Attach();
        public void Detach();
    }
}
