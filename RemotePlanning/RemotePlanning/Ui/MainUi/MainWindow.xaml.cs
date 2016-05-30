using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using RemotePlanning.Network;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.MainUi.GameCanvas;
using RemotePlanning.Ui.PlanningSheetsUi;
using RemotePlanning.Ui.PlanningSheetsUi.Control;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.StorycardsUi.Control;

namespace RemotePlanning.Ui.MainUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private IterationViewModel _previouslySelectedIteration;
        private readonly CanvasElementHandler<PlanningSheetControl> _planningSheetHandler;
        private readonly CanvasElementHandler<StorycardControl> _storycardHandler;
        public MainWindowViewModel ViewModel { get; private set; }

        public event EventHandler<RoutedEventArgs> WindowLoaded;
        public event EventHandler<EventArgs> WindowClosed;
        public event EventHandler<NetworkConnectEventArgs> NetworkConnect;
        public event EventHandler<NetworkHostEventArgs> HostNetworkSession;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new MainWindowViewModel(new CanvasZoomViewModel());
            ViewModel.ZoomViewModel.ScaleX = 0.5;
            ViewModel.ZoomViewModel.ScaleY = 0.5;
            ViewModel.PropertyChanged += SelectedIteration_PropertyChange;
            _planningSheetHandler = new CanvasElementHandler<PlanningSheetControl>(GameCanvas, 1, 100);
            _storycardHandler = new CanvasElementHandler<StorycardControl>(GameCanvas, 101, 400);
            Loaded += RaiseLoaded;
            Closed += RaiseClosed;
        }

        private void RaiseClosed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(sender, e);
        }

        private void RaiseLoaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded?.Invoke(sender, e);
        }


        private void SelectedIteration_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedIteration"))
            {
                if (_previouslySelectedIteration != null)
                {
                    _previouslySelectedIteration.PlanningSheets.CollectionChanged -= _planningSheetHandler.OnCollectionChange;
                    _previouslySelectedIteration.Storycards.CollectionChanged -= _storycardHandler.OnCollectionChange;
                }
                _planningSheetHandler.Clear();
                _storycardHandler.Clear();
                ViewModel.SelectedIteration.PlanningSheets.CollectionChanged += _planningSheetHandler.OnCollectionChange;
                ViewModel.SelectedIteration.Storycards.CollectionChanged += _storycardHandler.OnCollectionChange;
                foreach (PlanningSheetViewModel planningSheetViewModel in ViewModel.SelectedIteration.PlanningSheets)
                {
                    _planningSheetHandler.AddToCanvas(planningSheetViewModel);
                }
                foreach (PlacedStorycardViewModel storycard in ViewModel.SelectedIteration.Storycards)
                {
                    _storycardHandler.AddToCanvas(storycard);
                }
                _previouslySelectedIteration = ViewModel.SelectedIteration;
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
            storycardBatchLoadWindow.StorycardCreated += AddNewStorycards;
            storycardBatchLoadWindow.ShowDialog();
        }

        private void AddNewStorycards(object sender, StorycardCreatedEventArgs e)
        {
            e.Storycards.ForEach(card =>
            {
                ViewModel.SelectedIteration.Storycards.Add(new PlacedStorycardViewModel(card));
            });

        }

        private void SelectIteration_OnClick(object sender, RoutedEventArgs e)
        {
            var selectIterationWindow = new SelectIterationWindow(new SelectIterationViewModel(ViewModel.SelectedProject.Iterations, ViewModel.SelectedIteration))
            {
                Owner = this
            };
            selectIterationWindow.SelectIteration += ChangeSelectedIteration;
            selectIterationWindow.ShowDialog();

        }

        private void ChangeSelectedIteration(object sender, SelectIterationEventArgs eventArgs)
        {
            ViewModel.SelectedIteration = eventArgs.SelectedIteration;
        }

        private void AddSheet_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedIteration.PlanningSheets.Add(new PlanningSheetViewModel());
        }

        private void NetworkConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var networkConnectWindow = new NetworkConnectWindow() { Owner = this };
            networkConnectWindow.ConnectToServer += RaiseConnect;
            networkConnectWindow.ShowDialog();
        }

        private void RaiseConnect(object sender, NetworkConnectEventArgs e)
        {
            NetworkConnect?.Invoke(sender, e);
        }

        private void NetworkHost_OnClick(object sender, RoutedEventArgs e)
        {
            HostNetworkSession?.Invoke(this, new NetworkHostEventArgs());
        }

        public void AddStatusMessage(string message)
        {
            StatusText.Text = message;
        }

    }
}
