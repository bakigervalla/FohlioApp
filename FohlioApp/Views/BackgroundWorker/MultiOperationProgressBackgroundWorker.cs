using System;
using System.Threading;
using System.Threading.Tasks;
using Fohlio.RevitReportsIntegration.Utils.Native;

namespace Fohlio.RevitReportsIntegration.Views.BackgroundWorker
{
    public class MultiOperationProgressBackgroundWorker
    {
        private MultiOperationFormProgress formProgress;
        private readonly object locker = new object();
        private DateTime formCreated;

        public MultiOperationProgressBackgroundWorker(int maximumProgress, IntPtr parentWindowHandle, bool supportsCancel)
        {
            if (maximumProgress == 0)
                return;

            ThreadPool.QueueUserWorkItem(state => CreateFormProgress(maximumProgress, supportsCancel, parentWindowHandle));

            lock (locker)
                while (formProgress == null)
                    Monitor.Wait(locker, TimeSpan.FromMilliseconds(100));
        }

        public bool IsUserCanceled => formProgress.IsCancellingInvoked;

        public event EventHandler OperationCancelRequested;

        public void ReportProgress(int value)
        {
            formProgress.ProgressValue = value;
        }

        public void ReportInternalOperationProgress(int value)
        {
            formProgress.InternalOperationProgressValue = value;
        }

        public void Stop()
        {
            CloseProgressForm();
        }

        public void SetTitle(string title)
        {
            formProgress.SetTitle(title);
        }

        public void SetInternalOperation(string name)
        {
            formProgress.SetInternalOperationName(name);
        }

        public void DoWork(Action action)
        {
            var task = Task.Factory.StartNew(action);

            task.ContinueWith(task1 => CloseProgressForm());
            task.Wait();
        }

        private void CreateFormProgress(int maxProgress, bool supportsCancel, IntPtr parentWindowHandle)
        {
            lock (locker)
            {
                formProgress = new MultiOperationFormProgress(maxProgress, supportsCancel)
                    {
                        IsMoveable = true
                    };

                formProgress.OperationCancelRequested += (sender, e) =>
                    {
                        OperationCancelRequested?.Invoke(this, EventArgs.Empty);
                    };

                formCreated = DateTime.Now;

                Monitor.Pulse(locker);
            }

            formProgress.ShowDialog(new Win32Window(parentWindowHandle));
        }

        private void CloseProgressForm()
        {
            lock (locker)
            {
                if (DateTime.Now - formCreated < TimeSpan.FromMilliseconds(100))
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));

                if (formProgress == null) return;
                if (formProgress.InvokeRequired)
                    formProgress.Invoke((Action)(() => formProgress.Close()));
                else
                    formProgress.Close();

                Monitor.Wait(locker, 100);
            }
        }
    }
}