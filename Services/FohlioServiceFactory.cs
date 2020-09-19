using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Services.Services;
using Fohlio.RevitReportsIntegration.Services.Services.Implementation;

namespace Fohlio.RevitReportsIntegration.Services
{
    public class FohlioServiceFactory : IFohlioServiceFactory
    {
        private readonly IDictionary<Type, IFohlioService> services;

        public FohlioServiceFactory(IApiCaller apiClient, ISerializer serializer)
        {
            services = new Dictionary<Type, IFohlioService>
                {
                    {typeof (IFohlioAuthentificationService), new FohlioAuthentificationService(apiClient, serializer)},
                    {typeof (IFohlioProjectsService), new FohlioProjectsService(apiClient, serializer)},
                    {typeof (IFohlioFileDownloadService), new FohlioFileDownloadService(apiClient)}
                };
        }

        public T Create<T>() where T : class, IFohlioService
        {
            IFohlioService service;

            if (!services.TryGetValue(typeof(T), out service))
                return null;

            return (T)service;
        }
    }
}