using System.Net;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.Services.Entities.ApiCalls;

namespace Fohlio.RevitReportsIntegration.Services.Services.Implementation
{
    internal class FohlioAuthentificationService : IFohlioAuthentificationService
    {
        private readonly IApiCaller apiClient;
        private readonly ISerializer serializer;

        public FohlioAuthentificationService(IApiCaller apiClient, ISerializer serializer)
        {
            this.apiClient = apiClient;
            this.serializer = serializer;
        }

        public ServiceResponse<AccessToken> Authentificate(FohlioAuthentificationOptions options)
        {
            var apiCallOptions = new ApiCallQuery("/openapi/tokens/current", ApiMethod.GET, "application/json");

            apiCallOptions.AddHeaderParameter(new ApiParameter("Accept", "application/json"));

            apiCallOptions.AddQueryParameter(new ApiParameter("email", options.Email));
            apiCallOptions.AddQueryParameter(new ApiParameter("password", options.Password));

            var response = apiClient.CallApi(apiCallOptions);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = serializer.Deserialize(response.Content);

                    return ServiceResponse<AccessToken>.Success(HttpStatusCode.OK, new AccessToken(result.value));

                case HttpStatusCode.Unauthorized:
                    return ServiceResponse<AccessToken>.Fail(HttpStatusCode.Unauthorized, "Unauthorized!", null);

                case HttpStatusCode.Forbidden:
                    try
                    {
                        var content = serializer.Deserialize(response.Content);

                        return ServiceResponse<AccessToken>.Fail(HttpStatusCode.Forbidden, (string)content.error, null, (string)content.description);
                    }
                    catch
                    {
                        return ServiceResponse<AccessToken>.Fail(HttpStatusCode.Forbidden, "Forbidden!", null);
                    }

                default:
                    return ServiceResponse<AccessToken>.Fail(response.StatusCode, "Unknown server communication error occured", null);
            }
        }
    }
}