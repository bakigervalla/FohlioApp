using System.Windows;

namespace Fohlio.RevitReportsIntegration.Views
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow
    {

        public ReportsWindow()
        {
            InitializeComponent();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            this.Left = ((SystemParameters.WorkArea.Width - Width) / 2) + SystemParameters.WorkArea.Left;
        }

    }
}
