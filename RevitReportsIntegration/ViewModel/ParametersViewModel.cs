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
    public class ParametersViewModel : ViewModelBase
    {
        private static readonly Lazy<ParametersViewModel> InstanceObj = new Lazy<ParametersViewModel>(() => new ParametersViewModel());
        public event EventHandler<Project> NavigateTo;

        public event EventHandler<Project> ColumnsRequested;

        private IEnumerable<Column> _columns;
        public IEnumerable<Column> Columns { get => _columns; set { _columns = value; OnPropertyChanged(nameof(Columns)); } }

        public static ParametersViewModel Instance = InstanceObj.Value;

        private Project _project;
        public Project Project { get => _project; set { _project = value; OnPropertyChanged(nameof(Project)); } }

        public ICommand NextCommand { get; }

        private ParametersViewModel()
        {
            NextCommand = new RelayCommand(() => NavigateTo?.Invoke(BrowserState.Completed, Project));
        }

        public void Initialize(Project project)
        {
            Project = project;

            ColumnsRequested?.Invoke(this, project);
        }

        public void Initialize(IEnumerable<Column> _columns)
        {
            Columns = _columns;
        }

    }
}
