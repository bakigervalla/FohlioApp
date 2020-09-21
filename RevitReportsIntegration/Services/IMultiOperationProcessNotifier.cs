using System;

namespace Fohlio.RevitReportsIntegration.Services
{
    public interface IMultiOperationProcessNotifier
    {
        event EventHandler OperationCancelRequested;

        void Start(int totalCount, string title, IntPtr mainWindowHandle);

        void NotifyNext();

        void StartInternalOperation(int totalCalculationsCount, string operationName);

        void NotifyNextInternalOperationStep();

        void NotifyInternalOperationProgressValue(int value);

        void NotifyEarlyInternalOperationComplete();

        void Stop();
    }
}