using System.Collections.ObjectModel;
using RemotePlanning.Iteration;
using RemotePlanning.Projects;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Main
{
    public class MainWindowViewModel : ViewModel
    {
        private ProjectViewModel _selectedProject;
        private IterationViewModel _selectedIteration;
        public CanvasZoomViewModel ZoomViewModel { get; }
        public ObservableCollection<ProjectViewModel> Projects { get; private set; }

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
            Projects = new ObservableCollection<ProjectViewModel>();
        }
    }
}