using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Services;
using Fohlio.RevitReportsIntegration.Services.Implementation;
using Fohlio.RevitReportsIntegration.Services.RestApi;
using Fohlio.RevitReportsIntegration.Services.Serialization;
using Fohlio.RevitReportsIntegration.Services.Services;
using Unity;
using Unity.Lifetime;

namespace Fohlio.RevitReportsIntegration.IoC
{
    public class IoCContainer
    {
        private readonly IUnityContainer container;

        private static readonly Lazy<IoCContainer> InstanceObj = new Lazy<IoCContainer>(() => new IoCContainer());

        private IoCContainer()
        {
            container = new UnityContainer();

            SetupContainer();
        }

        private T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        private void SetupContainer()
        {
            container
                .RegisterType<IReportsControlView, RevitReportsWindowAdapter>()
                .RegisterType<IApiCaller, FohlioApiCaller>()
                .RegisterType<ISerializer, JsonSerializer>()
                .RegisterType<IFohlioServiceFactory, FohlioServiceFactory>()
                .RegisterType<IMultiOperationProcessNotifier, MultiOperationProcessNotifier>()
                .RegisterType<IPdfReportsSettingsDialog, PdfReportsSettingsDialog>()
                .RegisterType<IFohlioServiceFactory, FohlioServiceFactory>(new ContainerControlledLifetimeManager());
        }
    }
}