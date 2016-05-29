using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using RemotePlanning.Storycards;
using RemotePlanning.ViewModels;

namespace RemotePlanning.PlanningSheets
{
    public class PlanningSheetViewModel : ViewModel
    {
        private string _role;
        private SolidColorBrush _color;

        private bool _isNotCurrentlySorting;


        public PlanningSheetViewModel()
        {
            Color = Brushes.Green;
            PlannedCards = new ObservableCollection<PlacedStorycardViewModel>();
            //            PlannedCards.CollectionChanged += SortPlannedCards;
            //            _isNotCurrentlySorting = true;
        }

        private void SortPlannedCards(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isNotCurrentlySorting)
            {
                _isNotCurrentlySorting = false;
                List<PlacedStorycardViewModel> sortedList = PlannedCards.OrderBy(x => x).ToList();
                PlannedCards.Clear();
                sortedList.ForEach(x => PlannedCards.Add(x));
                _isNotCurrentlySorting = true;
            }

        }
        public ObservableCollection<PlacedStorycardViewModel> PlannedCards { get; private set; }
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