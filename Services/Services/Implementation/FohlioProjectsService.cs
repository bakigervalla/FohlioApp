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
                return ServiceResponse<IEnumerable<Project>>.Fail(response.StatusCode, "Unknown server communication error occured", null);

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
            var kind = (string)report.kind;

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


        #region Export Fohlio

        public ServiceResponse<IEnumerable<Parameter>> GetParameters(AccessToken token, Project project)
        {
            var parameters = FetchParameters(token, project).Data;

            return ServiceResponse<IEnumerable<Parameter>>.Success(HttpStatusCode.OK, parameters);
        }

        private ServiceResponse<IReadOnlyCollection<Parameter>> FetchParameters(AccessToken token, Project project)
        {
            // var apiCallOptions = new ApiCallQuery("/openapi/projects/{id}/items", ApiMethod.GET, "application/json");
            var apiCallOptions = new ApiCallQuery("/openapi/divisions/project/{id}", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Authorization", token.Token));

            apiCallOptions.AddPathParameter(new ApiParameter("id", project.Id.ToString()));

            var response = apiClient.CallApi(apiCallOptions);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<IReadOnlyCollection<Parameter>>.Fail(response.StatusCode, "Unknown server communication error occured", null);

            var result = serializer.DeserializeArray(response.Content);

            var parameters = new List<Parameter>();

            for (var i = 0; i < result.Count; ++i)
            {
                var parameter = result[i];

                parameters.Add(new Parameter((int)parameter.id, (string)parameter.name, (string)parameter.code, (string)parameter.key, (dynamic)parameter.children));
            }

            return ServiceResponse<IReadOnlyCollection<Parameter>>.Success(HttpStatusCode.OK, parameters);
        }

        public ServiceResponse<IEnumerable<Division>> GetDivisions(AccessToken token, Project project)
        {
            var divisions = FetchDivisions(token, project).Data;

            return ServiceResponse<IEnumerable<Division>>.Success(HttpStatusCode.OK, divisions);
        }

        private ServiceResponse<IReadOnlyCollection<Division>> FetchDivisions(AccessToken token, Project project)
        {
            var apiCallOptions = new ApiCallQuery("/openapi/divisions/project/{id}", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Authorization", token.Token));

            apiCallOptions.AddPathParameter(new ApiParameter("id", project.Id.ToString()));
            
            var response = apiClient.CallApi(apiCallOptions);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<IReadOnlyCollection<Division>>.Fail(response.StatusCode, "Unknown server communication error occured", null);

            var result = serializer.DeserializeArray(response.Content);

            var divisions = new List<Division>();

            for (var i = 0; i < result.Count; ++i)
            {
                var division = result[i];

                divisions.Add(new Division((int)division.id, (string)division.name, (string)division.code, (string)division.key, (dynamic)division.children));
            }

            return ServiceResponse<IReadOnlyCollection<Division>>.Success(HttpStatusCode.OK, divisions);
        }

        public ServiceResponse<IEnumerable<Column>> GetColumns(AccessToken token, Project project)
        {
            var columns = FetchColumns(token, project).Data;

            return ServiceResponse<IEnumerable<Column>>.Success(HttpStatusCode.OK, columns);
        }

        private ServiceResponse<IReadOnlyCollection<Column>> FetchColumns(AccessToken token, Project project)
        {
            var apiCallOptions = new ApiCallQuery("/openapi/columns/project/{id}", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Authorization", token.Token));

            apiCallOptions.AddPathParameter(new ApiParameter("id", project.Id.ToString()));

            var response = apiClient.CallApi(apiCallOptions);

            if (response.StatusCode != HttpStatusCode.OK)
                return ServiceResponse<IReadOnlyCollection<Column>>.Fail(response.StatusCode, "Unknown server communication error occured", null);

            var result = serializer.DeserializeArray(response.Content);

            var columns = new List<Column>();

            for (var i = 0; i < result.Count; ++i)
            {
                var column = result[i];

                columns.Add(new Column((int)column.id, (string)column.key, (string)column.name, (string)column.type, (int)column.position));
            }

            return ServiceResponse<IReadOnlyCollection<Column>>.Success(HttpStatusCode.OK, columns);
        }

        #endregion


    }
}