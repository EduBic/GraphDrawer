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
        private HookViewModel start;

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
                        hook.Item2.MouseDown += OutHook_OnMouseDown;
                    }
                }
            }
        }

        private void OutHook_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // save internal state
            var hook = sender as Ellipse;
            start = hook.DataContext as HookViewModel;

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
                        hook.Item2.MouseDown -= OutHook_OnMouseDown;
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
                        hook.Item2.MouseDown += InpHook_MouseDown;
                    }
                }
            }
        }

        private void InpHook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var inHook = sender as Ellipse;
            var end = inHook.DataContext as HookViewModel;

            var edgeVm = new EdgeViewModel(start, end);
            var edge = new EdgeCanvas(edgeVm);
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
                        hook.Item2.MouseDown -= InpHook_MouseDown;
                    }
                }
            }
        }


    }
}