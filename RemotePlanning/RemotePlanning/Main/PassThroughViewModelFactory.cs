using RemotePlanning.Main.GameCanvas;

namespace RemotePlanning.Main
{
    public class PassThroughViewModelFactory : IViewModelFactory
    {
        public object Build(object sourceObject)
        {
            return sourceObject;
        }
    }
}