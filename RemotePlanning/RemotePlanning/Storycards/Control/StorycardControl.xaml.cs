using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemotePlanning.Storycards.UserControl
{
    /// <summary>
    /// Interaction logic for Storycard.xaml
    /// </summary>
    public partial class Storycard : System.Windows.Controls.UserControl
    {

        public event EventHandler<StorycardMovedEventArgs> StorycardMoved;

        public Storycard()
        {
            InitializeComponent();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var newLeft = Canvas.GetLeft(this) + e.HorizontalChange;
            var newTop = Canvas.GetTop(this) + e.VerticalChange;

            Canvas.SetLeft(this, newLeft);
            Canvas.SetTop(this, newTop);

            Raise_StorycardMoved(new StorycardMovedEventArgs());
        }

        private void Raise_StorycardMoved(StorycardMovedEventArgs storycardMovedEventArgs)
        {
            StorycardMoved?.Invoke(this, storycardMovedEventArgs);
        }
    }

    public class StorycardMovedEventArgs : EventArgs
    {
    }
}
