using System;

namespace RemotePlanning.Operations.Async
{
    internal interface IDiscreetAsyncOperation
    {
        event EventHandler<OperationEventArgs> OperationStatus;

        void DoWork();

        string Description { get; }

    }
}