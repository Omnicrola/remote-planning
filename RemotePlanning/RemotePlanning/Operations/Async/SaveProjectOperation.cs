using System;
using System.IO;
using System.Runtime.Serialization;
using RemotePlanning.Data;
using RemotePlanning.Operations.Synchronous;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Operations.Async
{
    internal class SaveProjectOperation : IDiscreetSynchronousOperation
    {
        private readonly string _fileName;
        private readonly ViewModelParser _viewModelParser;
        private readonly DataContractSerializer _dataContractSerializer;
        public string Description => "Saving project";
        public event EventHandler<OperationEventArgs> OperationStatus;

        public SaveProjectOperation(string fileName, ViewModelParser viewModelParser, DataContractSerializer dataContractSerializer)
        {
            _fileName = fileName;
            _viewModelParser = viewModelParser;
            _dataContractSerializer = dataContractSerializer;
        }

        public void DoWork()
        {
            using (var fileStream = File.Open(_fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var applicationData = _viewModelParser.ExtractData();
                _dataContractSerializer.WriteObject(fileStream, applicationData);

                OperationStatus?.Invoke(this, new OperationEventArgs("Project saved!"));
            }
        }

    }
}