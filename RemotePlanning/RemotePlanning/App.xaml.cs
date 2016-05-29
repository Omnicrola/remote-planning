using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NetworkModel;
using NetworkModel.Networking;
using RemotePlanning.Data;

namespace RemotePlanning
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Start(object sender, StartupEventArgs args)
        {
            var ipString = "127.0.0.1";
            var networkServer = new NetworkServerConnection(ipString);
            new Thread(() => networkServer.Start()).Start();


            var networkClient = new NetworkClientConnection(ipString);
            new Thread(() =>
            {
                Console.WriteLine("Client connecting...");
                networkClient.Connect();

                Console.WriteLine("Sending 1");
                networkClient.SendMessage("Hello world!");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Sending repeat");
                    networkClient.SendMessage("Repeating message: " + i);

                }
                //                networkClient.Close();
            }).Start();



            //            var mainWindow = new MainWindow();
            //            var planningGameManager = new PlanningGameManager(mainWindow);
            //            mainWindow.Show();
        }
    }
}
