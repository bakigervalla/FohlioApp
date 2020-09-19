using System;
using System.Net;
using Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls;
using Fohlio.RevitReportsIntegration.Services.Services;
using RestSharp;

namespace Fohlio.RevitReportsIntegration.Services.RestApi
{
    public class FohlioApiCaller : IApiCaller
    {
        private readonly RestClient client;

        public FohlioApiCaller()
        {
#if DEBUG
            const string baseUrl = "https://test.fohlio.com/";
#else
            var baseUrl = System.Configuration.ConfigurationManager.OpenExeConfiguration(GetType().Assembly.Location).AppSettings.Settings["ServiceAddress"].Value; 
#endif

            client = new RestClient(baseUrl)
                {
                    UserAgent = "Fohlio Revit service client",
                    Timeout = (int) TimeSpan.FromHours(1).TotalMilliseconds
                };
        }

        public IApiResponse CallApi(ApiCallQuery query)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;

            var request = PrepareRequest(query);

            var response = client.Execute(request);

            return new FohlioApiResponse(response.StatusCode, response.Content, response.RawBytes);
        }

        public IApiResponse DownloadFile(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;

            var downloader = new RestClient();

            var request = new RestRequest(url);

            var response = downloader.Execute(request);

            return new FohlioApiResponse(response.StatusCode, response.Content, response.RawBytes);
        }

        private static RestRequest PrepareRequest(ApiCallQuery query)
        {
            var request = new RestRequest(query.Path, GetMethod(query.Method));

            foreach (var pathParameter in query.PathParameters)
                request.AddParameter(pathParameter.ParameterName, pathParameter.Value, ParameterType.UrlSegment);

            foreach (var headerParameter in query.HeaderParameters)
                request.AddHeader(headerParameter.ParameterName, headerParameter.Value);

            foreach (var queryParameter in query.QueryParameters)
                request.AddQueryParameter(queryParameter.ParameterName, queryParameter.Value);

            foreach (var formParameter in query.FormParameters)
                request.AddParameter(formParameter.ParameterName, formParameter.Value);
            
            if (query.Body != null)
            {
                if (!string.IsNullOrWhiteSpace(query.ContentType))
                    request.AddParameter(query.ContentType, query.Body, ParameterType.RequestBody);
                else if (query.Body is string)
                    request.AddParameter("application/json", query.Body, ParameterType.RequestBody);
            }

            return request;
        }

        private static Method GetMethod(ApiMethod method)
        {
            switch (method)
            {
                case ApiMethod.GET:
                    return Method.GET;

                case ApiMethod.POST:
                    return Method.POST;

                case ApiMethod.PUT:
                    return Method.PUT;

                case ApiMethod.PATCH:
                    return Method.PATCH;

                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }
    }
}