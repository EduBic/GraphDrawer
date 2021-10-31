using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphDrawer
{
    public record DanglingHook(int NodeId, int ConnIndex, int HookIndex);

    internal class ConnectCanvasState : ICanvasState
    {
        private readonly Canvas mainCanvas;

        // internal state
        private Point start;

        public ConnectCanvasState(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }

        public void Attach()
        {
            // start from the outputHooks
            AttachOutputHooks();
        }

        public void Detach()
        {
            DetachOutputHooks();
            DetachInputHooks();
        }

        private void AttachOutputHooks()
        {
            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    var node = child as NodeCanvas;
                    foreach (var hook in node.OutHooks)
                    {
                        hook.MouseDown += OutHook_OnMouseDown;
                    }
                }
            }
        }

        private void OutHook_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // save internal state
            start = e.GetPosition(mainCanvas);

            DetachOutputHooks();
            AttachInputHooks();
        }

        private void DetachOutputHooks()
        {
            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    var node = child as NodeCanvas;
                    foreach (var hook in node.OutHooks)
                    {
                        hook.MouseDown -= OutHook_OnMouseDown;
                    }
                }
            }
        }


        private void AttachInputHooks()
        {
            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    var node = child as NodeCanvas;
                    foreach (var hook in node.InpHooks)
                    {
                        hook.MouseDown += InpHook_MouseDown;
                    }
                }
            }
        }

        private void InpHook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var end = e.GetPosition(mainCanvas);

            var edge = new EdgeCanvas(start, end);
            mainCanvas.Children.Add(edge);

            DetachInputHooks();
            AttachOutputHooks();
        }

        private void DetachInputHooks()
        {
            foreach (var child in mainCanvas.Children)
            {
                if (child is NodeCanvas)
                {
                    var node = child as NodeCanvas;
                    foreach (var hook in node.InpHooks)
                    {
                        hook.MouseDown -= InpHook_MouseDown;
                    }
                }
            }
        }


    }
}