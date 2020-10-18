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
    public class FamiliesViewModel : ViewModelBase
    {
        private static readonly Lazy<FamiliesViewModel> InstanceObj = new Lazy<FamiliesViewModel>(() => new FamiliesViewModel());
        public event EventHandler<Project> NavigateTo;

        public event EventHandler<Project> DivisionsRequested;

        private IEnumerable<Division> _families;
        public IEnumerable<Division> Families { get => _families; set { _families = value; OnPropertyChanged(nameof(Families)); } }

        public static FamiliesViewModel Instance = InstanceObj.Value;

        private Project _project;
        public Project Project { get => _project; set { _project = value; OnPropertyChanged(nameof(Project)); } }

        public ICommand NextCommand { get; }

        private FamiliesViewModel()
        {
            NextCommand = new RelayCommand(() => NavigateTo?.Invoke(BrowserState.Parameters, Project));
        }

        public void Initialize(Project project)
        {

            Project = project;

            DivisionsRequested?.Invoke(this, project);
        }

        public void Initialize(IEnumerable<Division> _families)
        {
            Families = _families;
        }

    }
}
