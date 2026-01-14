using Swack.Logic;
using Swack.Logic.Models;
using Swack.UI.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Swack.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var currentUser = new User("John Doe", "https://robohash.org/123.png?size=150x150");
            var messagingLogic = new SimulatedMessagingLogic(currentUser);

            var viewModel = new MainViewModel(messagingLogic);

            this.DataContext = viewModel;
            // register onto Loaded event
            this.Loaded += async (s, e) => await viewModel.InitializeAsync();
        }
    }
}