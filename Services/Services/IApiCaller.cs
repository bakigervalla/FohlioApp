using Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls;

namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface IApiCaller
    {
        IApiResponse CallApi(ApiCallQuery query);

        IApiResponse DownloadFile(string url);
    }
}