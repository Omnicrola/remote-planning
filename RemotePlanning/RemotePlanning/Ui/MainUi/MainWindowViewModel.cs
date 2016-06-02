using System.Collections.ObjectModel;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.ProjectsUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.MainUi
{
    public class MainWindowViewModel : ViewModel
    {
        private ProjectViewModel _selectedProject;
        private IterationViewModel _selectedIteration;
        public CanvasZoomViewModel ZoomViewModel { get; }

        public ProjectViewModel SelectedProject
        {
            get { return _selectedProject; }
            set { SetPropertyField(ref _selectedProject, value); }
        }

        public IterationViewModel SelectedIteration
        {
            get { return _selectedIteration; }
            set { SetPropertyField(ref _selectedIteration, value); }
        }

        public MainWindowViewModel(CanvasZoomViewModel canvasZoomViewModel)
        {
            this.ZoomViewModel = canvasZoomViewModel;
            var projectViewModel = new ProjectViewModel();
            var iterationViewModel = new IterationViewModel();
            projectViewModel.Iterations.Add(_selectedIteration);

            _selectedIteration = iterationViewModel;
            _selectedProject = projectViewModel;
        }
    }
}