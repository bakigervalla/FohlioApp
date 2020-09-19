using System.Net;
using Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls;

namespace Fohlio.RevitReportsIntegration.Services.RestApi
{
    public class FohlioApiResponse : IApiResponse
    {
        public FohlioApiResponse(HttpStatusCode statusCode, string content, byte[] rawContent)
        {
            StatusCode = statusCode;

            Content = content;

            RawContent = rawContent;
        }

        public HttpStatusCode StatusCode { get; }

        public string Content { get; }

        public byte[] RawContent { get; }
    }
}