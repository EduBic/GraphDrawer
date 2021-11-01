using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphDrawer
{
    public class FreeCanvasState : ICanvasState
    {
        private readonly Canvas mainCanvas;

        public NodeCanvas SelectedElement { get; private set; }

        public FreeCanvasState(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }

        public void Attach()
        {
            foreach (var child in mainCanvas.Children)
            {
                (child as Canvas).MouseDown += OnMouseDown;
            }
            mainCanvas.MouseLeave += Canvas_MouseUp;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedElement?.Deselect();

            if (sender is NodeCanvas)
            {
                SelectedElement = sender as NodeCanvas;
                SelectedElement.Selected();

                // add feature of element moving
                mainCanvas.MouseMove += Canvas_MouseMove;
                mainCanvas.MouseUp += Canvas_MouseUp;
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(mainCanvas);
            //var translationX = p.X - elemVm.X;
            //var translationY = p.Y - elemVm.Y;

            if (p.X < 0 || p.Y < 0)
            {
                Canvas_MouseUp(mainCanvas, e);
            }

            (SelectedElement.DataContext as NodeViewModel).ChangePosition(p);

            //Canvas.SetLeft(SelectedElement, p.X);
            //Canvas.SetTop(SelectedElement, p.Y);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mainCanvas.MouseMove -= Canvas_MouseMove;
            mainCanvas.MouseUp -= Canvas_MouseUp;
        }

        public void Detach()
        {
            SelectedElement?.Deselect();
            SelectedElement = null;

            foreach (var child in mainCanvas.Children)
            {
                (child as Canvas).MouseDown -= OnMouseDown;
            }
        }
    }
}
