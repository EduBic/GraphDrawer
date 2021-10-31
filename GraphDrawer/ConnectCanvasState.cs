using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphDrawer
{
    internal class ConnectCanvasState : ICanvasState
    {
        private readonly Canvas mainCanvas;



        public ConnectCanvasState(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }

        public void Attach()
        {
            mainCanvas.MouseDown += OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(mainCanvas);
            mainCanvas.Children.Add(new EdgeCanvas(p));
        }

        public void Detach()
        {
            mainCanvas.MouseDown -= OnMouseDown;
        }
    }
}