using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Fohlio.RevitReportsIntegration.Entities;
using Fohlio.RevitReportsIntegration.Properties;
using Fohlio.RevitReportsIntegration.Services;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.Services.RestApi;
using Fohlio.RevitReportsIntegration.Services.Serialization;
using Fohlio.RevitReportsIntegration.Services.Services;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Unity;
using Unity.Resolution;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class MainBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<MainBrowserViewModel> InstanceObj = new Lazy<MainBrowserViewModel>(() => new MainBrowserViewModel());
        private IFohlioAuthentificationService authentificationService;
        private IFohlioProjectsService projectsService;
        private IFohlioFileDownloadService fileDownloadService;
        private BrowserState state;
        private bool isBusy;
        private AccessToken accessToken;

        private MainBrowserViewModel()
        {
            state = BrowserState.Login;

            LoginBrowserViewModel.Instance.LoginRequested += OnLoginRequested;

            var projectsBrowser = ProjectsBrowserViewModel.Instance;

            projectsBrowser.RefreshProjectListRequested += OnRefreshProjectListRequested;

            projectsBrowser.SelectProject += BrowserOnSelectProject;

            projectsBrowser.Initialize(this);

            var reportsBrowser = ReportsBrowserViewModel.Instance;

            reportsBrowser.ImportReports += OnImportReports;

            reportsBrowser.RefreshProjectReports += BrowserOnSelectProject;

            reportsBrowser.SwitchbackToProjectsListRequested += OnSwitchbackToProjectsListRequested;

            AccountBrowserViewModel.Instance.LogoutRequested += OnLogoutRequested;

            var container = new UnityContainer();
            var apiCaller = container.Resolve<FohlioApiCaller>();
            var serializer = container.Resolve<JsonSerializer>();

            // var serviceFactory = container.Resolve<IFohlioServiceFactory>();// new ParameterOverride("apiCaller", apiCaller));

            var serviceFactory = new FohlioServiceFactory(apiCaller, serializer);



            this.Initialize(serviceFactory);
        }

        public static MainBrowserViewModel Instance => InstanceObj.Value;
        
        public BrowserState State
        {
            get { return state; }
            private set
            {
                var stateChanged = state != value;

                state = value;

                OnPropertyChanged();

                if (stateChanged)
                {
                    StateChanged?.Invoke(this, new MainBrowserStateChangedEventArgs(state));

                    CurrentActionBrowserViewModel.Instance.SetState(state);
                }
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            private set
            {
                isBusy = value;

                OnPropertyChanged();
            }
        }

        public event EventHandler<MainBrowserStateChangedEventArgs> StateChanged;

        public event EventHandler<LaunchReportsImportRequestedEventArgs> LaunchReportsImportRequested;

        public event EventHandler<PdfImportSettingsPromptEventArgs> PdfImportSettingsPrompt;

        public void Initialize(IFohlioServiceFactory serviceFactory)
        {
            authentificationService = serviceFactory.Create<IFohlioAuthentificationService>();

            projectsService = serviceFactory.Create<IFohlioProjectsService>();

            fileDownloadService = serviceFactory.Create<IFohlioFileDownloadService>();
        }

        private void OnLoginRequested(object sender, LoginRequestedEventArgs e)
        {
            if (State != BrowserState.Login)
                return;

            IsBusy = true;

            var backgroundWorker = new BackgroundWorker();

            ServiceResponse<AccessToken> response = null;

            backgroundWorker.DoWork += (o, args) =>
                {
                    response = authentificationService.Authentificate(new FohlioAuthentificationOptions(e.UserName, e.Password));
                };

            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                IsBusy = false;

                if (response == null)
                {
                    ShowServiceCommunicationException(args.Error.Message);

                    return;
                }

                if (!response.IsSuccess)
                {
                    ShowServiceCommunicationException(response.Message, response.ExtendedMessage);

                    return;
                }

                accessToken = response.Data;

                 State = BrowserState.ProjectsList;
                //* State = BrowserState.Dashboard;

                //var dashboardBrowser = DashboardViewModel.Instance;

                //dashboardBrowser.Initialize(this);

                

                AccountBrowserViewModel.Instance.Authentificate(e.UserName);
            };

            backgroundWorker.RunWorkerAsync();
        }

        private void OnRefreshProjectListRequested(object sender, EventArgs e)
        {
            if (State != BrowserState.ProjectsList || accessToken == null)
                return;

            IsBusy = true;

            ServiceResponse<IEnumerable<Project>> response = null;

            var backgroundWorker = new BackgroundWorker();
            
            backgroundWorker.DoWork += (o, args) =>
                {
                    response = projectsService.FindProjects(accessToken);
                };

            backgroundWorker.RunWorkerCompleted += (o, args) =>
                {
                    IsBusy = false;

                    if (response == null)
                    {
                        ShowServiceCommunicationException(args.Error.Message);

                        return;
                    }

                    if (response.IsSuccess)
                        ProjectsBrowserViewModel.Instance.Initialize(response.Data);
                    else
                        ShowServiceCommunicationException(response.Message);
                };

            backgroundWorker.RunWorkerAsync();
        }

        private void BrowserOnSelectProject(object sender, SelectProjectEventArgs e)
        {
            if (accessToken == null)
                return;

            IsBusy = true;

            ServiceResponse<IEnumerable<Report>> response = null;

            var backgroundWorker = new BackgroundWorker();
            
            backgroundWorker.DoWork += (o, args) =>
                {
                    response = projectsService.FindReports(accessToken, e.Project);
                };

            backgroundWorker.RunWorkerCompleted += (o, args) =>
                {
                    IsBusy = false;

                    if (response == null)
                    {
                        ShowServiceCommunicationException(args.Error.Message);

                        return;
                    }

                    if (response.IsSuccess)
                    {
#if REVIT_2020
                        var reports = response.Data;
#else
                        var reports = response.Data.Where(x => x.Kind == ReportKind.Xlsx); 
#endif
                        ReportsBrowserViewModel.Instance.Initialize(e.Project, reports);

                        State = BrowserState.ReportsList;
                    }
                    else
                        ShowServiceCommunicationException(response.Message);
                };
            
            backgroundWorker.RunWorkerAsync();
        }

        private void OnImportReports(object sender, ImportReportsEventArgs e)
        {
            if (State != BrowserState.ReportsList)
                return;

            var reports = new List<IReportContent>(e.Reports.Count());

            var backgroundWorker = new BackgroundWorker();

            IsBusy = true;

            backgroundWorker.DoWork += (o, args) =>
                {
                    foreach (var report in e.Reports)
                    {
                        //var response = fileDownloadService.DownloadFile(report.Url);

                        //if (!response.IsSuccess)
                        //    throw new Exception(response.Message);

                        // reports.Add(new ReportContent(report, response.Data));
                    }
                };

            backgroundWorker.RunWorkerCompleted += (o, args) =>
                {
                    IsBusy = false;

                    if (args.Error != null)
                    {
                        ShowServiceCommunicationException(args.Error.Message);

                        return;
                    }

                    var pdfReports = reports
                        //.Where(x => x.Report.Kind == ReportKind.Pdf)
                        .ToList();

                    if (pdfReports.Any())
                    {
                        var eventArgs = new PdfImportSettingsPromptEventArgs(pdfReports);

                        PdfImportSettingsPrompt?.Invoke(this, eventArgs);

                        if (eventArgs.Cancel)
                            return;
                    }

                    LaunchReportsImportRequested?.Invoke(this, new LaunchReportsImportRequestedEventArgs(reports, e.TitleBlockId));
                };

            backgroundWorker.RunWorkerAsync();
        }

        private void OnSwitchbackToProjectsListRequested(object sender, EventArgs e)
        {
            State = BrowserState.ProjectsList;

            OnRefreshProjectListRequested(sender, e);
        }

        private void OnLogoutRequested(object sender, EventArgs e)
        {
            accessToken = null;

            State = BrowserState.Login;
        }

        private static void ShowServiceCommunicationException(string message)
        {
            MessageBox.Show(message, Resources.ErrorDialogCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void ShowServiceCommunicationException(string mainMessage, string extendedContent)
        {
//            var dialog = new TaskDialog(Resources.ErrorDialogCaption)
//                {
//                    MainInstruction = mainMessage,
//                    MainContent = extendedContent,
//                    TitleAutoPrefix = false,
//#if REVIT_2020 || REVIT_2019
//                    MainIcon = TaskDialogIcon.TaskDialogIconError
//#else
//                    MainIcon = TaskDialogIcon.TaskDialogIconWarning
//#endif
//                };

//            dialog.Show();
        }
    }
}