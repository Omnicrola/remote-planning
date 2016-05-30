using System;
using System.Windows;
using RemotePlanning.Ui.MainUi;

namespace RemotePlanning.Ui.IterationUi
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
