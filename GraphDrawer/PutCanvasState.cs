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
    public class PutCanvasState : ICanvasState
    {
        private readonly Canvas mainCanvas;

        private Point lastPoint;
        private NodeConfigWindow nodeConfigWindow;

        public PutCanvasState(Canvas mainCanvas)
        {
            this.mainCanvas = mainCanvas;
        }

        public void Attach()
        {
            mainCanvas.MouseDown += Put_MouseDown;
        }

        private void Put_MouseDown(object s, MouseEventArgs e)
        {
            lastPoint = e.GetPosition(mainCanvas);
            nodeConfigWindow = new NodeConfigWindow();

            nodeConfigWindow.OkClicked += CreateNodeCanvas;
            nodeConfigWindow.CancelClicked += CleanCreationNodeListeners;
            nodeConfigWindow.Closed += CloseNodeConfigWindow;

            nodeConfigWindow.Show();
        }

        private void CloseNodeConfigWindow(object sender, EventArgs e)
        {
            nodeConfigWindow.Closed -= CloseNodeConfigWindow;
            CleanCreationNodeListeners();
        }

        private void CleanCreationNodeListeners()
        {
            // detach listeners
            nodeConfigWindow.OkClicked -= CreateNodeCanvas;
            nodeConfigWindow.CancelClicked -= CleanCreationNodeListeners;

            nodeConfigWindow.Close();
            nodeConfigWindow = null;
            lastPoint = new Point();
        }

        private void CreateNodeCanvas(List<ConnectionConfigViewModel> connections)
        {
            var nodeVm = new NodeViewModel(lastPoint, connections);
            mainCanvas.Children.Add(new NodeCanvas(nodeVm));
            CleanCreationNodeListeners();
        }

        public void Detach()
        {
            mainCanvas.MouseDown -= Put_MouseDown;
        }
    }
}
