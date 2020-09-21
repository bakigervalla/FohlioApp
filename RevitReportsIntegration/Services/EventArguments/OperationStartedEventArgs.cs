using System;

namespace Fohlio.RevitReportsIntegration.Services.EventArguments
{
    public class OperationStartedEventArgs : EventArgs
    {
        public OperationStartedEventArgs(string operationName, int count)
        {
            OperationName = operationName;

            Count = count;
        }

        public string OperationName { get; }

        public int Count { get; }
    }
}