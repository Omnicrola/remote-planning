using System.Collections.ObjectModel;
using RemotePlanning.Iteration;
using RemotePlanning.Storycards;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Projects
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