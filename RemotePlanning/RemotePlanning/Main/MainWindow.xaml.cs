using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using RemotePlanning.Main;
using RemotePlanning.PlanningSheets;
using RemotePlanning.PlanningSheets.Control;

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
            ViewModel.PropertyChanged += SelectedIteration_PropertyChange;
        }

        private void SelectedIteration_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedIteration"))
            {
                GameCanvas.Children.Clear();
                foreach (PlanningSheetViewModel planningSheetViewModel in ViewModel.SelectedIteration.PlanningSheets)
                {
                    GameCanvas.Children.Add(new PlanningSheetControl() { DataContext = planningSheetViewModel });
                }

            }
        }

        private void Canvas_OnMouseWheel(object sender, MouseWheelEventArgs mouseArgs)
        {
            ViewModel.ZoomViewModel.AdjustScale(CanvasContainer, mouseArgs);
            mouseArgs.Handled = true;
        }
    }
}
