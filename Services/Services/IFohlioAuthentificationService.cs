using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface IFohlioAuthentificationService : IFohlioService
    {
        ServiceResponse<AccessToken> Authentificate(FohlioAuthentificationOptions options);
    }
}