using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class DivisionsViewModel : ViewModelBase
    {
        private static readonly Lazy<DivisionsViewModel> InstanceObj = new Lazy<DivisionsViewModel>(() => new DivisionsViewModel());

        public event EventHandler<Project> DivisionsRequested;

        private IEnumerable<Division> _divisions;
        public IEnumerable<Division> Divisions { get => _divisions; set { _divisions = value; OnPropertyChanged(nameof(Divisions)); } }

        //public ICommand OnLoadedCommand { get; }

        private DivisionsViewModel()
        {
            //OnLoadedCommand = new RelayCommand(GetDevisions);
        }

        public static DivisionsViewModel Instance = InstanceObj.Value;

        private Project _project;
        public Project Project { get => _project; set { _project = value; OnPropertyChanged(nameof(Project)); } }

        public void Initialize(Project project)
        {
            DivisionsRequested?.Invoke(this, project);
        }

        public void Initialize(IEnumerable<Division> divisions)
        {
            Divisions = divisions;
        }

    }
}
