using System;

namespace RemotePlanning.Ui.MainUi.GameCanvas
{
    public class ElementMovedEventArgs : EventArgs
    {
        public IMoveableElement MovedElement { get; private set; }
        public double HorizontalChange { get; private set; }
        public double VerticalChange { get; private set; }

        public ElementMovedEventArgs(IMoveableElement movedElement, double horizontalChange, double verticalChange)
        {
            MovedElement = movedElement;
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }
    }
}