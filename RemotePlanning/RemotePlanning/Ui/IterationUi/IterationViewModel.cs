using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using RemotePlanning.Ui.PlanningSheetsUi;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.IterationUi
{
    [DataContract]
    public class IterationViewModel : ViewModel
    {
        public IterationViewModel()
        {
            PlanningSheets = new ObservableCollection<PlanningSheetViewModel>();
            Storycards = new ObservableCollection<PlacedStorycardViewModel>();
        }

        [DataMember]
        public int IterationNumber { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public ObservableCollection<PlanningSheetViewModel> PlanningSheets { get; private set; }
        [DataMember]
        public ObservableCollection<PlacedStorycardViewModel> Storycards { get; private set; }
    }
}