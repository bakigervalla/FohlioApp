using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private static readonly Lazy<DashboardViewModel> InstanceObj = new Lazy<DashboardViewModel>(() => new DashboardViewModel());
        MainBrowserViewModel _mainBrowser;

        public ICommand SwitchCommand { get; }

        public DashboardViewModel()
        {
            SwitchCommand = new RelayCommand<string>( (p) => NavigateModule(p) );
        }

        public void Initialize(MainBrowserViewModel mainBrowser)
        {
            _mainBrowser = mainBrowser;
        }

        public static DashboardViewModel Instance = InstanceObj.Value;

        private void NavigateModule(string p)
        {
            //if (p == "reports")
            //    _mainBrowser.State = BrowserState.ProjectsList;
        }
    }
}
