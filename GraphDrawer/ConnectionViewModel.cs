using System.Collections.Generic;

namespace GraphDrawer
{
    public class ConnectionViewModel
    {
        public List<HookViewModel> Inputs { get; }
        public List<HookViewModel> Outputs { get; }

        public ConnectionViewModel(List<HookViewModel> inputs, List<HookViewModel> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
        }

    }
}