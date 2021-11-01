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
        private readonly Line line;

        public EdgeCanvas(EdgeViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            SetLeft(this, vm.Start.X);
            SetTop(this, vm.Start.Y);

            line = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = vm.End.X - vm.Start.X,
                Y2 = vm.End.Y - vm.Start.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };

            _ = Children.Add(line);
        }
    }
}
