using System;

namespace RemotePlanning.Ui.MainUi
{
    public class FileEventArgs : EventArgs
    {
        public string FileName { get; }

        public FileEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}