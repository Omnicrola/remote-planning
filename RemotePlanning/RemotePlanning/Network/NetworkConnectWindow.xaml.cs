using System;
using System.Windows;
using RemotePlanning.Ui.MainUi;

namespace RemotePlanning.Network
{
    /// <summary>
    /// Interaction logic for NetworkConnectWindow.xaml
    /// </summary>
    public partial class NetworkConnectWindow : Window
    {
        public event EventHandler<NetworkConnectEventArgs> ConnectToServer;
        public NetworkConnectWindow()
        {
            InitializeComponent();
        }

        private void Connect_OnClick(object sender, RoutedEventArgs e)
        {
            var networkConnectEventArgs = new NetworkConnectEventArgs(AddressTextbox.Text);
            ConnectToServer?.Invoke(this, networkConnectEventArgs);
            Close();
        }
    }
}
