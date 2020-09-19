namespace Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls
{
    public interface IApiParameter
    {
        string ParameterName { get; }

        string Value { get; }
    }
}