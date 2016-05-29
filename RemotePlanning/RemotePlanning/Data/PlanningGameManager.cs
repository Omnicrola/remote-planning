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
        private readonly Main.MainWindow _mainWindow;

        public PlanningGameManager(Main.MainWindow mainWindow)
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
            var iterationViewModel1 = new IterationViewModel
            {
                EndDate = new DateTime(2016, 7, 8),
                IterationNumber = 6,
            };
            var iterationViewModel2 = new IterationViewModel
            {
                EndDate = new DateTime(2016, 7, 1),
                IterationNumber = 5,
            };
            var planningSheetViewModel = new PlanningSheetViewModel
            {
                Role = "DEV",
            };
            iterationViewModel1.PlanningSheets.Add(planningSheetViewModel);
            projectViewModel.Iterations.Add(iterationViewModel1);
            projectViewModel.Iterations.Add(iterationViewModel2);
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
            _mainWindow.ViewModel.SelectedIteration = iterationViewModel1;
        }
    }
}