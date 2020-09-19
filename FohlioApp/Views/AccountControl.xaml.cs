using System.Windows;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.Views
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl
    {
        public AccountControl()
        {
            InitializeComponent();
        }

        private void ShowTooltip(object sender, MouseEventArgs e)
        {
            TooltipPopup.IsOpen = true;
        }

        private void HideTooltip(object sender, RoutedEventArgs e)
        {
            TooltipPopup.IsOpen = false;
        }
    }
}
