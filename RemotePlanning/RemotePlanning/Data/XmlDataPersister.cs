using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml.Serialization;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.ProjectsUi;

namespace RemotePlanning.Data
{
    public class XmlDataPersister : IDataPersister
    {
        private readonly DataContractSerializer _xmlSerializer;
        private string _dataFileLocation;

        public XmlDataPersister(DataContractSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;
            _dataFileLocation = ConfigurationManager.AppSettings["DataFileLocation"];
        }

        public ApplicationDataStore LoadData()
        {
            if (File.Exists(_dataFileLocation))
            {
                using (var fileStream = File.Open(_dataFileLocation, FileMode.Open, FileAccess.Read))
                {
                    ApplicationDataStore dataStore = (ApplicationDataStore)_xmlSerializer.ReadObject(fileStream);
                    return dataStore;
                }
            }
            return new ApplicationDataStore { Project = new ProjectViewModel(), SelectedIteration = new IterationViewModel() };
        }

        public void WriteData(ApplicationDataStore applicationData)
        {
            FileStream fileStream;
            if (File.Exists(_dataFileLocation))
            {
                fileStream = File.Open(_dataFileLocation, FileMode.Truncate, FileAccess.Write);
            }
            else
            {
                fileStream = File.Open(_dataFileLocation, FileMode.OpenOrCreate, FileAccess.Write);
            }
            using (fileStream)
            {
                _xmlSerializer.WriteObject(fileStream, applicationData);
            }
        }
    }
}