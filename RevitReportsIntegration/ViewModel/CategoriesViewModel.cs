using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class CategoriesViewModel : ViewModelBase
    {
        private static readonly Lazy<CategoriesViewModel> InstanceObj = new Lazy<CategoriesViewModel>(() => new CategoriesViewModel());
        public event EventHandler<Project> NavigateTo;

        public event EventHandler<Project> ParametersRequested;

        private IList<Parameter> _categories;
        public IList<Parameter> Categories { get => _categories; set { _categories = value; OnPropertyChanged(nameof(Categories)); } }

        public static CategoriesViewModel Instance = InstanceObj.Value;

        private Project _project;
        public Project Project { get => _project; set { _project = value; OnPropertyChanged(nameof(Project)); } }

        public ICommand NextCommand { get; }

        private CategoriesViewModel()
        {
            NextCommand = new RelayCommand( () => NavigateTo?.Invoke(BrowserState.Families, Project) );
        }

        public void Initialize(Project project)
        {
            Project = project;

            ParametersRequested?.Invoke(this, project);
        }

        public void Initialize(IEnumerable<Parameter> categories)
        {
            Categories = categories.ToList();
        }

    }
}
