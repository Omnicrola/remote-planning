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
        public MainWindowViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new MainWindowViewModel(new CanvasZoomViewModel());
        }

        private void Canvas_OnMouseWheel(object sender, MouseWheelEventArgs mouseArgs)
        {

            ViewModel.ZoomViewModel.AdjustScale(CanvasContainer, mouseArgs);
            mouseArgs.Handled = true;
        }
    }
}
