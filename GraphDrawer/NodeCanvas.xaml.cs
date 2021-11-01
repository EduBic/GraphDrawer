using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphDrawer
{
    /// <summary>
    /// Interaction logic for NodeCanvas.xaml
    /// </summary>
    public partial class NodeCanvas : Canvas
    {
        internal const double CIRCLE_RADIUS = 20;
        internal const double HOOK_RADIUS = 6;
        internal const double HOOK_DIAMETER = HOOK_RADIUS * 2;
        internal const double DIST_FROM_ELLIPSE_INP = 10.0;
        internal const double DIST_FROM_ELLIPSE_OUT = 12.0;

        public Ellipse Circle { get; }

        public List<(Line, Ellipse)> InpHooks { get; }
        public List<(Line, Ellipse)> OutHooks { get; }


        public NodeCanvas(NodeViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            // set global coordinates
            SetLeft(this, vm.Origin.X);
            SetTop(this, vm.Origin.Y);

            this.Width = CIRCLE_RADIUS * 4;
            this.Height = CIRCLE_RADIUS * 4;


            // draw main circle
            Circle = new Ellipse()
            {
                Width = CIRCLE_RADIUS * 2,
                Height = CIRCLE_RADIUS * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2.0,
                Fill = Brushes.Silver
            };

            SetLeft(Circle, -CIRCLE_RADIUS);
            SetTop(Circle, -CIRCLE_RADIUS);

            Children.Add(Circle);

            // draw connection hooks
            var inputs = vm.Connections.SelectMany(conn => conn.Inputs).ToList();
            InpHooks = DrawHooks(inputs, -1);

            foreach (var hook in InpHooks)
            {
                Children.Add(hook.Item1);
                Children.Add(hook.Item2);
            }

            var outputs = vm.Connections.SelectMany(conn => conn.Outputs).ToList();
            OutHooks = DrawHooks(outputs, 1);

            foreach (var hook in OutHooks)
            {
                Children.Add(hook.Item1);
                Children.Add(hook.Item2);
            }
        }

        // direction = 1 => RIGHT, -1 => LEFT
        private static List<(Line, Ellipse)> DrawHooks(List<HookViewModel> hooks, int direction)
        {
            var res = new List<(Line, Ellipse)>();
            var distanceBtwHooks = CIRCLE_RADIUS * 2 / (1 + hooks.Count);
            int counter = 1;
            foreach (var hookVm in hooks)
            {
                var xHookCoord = direction * (CIRCLE_RADIUS + DIST_FROM_ELLIPSE_OUT);

                var yHookCoord = distanceBtwHooks * counter - CIRCLE_RADIUS;
                var hook = DrawHook(new Point(xHookCoord, yHookCoord), direction);
                res.Add(hook);
                counter++;
            }

            return res;
        }

        // direction = 1 => RIGHT, -1 => LEFT
        private static (Line, Ellipse) DrawHook(Point linePointExternal, int direction)
        {
            var origin = new Point(0, 0);
            var linePointToEllipse = new Point(
                origin.X + direction * Math.Sqrt(Math.Pow(CIRCLE_RADIUS, 2.0) - Math.Pow(linePointExternal.Y - origin.Y, 2.0)),
                linePointExternal.Y);

            var line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = linePointExternal.X - direction * HOOK_RADIUS,
                Y1 = linePointExternal.Y,
                X2 = linePointToEllipse.X,
                Y2 = linePointToEllipse.Y,
            };
            SetLeft(line, 0);
            SetTop(line, 0);

            var hookView = new Ellipse()
            {
                Width = HOOK_DIAMETER,
                Height = HOOK_DIAMETER,
                Stroke = Brushes.DarkRed, // FlowTypeToColor(flowType),
                StrokeThickness = direction == 1 ? 0.0 : 2.0,
                Fill = direction == 1 ? Brushes.DarkRed : Brushes.Gainsboro // FlowTypeToColor(flowType) : Brushes.Gainsboro,
            };
            SetLeft(hookView, linePointExternal.X - HOOK_RADIUS);
            SetTop(hookView, linePointExternal.Y - HOOK_RADIUS);

            return (line, hookView);
        }

        internal void Deselect()
        {
            Circle.Stroke = Brushes.Black;
        }

        internal void Selected()
        {
            Circle.Stroke = Brushes.Goldenrod;
        }
    }
}
