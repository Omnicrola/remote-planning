using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using RemotePlanning.Ui.MainUi.GameCanvas;

namespace RemotePlanning.Ui.PlanningSheetsUi.Control
{
    /// <summary>
    /// Interaction logic for MovedElement.xaml
    /// </summary>
    public partial class PlanningSheetControl : IMoveableElement
    {
        private readonly MouseDragHelper _mouseDragHelper;

        public event EventHandler<ElementMovedEventArgs> ElementMoved;

        public PlanningSheetControl()
        {
            InitializeComponent();
            _mouseDragHelper = new MouseDragHelper(this);
            _mouseDragHelper.MouseDrag += Raise_PlanningSheetMoved;
        }


        private void Raise_PlanningSheetMoved(object sender, DragDeltaEventArgs deltaArgs)
        {
            ElementMoved?.Invoke(this, new ElementMovedEventArgs(this, deltaArgs.HorizontalChange, deltaArgs.VerticalChange));
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
    }
}
