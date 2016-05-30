using System;
using System.Net.NetworkInformation;
using System.Windows;
using RemotePlanning.Network;

namespace RemotePlanning.Main
{
    public interface IMainWindow
    {
        event EventHandler<RoutedEventArgs> WindowLoaded;
        event EventHandler<NetworkConnectEventArgs> NetworkConnect;
        event EventHandler<NetworkHostEventArgs> HostNetworkSession;
        MainWindowViewModel ViewModel { get; }
    }
}