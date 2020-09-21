#if DEBUG
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Fohlio.RevitReportsIntegration.ViewModel;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.Views;

namespace Fohlio.RevitReportsIntegration.Services.Implementation
{
    public class RevitReportsWindowAdapter : IReportsControlView
    {
        
        private readonly IPdfReportsSettingsDialog pdfReportsSettingsDialog;

        public RevitReportsWindowAdapter( IPdfReportsSettingsDialog pdfReportsSettingsDialog, IFohlioServiceFactory serviceFactory)
        {
            
            this.pdfReportsSettingsDialog = pdfReportsSettingsDialog;

            var browser = MainBrowserViewModel.Instance;

            browser.Initialize(serviceFactory);

            // browser.LaunchReportsImportRequested += (sender, e) => this.reportImportLauncher.LaunchReportsImport(e.Reports, e.TitleBlockId);

            browser.PdfImportSettingsPrompt += OnPdfImportSettingsPrompt;
        }

        public void Initialize(object application)
        {
            
        }

        public void Show(object application)
        {
            LoadDependentAssemblies();

            // reportImportLauncher.Initialize();

            var window = new ReportsWindow();

            window.Show();
        }

        public void Hide(object application)
        {

        }

        public void RefreshTitleBlocks(object document)
        {
            // var collector = collectorPrototype.Create(document);

            //var titleBlocks = collector
            //    .FindTitleBlocks()
            //    .Select(x => new TitleBlockViewModel(x.Id, x.Name))
            //    .Union(new[] { TitleBlockViewModel.CreateEmpty() });

            // ReportsBrowserViewModel.Instance.Initialize(titleBlocks);
        }

        private void LoadDependentAssemblies()
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            Assembly.Load("EPPlus");
            Assembly.Load("RestSharp");
            Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewtonSoft.Json.dll"));

        }

        private void OnPdfImportSettingsPrompt(object sender, PdfImportSettingsPromptEventArgs e)
        {
            e.Cancel = !pdfReportsSettingsDialog.Show(e.Reports, IntPtr.Zero);
        }
    }
} 
#endif