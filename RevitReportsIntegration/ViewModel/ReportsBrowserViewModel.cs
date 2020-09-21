using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class ReportsBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<ReportsBrowserViewModel> InstanceObj = new Lazy<ReportsBrowserViewModel>(() => new ReportsBrowserViewModel());
        private readonly ObservableCollection<TitleBlockViewModel> titleBlocks = new ObservableCollection<TitleBlockViewModel>();
        private readonly ObservableCollection<ReportViewModel> reports = new ObservableCollection<ReportViewModel>();
        private TitleBlockViewModel selectedTitleBlock;
        private Project project;

        private ReportsBrowserViewModel()
        {
 

            RefreshProjectReportsCommand = new RelayCommand(() => RefreshProjectReports?.Invoke(this, new SelectProjectEventArgs(project)));

            SwitchbackToProjectsListCommand = new RelayCommand(() => SwitchbackToProjectsListRequested?.Invoke(this, EventArgs.Empty));

            NavigateToPortalCommand = new RelayCommand(() => Process.Start(new ProcessStartInfo("https://www.fohlio.com/")));
        }

        public static ReportsBrowserViewModel Instance => InstanceObj.Value;

        public IEnumerable<TitleBlockViewModel> TitleBlocks => titleBlocks;

        public IEnumerable<ReportViewModel> Reports => reports;

        public TitleBlockViewModel SelectedTitleBlock
        {
            get { return selectedTitleBlock; }
            set
            {
                selectedTitleBlock = value;

                OnPropertyChanged();
            }
        }


        public event EventHandler<ImportReportsEventArgs> ImportReports;

        public event EventHandler<SelectProjectEventArgs> RefreshProjectReports;

        public event EventHandler SwitchbackToProjectsListRequested;

        public ICommand ImportReportsCommand { get; }

        public ICommand RefreshProjectReportsCommand { get; }

        public ICommand SwitchbackToProjectsListCommand { get; }

        public ICommand NavigateToPortalCommand { get; }

        public void Initialize(Project userProject, IEnumerable<Report> items)
        {
            project = userProject;

            reports.Clear();

            foreach (var report in items)
                reports.Add(new ReportViewModel(report));
        }

        public void Initialize(IEnumerable<TitleBlockViewModel> items)
        {
            var selectedTitleBlockId = SelectedTitleBlock?.Id;

            titleBlocks.Clear();

            foreach (var titleBlock in items)
                titleBlocks.Add(titleBlock);

            SelectedTitleBlock = titleBlocks.FirstOrDefault(x => x.Id == selectedTitleBlockId) ?? titleBlocks.FirstOrDefault();
        }
    }
}