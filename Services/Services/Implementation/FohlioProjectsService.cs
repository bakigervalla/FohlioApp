using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls;

namespace Fohlio.RevitReportsIntegration.Services.Services.Implementation
{
    internal class FohlioProjectsService : IFohlioProjectsService
    {
        private readonly IApiCaller apiClient;
        private readonly ISerializer serializer;

        public FohlioProjectsService(IApiCaller apiClient, ISerializer serializer)
        {
            this.apiClient = apiClient;
            this.serializer = serializer;
        }

        public ServiceResponse<IEnumerable<Project>> FindProjects(AccessToken token)
        {
            var apiCallOptions = new ApiCallQuery("/openapi/projects", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Authorization", token.Token));

            var response = apiClient.CallApi(apiCallOptions);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<IEnumerable<Project>>.Fail(response.StatusCode,"Unknown server communication error occured", null);
            
            var result = serializer.DeserializeArray(response.Content);

            var projects = new List<Project>();

            for (var i = 0; i < result.Count; ++i)
            {
                var project = result[i];

                projects.Add(new Project((int)project.id, (string)project.name, (bool)project.single_schedule));
            }

            return ServiceResponse<IEnumerable<Project>>.Success(HttpStatusCode.OK, projects);
        }

        public ServiceResponse<IEnumerable<Report>> FindReports(AccessToken token, Project project)
        {
            var reports = Enumerable.Range(1, int.MaxValue)
                .Select(x => FindReports(token, project, x))
                .TakeWhile(x => x.IsSuccess && x.Data.Count > 0)
                .SelectMany(x => x.Data);

            return ServiceResponse<IEnumerable<Report>>.Success(HttpStatusCode.OK, reports);
        }

        private ServiceResponse<IReadOnlyCollection<Report>> FindReports(AccessToken token, Project project, int page)
        {
            var apiCallOptions = new ApiCallQuery("/openapi/projects/{id}/reports", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Authorization", token.Token));

            apiCallOptions.AddPathParameter(new ApiParameter("id", project.Id.ToString()));

            apiCallOptions.AddQueryParameter(new ApiParameter("page", page.ToString()));

            var response = apiClient.CallApi(apiCallOptions);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<IReadOnlyCollection<Report>>.Fail(response.StatusCode, "Unknown server communication error occured", null);

            var result = serializer.DeserializeArray(response.Content);

            var reports = new List<Report>();

            for (var i = 0; i < result.Count; ++i)
            {
                var report = result[i];

                reports.Add(new Report((int)report.id, GetReportKind(report), (string)report.url, (DateTime)report.created_at));
            }

            return ServiceResponse<IReadOnlyCollection<Report>>.Success(HttpStatusCode.OK, reports);
        }

        private static ReportKind GetReportKind(dynamic report)
        {
            var kind = (string) report.kind;

            switch (kind)
            {
                case "xlsx":
                    return ReportKind.Xlsx;

                case "pdf":
                    return ReportKind.Pdf;

                default:
                    return ReportKind.Unknown;
            }
        }
    }
}