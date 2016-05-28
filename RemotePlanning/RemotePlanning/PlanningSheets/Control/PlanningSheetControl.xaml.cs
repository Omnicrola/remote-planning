using System;
using System.Windows.Controls;
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
            PlanningSheetMoved?.Invoke(this, new PlanningSheetMovedArgs());
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
    }
}
