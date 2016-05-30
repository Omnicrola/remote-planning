using System;

namespace RemotePlanning.Ui.MainUi.GameCanvas
{
    public interface IMoveableElement
    {
        event EventHandler<ElementMovedEventArgs> ElementMoved;
    }
}