using System;
using System.Collections.ObjectModel;
using RemotePlanning.PlanningSheets;
using RemotePlanning.Storycards;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Iteration
{
    public class IterationViewModel : ViewModel
    {
        public IterationViewModel()
        {
            PlanningSheets = new ObservableCollection<PlanningSheetViewModel>();
            Storycards = new ObservableCollection<PlacedStorycardViewModel>();
        }

        public int IterationNumber { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<PlanningSheetViewModel> PlanningSheets { get; private set; }
        public ObservableCollection<PlacedStorycardViewModel> Storycards { get; private set; }
    }
}