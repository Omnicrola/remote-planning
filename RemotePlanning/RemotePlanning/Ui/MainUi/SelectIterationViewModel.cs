using System.Collections.ObjectModel;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.MainUi
{
    public class SelectIterationViewModel : ViewModel
    {
        public ObservableCollection<IterationViewModel> Iterations { get; set; }
        public IterationViewModel SelectedIteration { get; set; }

        public SelectIterationViewModel(ObservableCollection<IterationViewModel> iterations, IterationViewModel selectedIteration)
        {
            Iterations = iterations;
            SelectedIteration = selectedIteration;
        }
    }
}