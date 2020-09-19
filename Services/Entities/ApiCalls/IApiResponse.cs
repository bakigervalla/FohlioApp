using System.Net;

namespace Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls
{
    public interface IApiResponse
    {
        HttpStatusCode StatusCode { get; }

        string Content { get; }

        byte[] RawContent { get; }
    }
}