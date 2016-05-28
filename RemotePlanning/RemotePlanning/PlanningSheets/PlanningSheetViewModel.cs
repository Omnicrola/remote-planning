using System;
using RemotePlanning.ViewModels;

namespace RemotePlanning.PlanningSheets
{
    public class PlanningSheetViewModel : ViewModel
    {
        private DateTime _weekEnding;
        private string _role;

        public DateTime WeekEnding
        {
            get { return _weekEnding; }
            set { SetPropertyField(ref _weekEnding, value); }
        }

        public string Role
        {
            get { return _role; }
            set { SetPropertyField(ref _role, value); }
        }

    }
}