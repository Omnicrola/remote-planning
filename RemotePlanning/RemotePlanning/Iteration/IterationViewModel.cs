using System;
using System.Collections.ObjectModel;
using RemotePlanning.PlanningSheets;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Iteration
{
    public class IterationViewModel : ViewModel
    {
        public IterationViewModel()
        {
            PlanningSheets = new ObservableCollection<PlanningSheetViewModel>();

        }

        public int IterationNumber { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<PlanningSheetViewModel> PlanningSheets { get; private set; }
    }
}