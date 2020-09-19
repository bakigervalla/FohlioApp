using System.IO;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface IFohlioFileDownloadService : IFohlioService
    {
        ServiceResponse<Stream> DownloadFile(string url);
    }
}