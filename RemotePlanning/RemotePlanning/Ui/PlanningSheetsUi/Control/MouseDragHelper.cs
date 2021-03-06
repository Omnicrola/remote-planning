﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RemotePlanning.Ui.PlanningSheetsUi.Control
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

        public void MouseDown(MouseButtonEventArgs mouseButtonEventArgs, IMoveableViewModel moveableViewModel)
        {
            _isMouseDown = true;

            moveableViewModel.ZIndex = 999;
            _startPosition = mouseButtonEventArgs.GetPosition(_parentControl);
            mouseButtonEventArgs.Handled = true;
        }

        public void MouseMove(MouseEventArgs mouseEventArgs, IMoveableViewModel moveableViewModel)
        {
            if (_isMouseDown)
            {
                var currentPosition = mouseEventArgs.GetPosition(_parentControl);
                var delta = currentPosition.Delta(_startPosition);
                if (delta.HorizontalChange != 0 || delta.VerticalChange != 0)
                {
                    OnMouseDragDelta(delta, moveableViewModel);
                }
                mouseEventArgs.Handled = true;
            }
        }

        public void MouseUp(MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isMouseDown = false;
            mouseButtonEventArgs.Handled = true;
        }

        private void OnMouseDragDelta(DragDeltaEventArgs delta, IMoveableViewModel moveableViewModel)
        {
            moveableViewModel.CanvasX += delta.HorizontalChange;
            moveableViewModel.CanvasY += delta.VerticalChange;

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