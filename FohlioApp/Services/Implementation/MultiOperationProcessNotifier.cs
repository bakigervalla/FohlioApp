using System;
using Fohlio.RevitReportsIntegration.Views.BackgroundWorker;

namespace Fohlio.RevitReportsIntegration.Services.Implementation
{
    public class MultiOperationProcessNotifier : IMultiOperationProcessNotifier
    {
        private MultiOperationProgressBackgroundWorker worker;
        private int totalOperationsCount;
        private int currentOperation;
        private int totalInternalCalculationsCount;
        private int currentInternalOperation;
        private int currentInternalOperationPercent;

        public event EventHandler OperationCancelRequested;

        public void Start(int totalCount, string title, IntPtr mainWindowHandle)
        {
            currentOperation = 0;

            totalOperationsCount = totalCount;

            worker = new MultiOperationProgressBackgroundWorker(totalOperationsCount, mainWindowHandle, false);

            worker.SetTitle(title);

            worker.OperationCancelRequested += OnOperationCancelRequested;
        }

        public void NotifyNext()
        {
            currentOperation = Math.Min(currentOperation + 1, totalOperationsCount);

            worker.ReportProgress(currentOperation);
        }

        public void StartInternalOperation(int totalCalculationsCount, string operationName)
        {
            totalInternalCalculationsCount = totalCalculationsCount;

            currentInternalOperation = 0;

            currentInternalOperationPercent = 0;

            worker.SetInternalOperation(operationName);

            worker.ReportInternalOperationProgress(0);
        }

        public void NotifyNextInternalOperationStep()
        {
            currentInternalOperation = Math.Min(currentInternalOperation + 1, totalInternalCalculationsCount);

            var currentPercent = 100 * currentInternalOperation / totalInternalCalculationsCount;

            if (currentPercent - currentInternalOperationPercent > 4)
            {
                currentInternalOperationPercent = currentPercent;
                worker.ReportInternalOperationProgress(currentPercent);
            }
        }

        public void NotifyInternalOperationProgressValue(int value)
        {
            currentInternalOperation = Math.Min(value, totalInternalCalculationsCount);

            currentInternalOperationPercent = 100 * currentInternalOperation / totalInternalCalculationsCount;

            worker.ReportInternalOperationProgress(currentInternalOperationPercent);
        }

        public void NotifyEarlyInternalOperationComplete()
        {
            worker.ReportInternalOperationProgress(100);
        }

        public void Stop()
        {
            worker.OperationCancelRequested -= OnOperationCancelRequested;

            worker.Stop();
        }

        private void OnOperationCancelRequested(object sender, EventArgs e)
        {
            OperationCancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}