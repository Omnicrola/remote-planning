using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RemotePlanning.PlanningSheets.Control
{
    /// <summary>
    /// Interaction logic for PlanningSheetControl.xaml
    /// </summary>
    public partial class PlanningSheetControl
    {
        private readonly MouseDragHelper _mouseDragHelper;

        public event EventHandler<PlanningSheetMovedArgs> PlanningSheetMoved;

        public PlanningSheetControl()
        {
            InitializeComponent();
            _mouseDragHelper = new MouseDragHelper(this);
            _mouseDragHelper.MouseDrag += Raise_StorycardMoved;
        }


        private void Raise_StorycardMoved(object sender, DragDeltaEventArgs deltaArgs)
        {
            PlanningSheetMoved?.Invoke(this, new PlanningSheetMovedArgs(this, deltaArgs.HorizontalChange, deltaArgs.VerticalChange));
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

    public class PlanningSheetMovedArgs : EventArgs
    {
        public PlanningSheetControl PlanningSheetControl { get; private set; }
        public double HorizontalChange { get; private set; }
        public double VerticalChange { get; private set; }

        public PlanningSheetMovedArgs(PlanningSheetControl planningSheetControl, double horizontalChange, double verticalChange)
        {
            PlanningSheetControl = planningSheetControl;
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }
    }
}
