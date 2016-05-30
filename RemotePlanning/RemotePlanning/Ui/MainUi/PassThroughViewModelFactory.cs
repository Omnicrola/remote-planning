using RemotePlanning.Ui.MainUi.GameCanvas;

namespace RemotePlanning.Ui.MainUi
{
    public class PassThroughViewModelFactory : IViewModelFactory
    {
        public object Build(object sourceObject)
        {
            return sourceObject;
        }
    }
}