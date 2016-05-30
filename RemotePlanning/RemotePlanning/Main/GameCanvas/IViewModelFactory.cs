namespace RemotePlanning.Main.GameCanvas
{
    public interface IViewModelFactory
    {
        object Build(object sourceObject);
    }
}