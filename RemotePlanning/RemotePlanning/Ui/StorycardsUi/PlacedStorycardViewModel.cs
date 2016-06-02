using System.ComponentModel;
using System.Runtime.Serialization;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.StorycardsUi
{
    [DataContract]
    public class PlacedStorycardViewModel : ViewModel, IStorcardViewModel
    {
        [DataMember]
        private StorycardViewModel _wrappedStorycard;

        private double _canvasX;
        private double _canvasY;
        private int _zIndex;

        public PlacedStorycardViewModel(StorycardViewModel wrappedStorycard)
        {
            this._wrappedStorycard = wrappedStorycard;
            wrappedStorycard.PropertyChanged += PropertyChangePassThrough;
        }

        private void PropertyChangePassThrough(object sender, PropertyChangedEventArgs e)
        {
            FireOnPropertyChanged(e.PropertyName);
        }


        [DataMember]
        public int Order { get; set; }

        public string Role { get { return _wrappedStorycard.Role; } set { } }
        public string Number { get { return _wrappedStorycard.Number; } set { } }
        public string Title { get { return _wrappedStorycard.Title; } set { } }
        public string Content { get { return _wrappedStorycard.Content; } set { } }
        public int Estimate { get { return _wrappedStorycard.Estimate; } set { } }

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