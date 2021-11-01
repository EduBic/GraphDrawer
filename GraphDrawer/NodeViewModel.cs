using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphDrawer
{
    public class NodeViewModel : BaseViewModel
    {
        public Point Origin { get; init; }
        public List<ConnectionViewModel> Connections { get; }

        public NodeViewModel(Point origin, List<ConnectionConfigViewModel> connectionConfigs)
        {
            Origin = origin;

            Connections = new List<ConnectionViewModel>(connectionConfigs.Count);
            foreach (var connConfig in connectionConfigs)
            {
                var inHooks = Enumerable.Range(0, connConfig.NumInput)
                    .Select(i => new HookViewModel(connConfig.Type))
                    .ToList();
                var outHooks = Enumerable.Range(0, connConfig.NumOutput)
                    .Select(i => new HookViewModel(connConfig.Type))
                    .ToList();

                var newConn = new ConnectionViewModel(inHooks, outHooks);
                Connections.Add(newConn);
            }
        }
    }
}
