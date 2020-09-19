namespace Fohlio.RevitReportsIntegration.Views.BackgroundWorker
{
    internal partial class MultiOperationFormProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiOperationFormProgress));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbInternalOperation = new System.Windows.Forms.Label();
            this.pbInternalOperation = new Fohlio.RevitReportsIntegration.Views.BackgroundWorker.PersentageProgressBar();
            this.CancelOpertationButton = new System.Windows.Forms.Button();
            this.lblRemainedTime = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbProgress = new Fohlio.RevitReportsIntegration.Views.BackgroundWorker.PersentageProgressBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbInternalOperation);
            this.panel1.Controls.Add(this.pbInternalOperation);
            this.panel1.Controls.Add(this.CancelOpertationButton);
            this.panel1.Controls.Add(this.lblRemainedTime);
            this.panel1.Controls.Add(this.lblElapsedTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 140);
            this.panel1.TabIndex = 5;
            // 
            // lbInternalOperation
            // 
            this.lbInternalOperation.AutoSize = true;
            this.lbInternalOperation.Location = new System.Drawing.Point(9, 45);
            this.lbInternalOperation.Name = "lbInternalOperation";
            this.lbInternalOperation.Size = new System.Drawing.Size(0, 13);
            this.lbInternalOperation.TabIndex = 11;
            // 
            // pbInternalOperation
            // 
            this.pbInternalOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInternalOperation.Location = new System.Drawing.Point(12, 65);
            this.pbInternalOperation.Name = "pbInternalOperation";
            this.pbInternalOperation.ShowProgress = true;
            this.pbInternalOperation.Size = new System.Drawing.Size(351, 23);
            this.pbInternalOperation.TabIndex = 10;
            this.pbInternalOperation.TextProgressStyle = Fohlio.RevitReportsIntegration.Views.BackgroundWorker.TextProgressStyle.Percent;
            // 
            // CancelOpertationButton
            // 
            this.CancelOpertationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelOpertationButton.Location = new System.Drawing.Point(241, 104);
            this.CancelOpertationButton.Name = "CancelOpertationButton";
            this.CancelOpertationButton.Size = new System.Drawing.Size(119, 23);
            this.CancelOpertationButton.TabIndex = 0;
            this.CancelOpertationButton.Text = "Cancel";
            this.CancelOpertationButton.UseVisualStyleBackColor = true;
            this.CancelOpertationButton.Visible = false;
            this.CancelOpertationButton.Click += new System.EventHandler(this.CancelOpertationButtonClick);
            // 
            // lblRemainedTime
            // 
            this.lblRemainedTime.AutoSize = true;
            this.lblRemainedTime.Location = new System.Drawing.Point(112, 114);
            this.lblRemainedTime.Name = "lblRemainedTime";
            this.lblRemainedTime.Size = new System.Drawing.Size(10, 13);
            this.lblRemainedTime.TabIndex = 9;
            this.lblRemainedTime.Text = "-";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Location = new System.Drawing.Point(112, 91);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(10, 13);
            this.lblElapsedTime.TabIndex = 8;
            this.lblElapsedTime.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Remains:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Time passed:";
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(12, 13);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.ShowProgress = true;
            this.pbProgress.Size = new System.Drawing.Size(351, 23);
            this.pbProgress.TabIndex = 5;
            this.pbProgress.TextProgressStyle = Fohlio.RevitReportsIntegration.Views.BackgroundWorker.TextProgressStyle.Percent;
            // 
            // MultiOperationFormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 140);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiOperationFormProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Прогресс выполнения";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProgressFormClosing);
            this.Load += new System.EventHandler(this.FormProgressLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRemainedTime;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PersentageProgressBar pbProgress;
        private System.Windows.Forms.Button CancelOpertationButton;
        private PersentageProgressBar pbInternalOperation;
        private System.Windows.Forms.Label lbInternalOperation;
    }
}