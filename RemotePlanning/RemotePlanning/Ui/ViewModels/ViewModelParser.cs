using System;
using System.Linq;
using System.Windows.Media;
using RemotePlanning.Data;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.MainUi;
using RemotePlanning.Ui.PlanningSheetsUi;
using RemotePlanning.Ui.ProjectsUi;
using RemotePlanning.Ui.StorycardsUi;

namespace RemotePlanning.Ui.ViewModels
{
    internal class ViewModelParser
    {
        private readonly IMainWindow _mainWindow;

        public ViewModelParser(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void ClearAndLoad(ApplicationDataStore applicationDataStore)
        {
            var mainWindowViewModel = _mainWindow.ViewModel;

            mainWindowViewModel.ZoomViewModel.Reset();

            mainWindowViewModel.SelectedProject = applicationDataStore.Project;
            mainWindowViewModel.SelectedIteration = applicationDataStore.SelectedIteration;
        }

        public ApplicationDataStore ExtractData()
        {
            var applicationDataStore = new ApplicationDataStore
            {
                Project = _mainWindow.ViewModel.SelectedProject,
                SelectedIteration = _mainWindow.ViewModel.SelectedIteration,
            };
            CreateFakeData(applicationDataStore);
            return applicationDataStore;
        }

        private void CreateFakeData(ApplicationDataStore applicationDataStore)
        {
            var projectViewModel = new ProjectViewModel
            {
                Name = "Bell"
            };
            var storycard1 = new StorycardViewModel()
            {
                Role = "DEV",
                Title = "My Storycard",
                Estimate = 16
            };
            var storycard2 = new StorycardViewModel()
            {
                Role = "DEV",
                Title = "My Storycard",
                Estimate = 16
            };
            projectViewModel.Storycards.Add(storycard1);
            projectViewModel.Storycards.Add(storycard2);

            var iterationViewModel = new IterationViewModel()
            {
                EndDate = new DateTime(2016, 7, 7),
                IterationNumber = 4,
            };
            iterationViewModel.Storycards.Add(new PlacedStorycardViewModel(storycard1));
            var planningSheetViewModel = new PlanningSheetViewModel
            {
                Role = "DEV",
                Color = Brushes.Green
            };

            planningSheetViewModel.PlannedCards.Add(new PlacedStorycardViewModel(storycard2));

            iterationViewModel.PlanningSheets.Add(planningSheetViewModel);

            projectViewModel.Iterations.Add(iterationViewModel);


            applicationDataStore.SelectedIteration = iterationViewModel;
            applicationDataStore.Project = projectViewModel;
        }
    }
}