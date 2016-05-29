﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace RemotePlanning.Storycards
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

    public class BatchStorycardTextParser
    {
        public List<StorycardViewModel> Parse(string text)
        {
            return new List<StorycardViewModel>()
            {
                new StorycardViewModel
                {
                    Estimate = 7,
                    Role = "DEV",
                    Title = "Hello Storycards! 2222"
                },
                new StorycardViewModel
                {
                    Estimate = 8,
                    Role = "DEV",
                    Title = "Hello Storycards!"
                }
            };
        }
    }
}
