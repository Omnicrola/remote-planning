using System.ComponentModel;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.StorycardsUi
{
    public class PlacedStorycardViewModel : ViewModel, IStorcardViewModel
    {
        private StorycardViewModel wrappedStorycard;

        public PlacedStorycardViewModel(StorycardViewModel wrappedStorycard)
        {
            this.wrappedStorycard = wrappedStorycard;
            wrappedStorycard.PropertyChanged += PropertyChangePassThrough;
        }

        private void PropertyChangePassThrough(object sender, PropertyChangedEventArgs e)
        {
            FireOnPropertyChanged(e.PropertyName);
        }

        public int Order { get; set; }

        public string Role { get { return wrappedStorycard.Role; } set { } }
        public string Title { get { return wrappedStorycard.Title; } set { } }
        public string Content { get { return wrappedStorycard.Content; } set { } }
        public int Estimate { get { return wrappedStorycard.Estimate; } set { } }
    }

}