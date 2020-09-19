using System.Collections.Generic;

namespace Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls
{
    public class ApiCallQuery
    {
        private readonly IList<IApiParameter> queryParameters = new List<IApiParameter>();
        private readonly IList<IApiParameter> headerParameters = new List<IApiParameter>();
        private readonly IList<IApiParameter> formParameters = new List<IApiParameter>();
        private readonly IList<IApiParameter> pathParameters = new List<IApiParameter>();

        internal ApiCallQuery(string path, ApiMethod method, string contentType, object body = null)
        {
            Path = path;
            Method = method;
            ContentType = contentType;
            Body = body;
        }

        public string Path { get; }

        public ApiMethod Method { get; }

        public string ContentType { get; }

        public object Body { get; }

        public IEnumerable<IApiParameter> QueryParameters => queryParameters;

        public IEnumerable<IApiParameter> HeaderParameters => headerParameters;

        public IEnumerable<IApiParameter> FormParameters => formParameters;

        public IEnumerable<IApiParameter> PathParameters => pathParameters;
        
        internal void AddQueryParameter(IApiParameter parameter) => queryParameters.Add(parameter);

        internal void AddHeaderParameter(IApiParameter parameter) => headerParameters.Add(parameter);

        internal void AddFormParameter(IApiParameter parameter) => formParameters.Add(parameter);

        internal void AddPathParameter(IApiParameter parameter) => pathParameters.Add(parameter);
    }
}