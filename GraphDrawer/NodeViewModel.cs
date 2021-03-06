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
        private double x;
        private double y;

        public double X 
        { 
            get { return x; }
            set { SetProperty(ref x, value); }
        }
        public double Y
        {
            get { return y; }
            set { SetProperty(ref y, value); }
        }

        public List<ConnectionViewModel> Connections { get; }

        public NodeViewModel(Point origin, List<ConnectionConfigViewModel> connectionConfigs)
        {
            X = origin.X;
            Y = origin.Y;

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

        internal void Translate(double translateX, double translateY)
        {
            X += translateX;
            Y += translateY;

            foreach (var conn in Connections)
            {
                foreach (var hook in conn.Inputs.Concat(conn.Outputs))
                {
                    hook.Translate(translateX, translateY);
                }
            }
        }

        public void ChangePosition(Point newOrigin)
        {
            var translateX = X - newOrigin.X;
            var translateY = Y - newOrigin.Y;

            X = newOrigin.X;
            Y = newOrigin.Y;

            foreach (var conn in Connections)
            {
                foreach (var hook in conn.Inputs.Concat(conn.Outputs))
                {
                    hook.Translate(-translateX, -translateY);
                }
            }
        }
    }
}
