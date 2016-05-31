using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Win32;
using RemotePlanning.Data;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Operations.Synchronous
{
    internal class OpenProjectOperation : IDiscreetSynchronousOperation
    {
        private readonly string _filename;
        private readonly ViewModelParser _viewModelParser;
        private readonly DataContractSerializer _xmlSerializer;

        public event EventHandler<OperationEventArgs> OperationStatus;
        public string Description => "Open project file";
        public bool IsAsync => true;

        public OpenProjectOperation(string filename, ViewModelParser viewModelParser, DataContractSerializer xmlSerializer)
        {
            _filename = filename;
            _viewModelParser = viewModelParser;
            _xmlSerializer = xmlSerializer;
        }

        public void DoWork()
        {
            try
            {
                var fileStream = File.Open(_filename, FileMode.Open, FileAccess.Read);
                ApplicationDataStore applicationData = (ApplicationDataStore)_xmlSerializer.ReadObject(fileStream);
                _viewModelParser.ClearAndLoad(applicationData);
                OperationStatus?.Invoke(this, new OperationEventArgs("Project loaded!"));
            }
            catch (Exception e)
            {
                Console.WriteLine("OOps");
            }
        }

    }
}