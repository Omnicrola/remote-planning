using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.ProjectsUi;
using RemotePlanning.Ui.StorycardsUi;

namespace RemotePlanning.Data
{
    public class XmlSerializerFactoy
    {
        public static DataContractSerializer Create()
        {
            IEnumerable<Type> knownTypes = new List<Type>
            {
                typeof(ProjectViewModel),
                typeof(IterationViewModel),
                typeof(StorycardViewModel),
                typeof(PlacedStorycardViewModel)
            };
            return new DataContractSerializer(typeof(ApplicationDataStore), knownTypes, 0x7FFF, false, true, null);
        }

    }
}