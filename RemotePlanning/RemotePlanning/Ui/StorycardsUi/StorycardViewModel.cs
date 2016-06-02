using System.Runtime.Serialization;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.StorycardsUi
{
    [DataContract]
    public class StorycardViewModel : ViewModel, IStorcardViewModel
    {
        private string _role;
        private string _title;
        private string _content;
        private int _estimate;
        private double _canvasX;
        private double _canvasY;
        private int _zIndex;
        private string _number;

        [DataMember]
        public string Role
        {
            get { return _role; }
            set { SetPropertyField(ref _role, value); }
        }

        [DataMember]
        public string Number
        {
            get { return _number; }
            set { SetPropertyField(ref _number, value); }
        }

        [DataMember]
        public string Title
        {
            get { return _title; }
            set { SetPropertyField(ref _title, value); }
        }

        [DataMember]
        public string Content
        {
            get { return _content; }
            set { SetPropertyField(ref _content, value); }
        }

        [DataMember]
        public int Estimate
        {
            get { return _estimate; }
            set { SetPropertyField(ref _estimate, value); }
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