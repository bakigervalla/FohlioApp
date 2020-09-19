namespace Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls
{
    internal class ApiParameter : IApiParameter
    {
        public ApiParameter(string parameterName, string value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public string ParameterName { get; }

        public string Value { get; }
    }
}