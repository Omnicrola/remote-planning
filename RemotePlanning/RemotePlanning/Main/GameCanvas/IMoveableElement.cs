using System;

namespace RemotePlanning.Main.GameCanvas
{
    public interface IMoveableElement
    {
        event EventHandler<ElementMovedEventArgs> ElementMoved;
    }
}