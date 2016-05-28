using System.ComponentModel;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Storycards
{
    public class StorycardViewModel : ViewModel, IStorcardViewModel
    {
        private string _role;
        private string _title;
        private string _content;
        private int _estimate;

        public string Role
        {
            get { return _role; }
            set { SetPropertyField(ref _role, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetPropertyField(ref _title, value); }
        }

        public string Content
        {
            get { return _content; }
            set { SetPropertyField(ref _content, value); }
        }

        public int Estimate
        {
            get { return _estimate; }
            set { SetPropertyField(ref _estimate, value); }
        }
    }
}