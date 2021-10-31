using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Put { get; set; }
        public bool Connect { get; set; }
        public bool Free { get; set; }


        private readonly PutCanvasState putCanvasState;
        private readonly FreeCanvasState freeCanvasState;
        private readonly ConnectCanvasState connectCanvasState;

        private ICanvasState currCanvasState;

        public MainWindow()
        {
            // default
            Put = true;

            InitializeComponent();

            putCanvasState = new PutCanvasState(MainCanvas);
            freeCanvasState = new FreeCanvasState(MainCanvas);
            connectCanvasState = new ConnectCanvasState(MainCanvas);

            // default
            SetCanvasState(putCanvasState);
        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            currCanvasState.Detach();
            if (Put)
            {
                Debug.WriteLine("Put");
                SetCanvasState(putCanvasState);
            }
            else if (Connect)
            {
                Debug.WriteLine("Connect");
                SetCanvasState(connectCanvasState);
            }
            else if (Free)
            {
                Debug.WriteLine("Free");
                SetCanvasState(freeCanvasState);
            }
        }

        private void SetCanvasState(ICanvasState state)
        {
            currCanvasState = state;
            currCanvasState.Attach();
        }
    }
}
