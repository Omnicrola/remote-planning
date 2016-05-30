using System;
using System.Net.NetworkInformation;
using System.Windows;

namespace RemotePlanning.Main
{
    public interface IMainWindow
    {
        event EventHandler<RoutedEventArgs> WindowLoaded;
        event EventHandler<NetworkConnectEventArgs> NetworkConnect;
        MainWindowViewModel ViewModel { get; }
    }
}