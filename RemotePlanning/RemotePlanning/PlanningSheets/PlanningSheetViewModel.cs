using System;
using System.Windows.Media;
using RemotePlanning.ViewModels;

namespace RemotePlanning.PlanningSheets
{
    public class PlanningSheetViewModel : ViewModel
    {
        private string _role;
        private SolidColorBrush _color;


        public PlanningSheetViewModel()
        {
            Color = Brushes.White;
        }
        public string Role
        {
            get { return _role; }
            set { SetPropertyField(ref _role, value); }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { SetPropertyField(ref _color, value); }
        }
    }
}