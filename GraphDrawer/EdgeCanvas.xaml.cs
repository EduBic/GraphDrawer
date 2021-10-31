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
    /// Interaction logic for EdgeCanvas.xaml
    /// </summary>
    public partial class EdgeCanvas : Canvas
    {
        private const double LINE_LEN = 10;
        private readonly Line line;

        public EdgeCanvas(Point center)
        {
            InitializeComponent();

            SetLeft(this, center.X);
            SetTop(this, center.Y);

            line = new Line()
            {
                X1 = -LINE_LEN / 2,
                Y1 = 0,
                X2 = LINE_LEN / 2,
                Y2 = 0,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5,
            };

            _ = Children.Add(line);
        }
    }
}
