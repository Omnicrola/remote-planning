using System;
using System.Windows.Threading;

namespace RemotePlanning.Commands
{
    internal interface IDiscreetOperation
    {
        event EventHandler<OperationEventArgs> OperationStarted;
        event EventHandler<OperationEventArgs> OperationFinished;

        void DoWork(Dispatcher mainThreadDispatcher);

        string Description { get; }

    }
}