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
        private const double CIRCLE_RADIUS = 20;
        private const double HOOK_RADIUS = 6;
        private const double HOOK_DIAMETER = HOOK_RADIUS * 2;

        private const double DIST_FROM_ELLIPSE_INP = 10.0;
        private const double DIST_FROM_ELLIPSE_OUT = 12.0;

        private readonly Ellipse circle;


        public NodeCanvas(Point center, List<ConnectionViewModel> connections)
        {
            InitializeComponent();

            // set global coordinates
            SetLeft(this, center.X);
            SetTop(this, center.Y);

            this.Width = CIRCLE_RADIUS * 4;
            this.Height = CIRCLE_RADIUS * 4;


            // draw main circle
            circle = new Ellipse()
            {
                Width = CIRCLE_RADIUS * 2,
                Height = CIRCLE_RADIUS * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2.0,
                Fill = Brushes.Silver
            };

            SetLeft(circle, -CIRCLE_RADIUS);
            SetTop(circle, -CIRCLE_RADIUS);

            Children.Add(circle);

            // draw connection hooks
            var inputs = connections.Select(conn => (conn.NumInput, conn.Type)).ToList();
            var inputsCount = connections.Select(conn => conn.NumInput).Sum();
            DrawHooks(inputs, inputsCount, -1);

            var outputs = connections.Select(conn => (conn.NumOutput, conn.Type)).ToList();
            var outputsCount = connections.Select(conn => conn.NumOutput).Sum();
            DrawHooks(outputs, outputsCount, 1);
        }

        // direction = 1 => RIGHT, -1 => LEFT
        private void DrawHooks(List<(int, string)> hookTuples, int count, int direction)
        {
            var distanceBtwHooks = CIRCLE_RADIUS * 2 / (1 + count);
            int counter = 1;
            foreach (var (num, type) in hookTuples)
            {
                var xHookCoord = direction * (CIRCLE_RADIUS + DIST_FROM_ELLIPSE_OUT);

                for (int k = 0; k < num; k++)
                {
                    var yHookCoord = distanceBtwHooks * counter - CIRCLE_RADIUS;
                    DrawHook(new Point(xHookCoord, yHookCoord), direction);
                    counter++;
                }
            }
        }

        // direction = 1 => RIGHT, -1 => LEFT
        private Ellipse DrawHook(Point linePointExternal, int direction)
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
            
            Children.Add(hookView);
            Children.Add(line);

            return hookView;
        }

        internal void Deselect()
        {
            circle.Stroke = Brushes.Black;
        }

        internal void Selected()
        {
            circle.Stroke = Brushes.Goldenrod;
        }
    }
}
