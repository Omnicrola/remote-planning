using System.Windows;
using System.Windows.Input;
using RemotePlanning.Main;

namespace RemotePlanning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel();
        }

        private void Canvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _mainWindowViewModel.AdjustScale(e.Delta);
        }
    }
}
