using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class FinalViewModel : ViewModelBase
    {
        private static readonly Lazy<FinalViewModel> InstanceObj = new Lazy<FinalViewModel>(() => new FinalViewModel());
        public event EventHandler<Project> NavigateTo;

        public event EventHandler<Project> ExportFohlioRequested;

        public static FinalViewModel Instance = InstanceObj.Value;

        private IEnumerable<Division> _families;
        public IEnumerable<Division> Families { get => _families; set { _families = value; OnPropertyChanged(nameof(Families)); } }

        private Project _project;
        public Project Project { get => _project; set { _project = value; OnPropertyChanged(nameof(Project)); } }

        private bool _state;
        public bool State { get => _state; set { _state = value; OnPropertyChanged(nameof(State)); } }

        public ICommand NextCommand { get; }
        public ICommand DownloadXlsCommand { get; }

        public ICommand FinishCommand { get; }
        public ICommand OpenFohlioCommand { get; }

        private FinalViewModel()
        {

            NextCommand = new RelayCommand(() => State = true);
            DownloadXlsCommand = new RelayCommand( DownloadXlsReport);

            FinishCommand = new RelayCommand<Window>((window) => window.Close());
            OpenFohlioCommand = new RelayCommand(() => System.Diagnostics.Process.Start("https://www.fohlio.com/"));
        }

        private void DownloadXlsReport()
        {
            // ToDo
        }

        public void Initialize(Project project)
        {

            Project = project;

            ExportFohlioRequested?.Invoke(this, project);
        }

        public void Initialize(IEnumerable<Division> _families)
        {
            Families = _families;
        }

    }
}
