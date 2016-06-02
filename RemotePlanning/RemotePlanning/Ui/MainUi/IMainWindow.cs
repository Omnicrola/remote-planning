using System;
using System.Windows;
using RemotePlanning.Network;

namespace RemotePlanning.Ui.MainUi
{
    public interface IMainWindow
    {
        event EventHandler<RoutedEventArgs> WindowLoaded;
        event EventHandler<EventArgs> WindowClosed;
        event EventHandler<NetworkConnectEventArgs> NetworkConnect;
        event EventHandler<NetworkHostEventArgs> HostNetworkSession;
        event EventHandler<FileEventArgs> OpenProject;
        event EventHandler<FileEventArgs> SaveProject;

        MainWindowViewModel ViewModel { get; }
        void AddStatusMessage(string message);
    }
}