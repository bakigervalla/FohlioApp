using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class MappViewModel : ViewModelBase
    {
        private static readonly Lazy<MappViewModel> InstanceObj = new Lazy<MappViewModel>(() => new MappViewModel());
        

        private MappViewModel()
        {
            //SwitchCommand = new RelayCommand<object>(Switch);
        }

        public static MappViewModel Instance = InstanceObj.Value;
    }
}
