using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RemotePlanning.PlanningSheets.Control
{
    public class MouseDragHelper
    {
        private readonly UserControl _parentControl;
        private bool _isMouseDown;
        private Point _startPosition;

        public MouseDragHelper(UserControl parentControl)
        {
            _parentControl = parentControl;
        }

        public event EventHandler<DragDeltaEventArgs> MouseDrag;

        public void MouseDown(MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isMouseDown = true;
            _startPosition = mouseButtonEventArgs.GetPosition(_parentControl);
        }

        public void MouseMove(MouseEventArgs mouseEventArgs)
        {
            if (_isMouseDown)
            {
                var currentPosition = mouseEventArgs.GetPosition(_parentControl);
                var delta = currentPosition.Delta(_startPosition);
                if (delta.HorizontalChange != 0 || delta.VerticalChange != 0)
                {
                    OnMouseDragDelta(delta);
                }
            }
        }

        public void MouseUp(MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isMouseDown = false;
        }

        private void OnMouseDragDelta(DragDeltaEventArgs delta)
        {
            var newLeft = Canvas.GetLeft(_parentControl) + delta.HorizontalChange;
            var newTop = Canvas.GetTop(_parentControl) + delta.VerticalChange;

            Canvas.SetLeft(_parentControl, newLeft);
            Canvas.SetTop(_parentControl, newTop);

            Raise_MovedEvent(delta);
        }

        private void Raise_MovedEvent(DragDeltaEventArgs eventArgs)
        {
            MouseDrag?.Invoke(_parentControl, eventArgs);
        }

    }

    public static class MouseDeltaExtension
    {
        public static DragDeltaEventArgs Delta(this Point point, Point otherPoint)
        {
            return new DragDeltaEventArgs(point.X - otherPoint.X, point.Y - otherPoint.Y);
        }
    }
}