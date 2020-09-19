using System.IO;
using System.Net;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Services.Services.Implementation
{
    public class FohlioFileDownloadService : IFohlioFileDownloadService
    {
        private readonly IApiCaller apiCaller;

        internal FohlioFileDownloadService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public ServiceResponse<Stream> DownloadFile(string url)
        {
            var response = apiCaller.DownloadFile(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<Stream>.Fail(response.StatusCode, "Unknown server communication error occured", Stream.Null);

            var stream = new MemoryStream(response.RawContent);

            return ServiceResponse<Stream>.Success(HttpStatusCode.OK, stream);
        }
    }
}