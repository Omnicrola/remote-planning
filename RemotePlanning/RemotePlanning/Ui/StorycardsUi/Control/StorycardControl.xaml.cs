using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using RemotePlanning.Ui.MainUi.GameCanvas;
using RemotePlanning.Ui.PlanningSheetsUi;
using RemotePlanning.Ui.PlanningSheetsUi.Control;

namespace RemotePlanning.Ui.StorycardsUi.Control
{
    /// <summary>
    /// Interaction logic for StorycardControl.xaml
    /// </summary>
    public partial class StorycardControl : IMoveableElement
    {
        private MouseDragHelper _mouseDragHelper;

        public event EventHandler<ElementMovedEventArgs> ElementMoved;

        public StorycardControl()
        {
            InitializeComponent();
            _mouseDragHelper = new MouseDragHelper(this);
            _mouseDragHelper.MouseDrag += Raise_StorycardMoved;
        }

        private void Self_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDragHelper.MouseDown(e, DataContext as IMoveableViewModel);
        }

        private void Self_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseDragHelper.MouseUp(e);
        }

        private void Self_OnMouseMove(object sender, MouseEventArgs e)
        {
            _mouseDragHelper.MouseMove(e, DataContext as IMoveableViewModel);
        }

        private void Raise_StorycardMoved(object sender, DragDeltaEventArgs deltaArgs)
        {
            ElementMoved?.Invoke(this, new ElementMovedEventArgs(this, deltaArgs.HorizontalChange, deltaArgs.VerticalChange));
        }
    }

}
