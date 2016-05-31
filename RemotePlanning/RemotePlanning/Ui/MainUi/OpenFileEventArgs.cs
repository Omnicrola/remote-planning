using System;

namespace RemotePlanning.Ui.MainUi
{
    public class OpenFileEventArgs : EventArgs
    {
        public string FileName { get; }

        public OpenFileEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}