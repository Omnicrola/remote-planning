using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.ProjectsUi
{
    [DataContract]
    public class ProjectViewModel : ViewModel
    {
        [DataMember]
        private string _name;

        public ProjectViewModel()
        {
            Iterations = new ObservableCollection<IterationViewModel>();
            Storycards = new ObservableCollection<StorycardViewModel>();
        }

        [DataMember]
        public ObservableCollection<StorycardViewModel> Storycards { get; private set; }

        [DataMember]
        public ObservableCollection<IterationViewModel> Iterations { get; private set; }

        public string Name
        {
            get { return _name; }
            set { SetPropertyField(ref _name, value); }
        }
    }
}