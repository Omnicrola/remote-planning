using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using RemotePlanning.Ui.MainUi.GameCanvas;

namespace RemotePlanning.Ui.StorycardsUi.Control
{
    /// <summary>
    /// Interaction logic for StorycardControl.xaml
    /// </summary>
    public partial class StorycardControl : IMoveableElement
    {

        public event EventHandler<ElementMovedEventArgs> ElementMoved;

        public StorycardControl()
        {
            InitializeComponent();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var newLeft = Canvas.GetLeft(this) + e.HorizontalChange;
            var newTop = Canvas.GetTop(this) + e.VerticalChange;

            Canvas.SetLeft(this, newLeft);
            Canvas.SetTop(this, newTop);

            Raise_StorycardMoved(new ElementMovedEventArgs(this, e.HorizontalChange, e.VerticalChange));
        }

        private void Raise_StorycardMoved(ElementMovedEventArgs storycardMovedEventArgs)
        {
            ElementMoved?.Invoke(this, storycardMovedEventArgs);
        }
    }

}
