using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using RemotePlanning.Main;
using RemotePlanning.Main.GameCanvas;

namespace RemotePlanning.PlanningSheets.Control
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
            _mouseDragHelper.MouseDrag += Raise_StorycardMoved;
        }


        private void Raise_StorycardMoved(object sender, DragDeltaEventArgs deltaArgs)
        {
            ElementMoved?.Invoke(this, new ElementMovedEventArgs(this, deltaArgs.HorizontalChange, deltaArgs.VerticalChange));
        }

        private void Self_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDragHelper.MouseDown(e);
        }

        private void Self_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseDragHelper.MouseUp(e);
        }

        private void Self_OnMouseMove(object sender, MouseEventArgs e)
        {
            _mouseDragHelper.MouseMove(e);
        }
    }
}
