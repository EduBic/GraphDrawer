using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphDrawer
{
    public class FreeCanvasState : ICanvasState
    {
        private readonly Canvas mainCanvas;

        public NodeCanvas SelectedElement { get; private set; }

        private Point? startCanvasPan;

        public FreeCanvasState(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }

        public void Attach()
        {
            mainCanvas.MouseDown += Canvas_StartPan;

            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    (child as NodeCanvas).MouseDown += NodeCanvas_StartElementMove;
                }
            }
            mainCanvas.MouseLeave += Canvas_StopElementMove;
            mainCanvas.MouseLeave += Canvas_StopPan;
        }

        private void Canvas_StartPan(object sender, MouseButtonEventArgs e)
        {
            SelectedElement?.Deselect();

            startCanvasPan = e.GetPosition(mainCanvas);

            var s = sender as Canvas;
            mainCanvas.MouseMove += Canvas_Pan;
            mainCanvas.MouseUp += Canvas_StopPan;
        }


        private void Canvas_Pan(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(mainCanvas);

            if (!startCanvasPan.HasValue)
                throw new NotSupportedException();

            var tX = (startCanvasPan.Value.X - p.X) * 0.6;
            var tY = (startCanvasPan.Value.Y - p.Y) * 0.6;
            startCanvasPan = p;

            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    var node = child as NodeCanvas;
                    (node.DataContext as NodeViewModel).Translate(-tX, -tY);
                }
            }
        }

        private void Canvas_StopPan(object sender, MouseEventArgs e)
        {
            startCanvasPan = null;
            mainCanvas.MouseMove -= Canvas_Pan;
            mainCanvas.MouseUp -= Canvas_StopPan;
        }

        private void NodeCanvas_StartElementMove(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            SelectedElement?.Deselect();

            if (sender is NodeCanvas)
            {
                SelectedElement = sender as NodeCanvas;
                SelectedElement.Selected();

                // add feature of element moving
                mainCanvas.MouseMove += Canvas_MouseMove;
                mainCanvas.MouseUp += Canvas_StopElementMove;
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(mainCanvas);
            //var translationX = p.X - elemVm.X;
            //var translationY = p.Y - elemVm.Y;

            if (p.X < 0 || p.Y < 0)
            {
                Canvas_StopElementMove(mainCanvas, e);
            }

            (SelectedElement.DataContext as NodeViewModel).ChangePosition(p);

            //Canvas.SetLeft(SelectedElement, p.X);
            //Canvas.SetTop(SelectedElement, p.Y);
        }

        private void Canvas_StopElementMove(object sender, MouseEventArgs e)
        {
            mainCanvas.MouseMove -= Canvas_MouseMove;
            mainCanvas.MouseUp -= Canvas_StopElementMove;
        }

        public void Detach()
        {
            SelectedElement?.Deselect();
            SelectedElement = null;

            mainCanvas.MouseDown -= Canvas_StartPan;
            mainCanvas.MouseLeave -= Canvas_StopElementMove;
            mainCanvas.MouseLeave -= Canvas_StopPan;

            foreach (var child in mainCanvas.Children)
            {
                (child as Canvas).MouseDown -= NodeCanvas_StartElementMove;
            }
        }
    }
}
