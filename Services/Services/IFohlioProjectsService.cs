using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface IFohlioProjectsService : IFohlioService
    {
        ServiceResponse<IEnumerable<Project>> FindProjects(AccessToken token);

        ServiceResponse<IEnumerable<Report>> FindReports(AccessToken token, Project project);

        ServiceResponse<IEnumerable<Area>> GetAreas(AccessToken token, Project project);

        ServiceResponse<IEnumerable<Parameter>> GetParameters(AccessToken token, Project project);

        ServiceResponse<IEnumerable<Division>> GetDivisions(AccessToken token, Project project);

        ServiceResponse<IEnumerable<Column>> GetColumns(AccessToken token, Project project);
    }
}