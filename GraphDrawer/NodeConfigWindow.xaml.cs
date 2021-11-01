using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace GraphDrawer
{
    /// <summary>
    /// Interaction logic for NodeConfigWindow.xaml
    /// </summary>
    public partial class NodeConfigWindow : Window
    {
        public event Action CancelClicked;
        public event Action<List<ConnectionConfigViewModel>> OkClicked;

        public List<string> Types { get; } = new List<string>()
        {
            "Air", "Water", "Refrigerant"
        };


        public ConnectionConfigViewModel CurrConnection { get; set; }
        public ObservableCollection<ConnectionConfigViewModel> Connections { get; set; }

        public NodeConfigWindow()
        {
            CurrConnection = new ConnectionConfigViewModel();
            Connections = new ObservableCollection<ConnectionConfigViewModel>();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connections.Add(CurrConnection.Clone());
            CurrConnection.Reset();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            OkClicked?.Invoke(Connections.ToList());
        }
    }
}
