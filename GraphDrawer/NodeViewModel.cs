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
            
            int inCounter = 1;
            var distanceBtwInHooks = NodeCanvas.CIRCLE_RADIUS * 2 / (1 + connectionConfigs.Select(x => x.NumInput).Sum());

            int outCounter = 1;
            var distanceBtwOutHooks = NodeCanvas.CIRCLE_RADIUS * 2 / (1 + connectionConfigs.Select(x => x.NumOutput).Sum());

            foreach (var connConfig in connectionConfigs)
            {
                var inHooks = new List<HookViewModel>();
                for (int k = 0; k < connConfig.NumInput; k++)
                {
                    var xHookCoord = -1 * (NodeCanvas.CIRCLE_RADIUS + NodeCanvas.DIST_FROM_ELLIPSE_INP);
                    var yHookCoord = distanceBtwInHooks * inCounter - NodeCanvas.CIRCLE_RADIUS;
                    inHooks.Add(new HookViewModel(connConfig.Type, xHookCoord, yHookCoord, -1, origin));
                    inCounter++;
                }

                var outHooks = new List<HookViewModel>();
                for (int k = 0; k < connConfig.NumOutput; k++)
                {
                    var xHookCoord = 1 * (NodeCanvas.CIRCLE_RADIUS + NodeCanvas.DIST_FROM_ELLIPSE_INP);
                    var yHookCoord = distanceBtwOutHooks * outCounter - NodeCanvas.CIRCLE_RADIUS;
                    outHooks.Add(new HookViewModel(connConfig.Type, xHookCoord, yHookCoord, 1, origin));
                    outCounter++;
                }

                var newConn = new ConnectionViewModel(inHooks, outHooks);
                Connections.Add(newConn);
            }
        }
    }
}
