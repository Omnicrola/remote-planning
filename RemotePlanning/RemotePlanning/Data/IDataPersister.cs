using System.Windows;

namespace RemotePlanning.Data
{
    internal interface IDataPersister
    {
        ApplicationDataStore LoadData();
        void WriteData(ApplicationDataStore applicationData);

    }
}