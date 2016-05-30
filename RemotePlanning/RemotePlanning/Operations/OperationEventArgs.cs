namespace RemotePlanning.Operations
{
    public class OperationEventArgs
    {
        public string Message { get; private set; }

        public OperationEventArgs(string message)
        {
            Message = message;
        }
    }
}