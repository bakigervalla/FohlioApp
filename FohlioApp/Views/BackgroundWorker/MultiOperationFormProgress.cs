using System;
using System.Windows.Forms;
using Fohlio.RevitReportsIntegration.Properties;

namespace Fohlio.RevitReportsIntegration.Views.BackgroundWorker
{
    internal partial class MultiOperationFormProgress : Form
    {
        private TimeSpan timeElapsed;

        public MultiOperationFormProgress(int progressMaximumValue, bool supportCancelling)
        {
            InitializeComponent();
            pbProgress.Maximum = progressMaximumValue;
            timeElapsed = TimeSpan.Zero;
            CancelOpertationButton.Visible = supportCancelling;
        }

        public int ProgressValue
        {
            set
            {
                if (value == pbProgress.Value)
                    return;

                var v = value;

                if (value >= pbProgress.Maximum)
                    v = pbProgress.Maximum;

                if (!InvokeRequired)
                    pbProgress.Value = value;
                else
                    Invoke((Action)(() => { pbProgress.Value = v; }));
            }
        }

        public int InternalOperationProgressValue
        {
            set
            {
                if (value == pbInternalOperation.Value)
                    return;

                var v = value;

                if (value >= pbInternalOperation.Maximum)
                    v = pbInternalOperation.Maximum;

                Action action = () => { pbInternalOperation.Value = v; };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }
        }

        public bool IsMoveable
        {
            get { return FormBorderStyle == FormBorderStyle.FixedToolWindow; }
            set
            {
                FormBorderStyle = value ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
            }
        }

        public bool IsCancellingInvoked { get; private set; }

        public event EventHandler OperationCancelRequested;

        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return;

            Action action = () =>
                {
                    Text = $"{title} - progress value";
                };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        public void SetInternalOperationName(string operationName)
        {
            Action action = () => { lbInternalOperation.Text = operationName; };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromMilliseconds(timer1.Interval));

            if (!InvokeRequired)
                lblElapsedTime.Text = timeElapsed.ToString();
            else
                Invoke((Action)(() => lblElapsedTime.Text = timeElapsed.ToString()));

            if (pbProgress.Value > 0)
            {
                var remainedTime = TimeSpan
                    .FromMilliseconds(timeElapsed.TotalMilliseconds / pbProgress.Value *
                                      (pbProgress.Maximum - pbProgress.Value));
                if (!InvokeRequired)
                    lblRemainedTime.Text = remainedTime.ToString(@"hh\:mm\:ss");
                else
                    Invoke((Action)(() => lblRemainedTime.Text = remainedTime.ToString(@"hh\:mm\:ss")));
            }
        }

        private void FormProgressLoad(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FormProgressFormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void CancelOpertationButtonClick(object sender, EventArgs e)
        {
            Action action = () =>
                {
                    CancelOpertationButton.Text = Resources.CancellationWaitingPrompt;
                    CancelOpertationButton.Enabled = false;
                    IsCancellingInvoked = true;

                    OperationCancelRequested?.Invoke(this, EventArgs.Empty);
                };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
