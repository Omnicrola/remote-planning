using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Media;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.PlanningSheetsUi
{
    [DataContract]
    public class PlanningSheetViewModel : ViewModel, IMoveableViewModel
    {
        private string _role;
        private SolidColorBrush _color;

        private bool _isNotCurrentlySorting;
        private double _canvasX;
        private double _canvasY;
        private int _zIndex;

        public PlanningSheetViewModel()
        {
            Color = Brushes.Green;
            PlannedCards = new ObservableCollection<PlacedStorycardViewModel>();
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

        [DataMember]
        public ObservableCollection<PlacedStorycardViewModel> PlannedCards { get; private set; }

        [DataMember]
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

        [DataMember]
        public Color HexColor
        {
            get { return Color.Color; }
            set { Color = new SolidColorBrush(value); }
        }

        [DataMember]
        public double CanvasX
        {
            get { return _canvasX; }
            set { SetPropertyField(ref _canvasX, value); }
        }

        [DataMember]
        public double CanvasY
        {
            get { return _canvasY; }
            set { SetPropertyField(ref _canvasY, value); }
        }

        [DataMember]
        public int ZIndex
        {
            get { return _zIndex; }
            set { SetPropertyField(ref _zIndex, value); }
        }
    }
}