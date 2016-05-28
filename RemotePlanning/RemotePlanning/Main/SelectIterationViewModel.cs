using System.Collections.ObjectModel;
using RemotePlanning.Iteration;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Main
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