using System.Collections.ObjectModel;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.ProjectsUi
{
    public class ProjectViewModel : ViewModel
    {
        private string _name;

        public ProjectViewModel()
        {
            Iterations = new ObservableCollection<IterationViewModel>();
            Storycards = new ObservableCollection<StorycardViewModel>();
        }

        public ObservableCollection<IterationViewModel> Iterations { get; private set; }
        public ObservableCollection<StorycardViewModel> Storycards { get; private set; }

        public string Name
        {
            get { return _name; }
            set { SetPropertyField(ref _name, value); }
        }
    }
}