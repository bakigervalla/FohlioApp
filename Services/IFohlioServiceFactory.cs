using Fohlio.RevitReportsIntegration.Services.Services;

namespace Fohlio.RevitReportsIntegration.Services
{
    public interface IFohlioServiceFactory
    {
        T Create<T>() where T : class, IFohlioService;
    }
}