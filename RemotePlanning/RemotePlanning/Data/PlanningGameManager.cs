using System;
using System.Windows;
using RemotePlanning.Iteration;
using RemotePlanning.PlanningSheets;
using RemotePlanning.Projects;
using RemotePlanning.Storycards;

namespace RemotePlanning.Data
{
    internal class PlanningGameManager
    {
        private readonly MainWindow _mainWindow;

        public PlanningGameManager(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            mainWindow.Loaded += Window_OnLoaded;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            var projectViewModel = new ProjectViewModel
            {
                Name = "Bell",
            };
            var iterationViewModel = new IterationViewModel
            {
                EndDate = new DateTime(2016, 7, 7),
                IterationNumber = 5,
            };
            var planningSheetViewModel = new PlanningSheetViewModel
            {
                Role = "DEV",
            };
            iterationViewModel.PlanningSheets.Add(planningSheetViewModel);
            projectViewModel.Iterations.Add(iterationViewModel);
            var storycard1 = new StorycardViewModel
            {
                Role = "DEV",
                Content = "I am some content",
                Estimate = 16,
                Title = "Create a remote planning game"
            };
            projectViewModel.Storycards.Add(storycard1);
            planningSheetViewModel.PlannedCards.Add(new PlacedStorycardViewModel(storycard1));

            _mainWindow.ViewModel.Projects.Add(projectViewModel);
            _mainWindow.ViewModel.SelectedProject = projectViewModel;
            _mainWindow.ViewModel.SelectedIteration = iterationViewModel;
        }
    }
}