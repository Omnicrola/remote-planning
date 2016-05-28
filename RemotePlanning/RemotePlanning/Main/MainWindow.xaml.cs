using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using RemotePlanning.Iteration;
using RemotePlanning.Main;
using RemotePlanning.PlanningSheets;
using RemotePlanning.PlanningSheets.Control;
using RemotePlanning.Storycards;

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

        private void AddCards_OnClick(object sender, RoutedEventArgs e)
        {
            var storycardBatchLoadWindow = new StorycardBatchLoadWindow() { Owner = this };
            storycardBatchLoadWindow.ShowDialog();
        }

        private void SelectIteration_OnClick(object sender, RoutedEventArgs e)
        {
            var selectIterationWindow = new SelectIterationWindow()
            {
                Owner = this
            };
            selectIterationWindow.ShowDialog();

        }
    }
}
