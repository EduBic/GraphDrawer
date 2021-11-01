using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            SetLeft(this, vm.Start.AbsX);
            SetTop(this, vm.Start.AbsY);

            SetBinding(LeftProperty, new Binding(nameof(vm.Start.AbsX)) { Source = vm.Start });
            SetBinding(TopProperty, new Binding(nameof(vm.Start.AbsY)) { Source = vm.Start });

            line = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = vm.End.AbsX - vm.Start.AbsX,
                Y2 = vm.End.AbsY - vm.Start.AbsY,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };

            // Binding with conversion
            vm.Start.PropertyChanged += UpdateHeadEdgeFromStart;
            vm.End.PropertyChanged += UpdateHeadEdge;

            _ = Children.Add(line);
        }

        private void UpdateHeadEdgeFromStart(object s, PropertyChangedEventArgs e)
        {
            var edgeVm = DataContext as EdgeViewModel;
            var hookVm = s as HookViewModel;

            if (e.PropertyName == nameof(hookVm.AbsX))
            {
                line.X2 = edgeVm.End.AbsX - hookVm.AbsX;
            }
            else if (e.PropertyName == nameof(hookVm.AbsY))
            {
                line.Y2 = edgeVm.End.AbsY - hookVm.AbsY;
            }
        }

        private void UpdateHeadEdge(object s, PropertyChangedEventArgs e)
        {
            var edgeVm = DataContext as EdgeViewModel;
            var hookVm = s as HookViewModel;

            if (e.PropertyName == nameof(hookVm.AbsX))
            {
                line.X2 = hookVm.AbsX - edgeVm.Start.AbsX;
            }
            else if (e.PropertyName == nameof(hookVm.AbsY))
            {
                line.Y2 = hookVm.AbsY - edgeVm.Start.AbsY;
            }
        }
    }
}
