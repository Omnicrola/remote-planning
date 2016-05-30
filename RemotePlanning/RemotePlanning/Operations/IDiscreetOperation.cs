using System;
using System.Windows.Threading;

namespace RemotePlanning.Operations
{
    internal interface IDiscreetOperation
    {
        event EventHandler<OperationEventArgs> OperationStatus;

        void DoWork();

        string Description { get; }

    }
}