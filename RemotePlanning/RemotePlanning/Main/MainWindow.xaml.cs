using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RemotePlanning.Iteration;
using RemotePlanning.Main;
using RemotePlanning.PlanningSheets;
using RemotePlanning.PlanningSheets.Control;
using RemotePlanning.Storycards;
using RemotePlanning.Storycards.Control;

namespace RemotePlanning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IterationViewModel _previouslySelectedIteration;
        public MainWindowViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new MainWindowViewModel(new CanvasZoomViewModel());
            ViewModel.ZoomViewModel.ScaleX = 0.5;
            ViewModel.ZoomViewModel.ScaleY = 0.5;
            ViewModel.PropertyChanged += SelectedIteration_PropertyChange;
        }

        private void SelectedIteration_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedIteration"))
            {
                if (_previouslySelectedIteration != null)
                {
                    _previouslySelectedIteration.PlanningSheets.CollectionChanged -= PlanningSheets_OnChange;
                    _previouslySelectedIteration.Storycards.CollectionChanged -= Storycards_OnChange;
                }
                ClearPlanningSheets();
                ViewModel.SelectedIteration.PlanningSheets.CollectionChanged += PlanningSheets_OnChange;
                ViewModel.SelectedIteration.Storycards.CollectionChanged += Storycards_OnChange;
                foreach (PlanningSheetViewModel planningSheetViewModel in ViewModel.SelectedIteration.PlanningSheets)
                {
                    AddPlanningSheet(planningSheetViewModel);
                }
                _previouslySelectedIteration = ViewModel.SelectedIteration;
            }
        }

        private void ClearPlanningSheets()
        {
            GameCanvas.Children
                .Cast<UIElement>()
                .OfType<PlanningSheetControl>()
                .ToList()
                .ForEach(c => c.PlanningSheetMoved -= ReorderPlanningSheets);
            GameCanvas.Children.Clear();
        }

        private void ReorderPlanningSheets(object sender, PlanningSheetMovedArgs e)
        {
            var lasControlToMove = e.PlanningSheetControl;
            int zIndex = 1;
            GetAllPlanningSheetControls()
                .ForEach(c =>
                {
                    if (c == lasControlToMove)
                    {
                        Canvas.SetZIndex(c, 99);
                    }
                    else
                    {
                        Canvas.SetZIndex(c, zIndex++);
                    }
                });
        }
        private void ReorderStorycards(object sender, StorycardMovedEventArgs e)
        {
            var lasControlToMove = e.StorycardControl;
            int zIndex = 101;
            GetAllStorycardControls()
                 .ForEach(c =>
                 {
                     if (c == lasControlToMove)
                     {
                         Canvas.SetZIndex(c, 999);
                     }
                     else
                     {
                         Canvas.SetZIndex(c, zIndex++);
                     }
                 });
        }

        private List<PlanningSheetControl> GetAllPlanningSheetControls()
        {
            return GameCanvas.Children
                .Cast<UIElement>()
                .OfType<PlanningSheetControl>()
                .OrderBy(Canvas.GetZIndex)
                .ToList();
        }
        private List<StorycardControl> GetAllStorycardControls()
        {
            return GameCanvas.Children
                .Cast<UIElement>()
                .OfType<StorycardControl>()
                .OrderBy(Canvas.GetZIndex)
                .ToList();
        }

        private void PlanningSheets_OnChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            e.NewItems.Cast<PlanningSheetViewModel>().ToList().ForEach(AddPlanningSheet);
        }
        private void Storycards_OnChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            e.NewItems.Cast<PlacedStorycardViewModel>().ToList().ForEach(AddStoryCard);
        }

        private void AddStoryCard(PlacedStorycardViewModel storycardViewModel)
        {
            var storycardControl = new StorycardControl() { DataContext = storycardViewModel };
            storycardControl.StorycardMoved += ReorderStorycards;
            Canvas.SetTop(storycardControl, new Random().Next(10, 500));
            Canvas.SetLeft(storycardControl, new Random().Next(10, 500));
            GameCanvas.Children.Add(storycardControl);
        }

        private void AddPlanningSheet(PlanningSheetViewModel planningSheetViewModel)
        {
            var planningSheetControl = new PlanningSheetControl() { DataContext = planningSheetViewModel };
            planningSheetControl.PlanningSheetMoved += ReorderPlanningSheets;
            Canvas.SetTop(planningSheetControl, new Random().Next(10, 500));
            Canvas.SetLeft(planningSheetControl, new Random().Next(10, 500));
            GameCanvas.Children.Add(planningSheetControl);
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
    }
}
