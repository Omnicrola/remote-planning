using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RemotePlanning.Main;

namespace RemotePlanning.Iteration
{
    /// <summary>
    /// Interaction logic for SelectIterationWindow.xaml
    /// </summary>
    public partial class SelectIterationWindow : Window
    {
        private SelectIterationViewModel selectIterationViewModel;

        public event EventHandler<SelectIterationEventArgs> SelectIteration;

        public SelectIterationWindow(SelectIterationViewModel selectIterationViewModel)
        {
            InitializeComponent();
            this.selectIterationViewModel = selectIterationViewModel;
            DataContext = selectIterationViewModel;
        }


        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Select_OnClick(object sender, RoutedEventArgs e)
        {
            var eventArgs = new SelectIterationEventArgs(selectIterationViewModel.SelectedIteration);
            SelectIteration?.Invoke(this, eventArgs);
            Close();
        }
    }

    public class SelectIterationEventArgs
    {
        public IterationViewModel SelectedIteration { get; private set; }

        public SelectIterationEventArgs(IterationViewModel selectedIteration)
        {
            SelectedIteration = selectedIteration;
        }
    }
}
