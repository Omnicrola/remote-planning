﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using RemotePlanning.Iteration;
using RemotePlanning.Main.GameCanvas;
using RemotePlanning.Network;
using RemotePlanning.PlanningSheets;
using RemotePlanning.PlanningSheets.Control;
using RemotePlanning.Storycards;
using RemotePlanning.Storycards.Control;

namespace RemotePlanning.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private IterationViewModel _previouslySelectedIteration;
        private readonly CanvasElementHandler<PlanningSheetControl> _planningSheetHandler;
        private readonly CanvasElementHandler<StorycardControl> _storycardHandler;
        public event EventHandler<RoutedEventArgs> WindowLoaded;
        public event EventHandler<NetworkConnectEventArgs> NetworkConnect;
        public event EventHandler<NetworkHostEventArgs> HostNetworkSession;
        public MainWindowViewModel ViewModel { get; private set; }

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
    }
}
