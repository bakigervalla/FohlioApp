using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface IFohlioProjectsService : IFohlioService
    {
        ServiceResponse<IEnumerable<Project>> FindProjects(AccessToken token);

        ServiceResponse<IEnumerable<Report>> FindReports(AccessToken token, Project project);

        ServiceResponse<IEnumerable<Division>> GetDivisions(AccessToken token, Project project);
    }
}