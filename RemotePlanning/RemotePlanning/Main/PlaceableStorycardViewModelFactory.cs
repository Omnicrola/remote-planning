using RemotePlanning.Main.GameCanvas;
using RemotePlanning.Storycards;

namespace RemotePlanning.Main
{
    public class PlaceableStorycardViewModelFactory : IViewModelFactory
    {
        public object Build(object sourceObject)
        {
            return new PlacedStorycardViewModel((StorycardViewModel)sourceObject);
        }
    }
}