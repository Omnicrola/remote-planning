using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RemotePlanning.Storycards.Control
{
    /// <summary>
    /// Interaction logic for StorycardControl.xaml
    /// </summary>
    public partial class StorycardControl : UserControl
    {

        public event EventHandler<StorycardMovedEventArgs> StorycardMoved;

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

            Raise_StorycardMoved(new StorycardMovedEventArgs(this));
        }

        private void Raise_StorycardMoved(StorycardMovedEventArgs storycardMovedEventArgs)
        {
            StorycardMoved?.Invoke(this, storycardMovedEventArgs);
        }
    }

    public class StorycardMovedEventArgs : EventArgs
    {
        public StorycardMovedEventArgs(StorycardControl storycardControl)
        {
            StorycardControl = storycardControl;
        }

        public StorycardControl StorycardControl { get; private set; }
    }
}
