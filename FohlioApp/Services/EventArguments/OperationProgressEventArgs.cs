using System;

namespace Fohlio.RevitReportsIntegration.Services.EventArguments
{
    public class OperationProgressEventArgs : EventArgs
    {
        public OperationProgressEventArgs(int current, int count)
        {
            Current = current;

            Count = count;
        }

        public int Current { get; }

        public int Count { get; }
    }
}