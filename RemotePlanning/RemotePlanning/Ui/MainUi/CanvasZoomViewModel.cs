using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RemotePlanning.Ui.PlanningSheetsUi.Control;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Ui.MainUi
{
    public class CanvasZoomViewModel : ViewModel
    {
        private const double SCALE_RATE = 1.1;

        private double _scaleX;
        private double _scaleY;
        private double _offsetX;
        private double _offsetY;
        private bool _isDraggingCanvas;
        private Point _lastMouseDragPosition;

        public CanvasZoomViewModel()
        {
            ScaleX = 1;
            ScaleY = 1;
        }

        public double ScaleX
        {
            get { return _scaleX; }
            set { SetPropertyField(ref _scaleX, value); }
        }

        public double ScaleY
        {
            get { return _scaleY; }
            set { SetPropertyField(ref _scaleY, value); }
        }

        public double OffsetX
        {
            get { return _offsetX; }
            set { SetPropertyField(ref _offsetX, value); }
        }

        public double OffsetY
        {
            get { return _offsetY; }
            set { SetPropertyField(ref _offsetY, value); }
        }


        public void AdjustScale(ScrollViewer zoomContainer, MouseWheelEventArgs mouseArgs)
        {
            var mousePosition = mouseArgs.GetPosition(zoomContainer);
            double horizontalPercent = mousePosition.X / zoomContainer.ViewportWidth;
            double verticalPercentage = mousePosition.Y / zoomContainer.ViewportHeight;

            var deltaX = (zoomContainer.ViewportWidth * SCALE_RATE) - zoomContainer.ViewportWidth;
            var deltaY = (zoomContainer.ViewportHeight * SCALE_RATE) - zoomContainer.ViewportHeight;

            var offsetIncrmentX = deltaX * horizontalPercent;
            var offsetIncrmentY = deltaY * verticalPercentage;
            if (mouseArgs.Delta > 0)
            {
                ScaleX *= SCALE_RATE;
                ScaleY *= SCALE_RATE;
                OffsetX -= offsetIncrmentX;
                OffsetY -= offsetIncrmentY;

            }
            else
            {
                ScaleX /= SCALE_RATE;
                ScaleY /= SCALE_RATE;
                OffsetX += offsetIncrmentX;
                OffsetY += offsetIncrmentY;
            }
        }

        public void Reset()
        {
            ScaleX = 1.0;
            ScaleY = 1.0;
            OffsetX = 0;
            OffsetY = 0;
        }

        public void MouseMove(ScrollViewer zoomContainer, MouseEventArgs mouseEventArgs)
        {
            if (_isDraggingCanvas)
            {
                var newPosition = mouseEventArgs.GetPosition(zoomContainer);
                var delta = newPosition.Delta(_lastMouseDragPosition);
                OffsetX += delta.HorizontalChange;
                OffsetY += delta.VerticalChange;
                _lastMouseDragPosition = newPosition;
            }
        }

        public void MouseUp(ScrollViewer zoomContainer, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isDraggingCanvas = false;
        }

        public void MouseDown(ScrollViewer zoomContainer, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isDraggingCanvas = true;
            _lastMouseDragPosition = mouseButtonEventArgs.GetPosition(zoomContainer);
        }
    }
}