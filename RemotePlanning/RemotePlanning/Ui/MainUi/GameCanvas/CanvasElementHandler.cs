using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RemotePlanning.Ui.MainUi.GameCanvas
{
    public class CanvasElementHandler<T> where T : Control, IMoveableElement
    {
        private readonly Canvas _canvas;
        private readonly int _minIndex;
        private readonly int _maxIndex;

        public CanvasElementHandler(Canvas canvas, int minIndex, int maxIndex)
        {
            _canvas = canvas;
            _minIndex = minIndex;
            _maxIndex = maxIndex;
        }

        public void OnCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (object newItem in e.NewItems)
            {
                AddToCanvas(newItem);
            }
        }

        public void AddToCanvas(object obj)
        {
            var control = Activator.CreateInstance<T>();
            control.DataContext = obj;
            control.ElementMoved += ReorderZIndexes;
            Canvas.SetTop(control, new Random().Next(10, 500));
            Canvas.SetLeft(control, new Random().Next(10, 500));
            _canvas.Children.Add(control);

        }

        public void Clear()
        {
            GetAllElementsOfType()
                .ForEach(c =>
                {
                    c.ElementMoved -= ReorderZIndexes;
                    _canvas.Children.Remove(c);
                });

        }

        private List<T> GetAllElementsOfType()
        {
            return _canvas.Children
                .Cast<UIElement>()
                .OfType<T>()
                .ToList();
        }

        public void ReorderZIndexes(object sender, ElementMovedEventArgs e)
        {
            var lastControlToMove = e.MovedElement;
            int zIndex = _minIndex;
            GetAllElementsOfType()
                .ForEach(c =>
                {
                    if (c == lastControlToMove)
                    {
                        Canvas.SetZIndex(c, _maxIndex);
                    }
                    else
                    {
                        Canvas.SetZIndex(c, zIndex++);
                    }
                });
        }
    }
}