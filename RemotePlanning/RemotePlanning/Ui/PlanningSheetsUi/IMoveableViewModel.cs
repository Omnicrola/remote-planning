namespace RemotePlanning.Ui.PlanningSheetsUi
{
    public interface IMoveableViewModel
    {
        double CanvasX { get; set; }
        double CanvasY { get; set; }
        int ZIndex { get; set; }
    }
}