using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using RemotePlanning.Operations.Async;
using RemotePlanning.Operations.Synchronous;

namespace RemotePlanning.Operations
{
    internal class OperationsQueue
    {
        private readonly object MUTEX = new object();

        private readonly Queue<IDiscreetAsyncOperation> _operationsToDo = new Queue<IDiscreetAsyncOperation>();
        private readonly Queue<IDiscreetAsyncOperation> _newOperations = new Queue<IDiscreetAsyncOperation>();
        private readonly Queue<IDiscreetSynchronousOperation> _synchronousOperations = new Queue<IDiscreetSynchronousOperation>();

        private readonly Dispatcher _mainThreadDispatcher;
        private readonly Thread _thread;
        private bool _isRunning;

        public event EventHandler<OperationEventArgs> OperationStatus;


        public OperationsQueue(Dispatcher mainThreadDispatcher)
        {
            _mainThreadDispatcher = mainThreadDispatcher;
            _isRunning = true;
            _thread = new Thread(ProcessQueue);
            _thread.Name = "Operations Queue";
            _thread.Priority = ThreadPriority.BelowNormal;
            _thread.Start();

        }

        public void AddOperation(IDiscreetAsyncOperation asyncOperation)
        {
            lock (MUTEX)
            {
                _newOperations.Enqueue(asyncOperation);
            }
        }
        public void AddOperation(IDiscreetSynchronousOperation syncOperation)
        {
            lock (MUTEX)
            {
                _synchronousOperations.Enqueue(syncOperation);
            }
        }

        private void ProcessQueue()
        {
            while (_isRunning)
            {
                LoadNewOperationsIntoQueue();
                ProcessNextTask();
                Thread.Sleep(25);
            }
        }

        private void LoadNewOperationsIntoQueue()
        {
            lock (MUTEX)
            {
                while (_newOperations.Count > 0)
                {
                    _operationsToDo.Enqueue(_newOperations.Dequeue());
                }
                while (_synchronousOperations.Count > 0)
                {
                    _mainThreadDispatcher.Invoke(() => ExecuteSyncTask(_synchronousOperations.Dequeue()));
                }
            }
        }

        private void ExecuteSyncTask(IDiscreetSynchronousOperation operation)
        {
            operation.OperationStatus += SendStatusUpdate;
            operation.DoWork();
            operation.OperationStatus -= SendStatusUpdate;
        }

        private void ProcessNextTask()
        {
            if (_operationsToDo.Count > 0)
            {
                var currentOperation = _operationsToDo.Dequeue();
                currentOperation.OperationStatus += SendStatusUpdate;
                currentOperation.DoWork();
                currentOperation.OperationStatus -= SendStatusUpdate;
            }

        }

        private void SendStatusUpdate(object sender, OperationEventArgs e)
        {
            _mainThreadDispatcher.InvokeAsync(() => OperationStatus?.Invoke(sender, e));
        }

        public void Dispose()
        {
            _isRunning = false;
            _thread.Abort();
        }


    }
}

