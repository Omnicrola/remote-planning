using RemotePlanning.Ui.MainUi.GameCanvas;
using RemotePlanning.Ui.StorycardsUi;

namespace RemotePlanning.Ui.MainUi
{
    public class PlaceableStorycardViewModelFactory : IViewModelFactory
    {
        public object Build(object sourceObject)
        {
            return new PlacedStorycardViewModel((StorycardViewModel)sourceObject);
        }
    }
}