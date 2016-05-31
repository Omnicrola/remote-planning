using System;
using RemotePlanning.Ui.MainUi;

namespace RemotePlanning.Operations.Synchronous
{
    internal interface IDiscreetSynchronousOperation
    {
        event EventHandler<OperationEventArgs> OperationStatus;

        void DoWork();

        string Description { get; }

    }
}