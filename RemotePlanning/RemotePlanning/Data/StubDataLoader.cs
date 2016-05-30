using System;
using System.Windows;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.MainUi;
using RemotePlanning.Ui.PlanningSheetsUi;
using RemotePlanning.Ui.ProjectsUi;
using RemotePlanning.Ui.StorycardsUi;

namespace RemotePlanning.Data
{
    internal class StubDataLoader : IDataPersister
    {

        public StubDataLoader()
        {
        }

        public ApplicationDataStore LoadData()
        {
            return new ApplicationDataStore();

        }

        public void WriteData(ApplicationDataStore applicationData)
        {

        }
    }
}