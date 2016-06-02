using System;
using System.Collections.Generic;
using System.Windows;

namespace RemotePlanning.Ui.StorycardsUi
{
    /// <summary>
    /// Interaction logic for StorycardBatchLoadWindow.xaml
    /// </summary>
    public partial class StorycardBatchLoadWindow : Window
    {
        private BatchStorycardTextParser _batchStorycardTextParser;
        public event EventHandler<StorycardCreatedEventArgs> StorycardCreated;

        public StorycardBatchLoadWindow()
        {
            InitializeComponent();
            _batchStorycardTextParser = new BatchStorycardTextParser();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            List<StorycardViewModel> storycards = _batchStorycardTextParser.Parse(StorycardText.Text);
            RaiseStorycardCreateEvent(storycards);
            Close();
        }

        private void RaiseStorycardCreateEvent(List<StorycardViewModel> storycards)
        {
            StorycardCreated?.Invoke(this, new StorycardCreatedEventArgs(storycards));
        }
    }

    public class StorycardCreatedEventArgs : EventArgs
    {
        public List<StorycardViewModel> Storycards { get; private set; }

        public StorycardCreatedEventArgs(List<StorycardViewModel> storycards)
        {
            Storycards = storycards;
        }
    }
}
