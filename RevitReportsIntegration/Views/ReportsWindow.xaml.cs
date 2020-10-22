using Fohlio.RevitReportsIntegration.ViewModel;
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

        private void Window_Closed(object sender, System.EventArgs e)
        {
            var viewModel = (MainBrowserViewModel)this.DataContext;
            viewModel.Logout();
        }
    }
}
