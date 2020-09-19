using System.Net;

namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class ServiceResponse
    {
        protected ServiceResponse(bool isSuccess, HttpStatusCode statusCode, string message, string extendedMessage = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message;
            ExtendedMessage = extendedMessage;
        }

        public bool IsSuccess { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public string Message { get; private set; }

        public string ExtendedMessage { get; private set; }

        public static ServiceResponse Success(HttpStatusCode statusCode) => new ServiceResponse(true, statusCode, null);

        public static ServiceResponse Fail(HttpStatusCode statusCode, string message) => new ServiceResponse(false, statusCode, message);
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        private ServiceResponse(bool isSuccess, HttpStatusCode statusCode, T data, string message, string extendedMessage = null) : base(isSuccess, statusCode, message, extendedMessage)
        {
            Data = data;
        }

        public T Data { get; }

        public static ServiceResponse<T> Success(HttpStatusCode statusCode, T data) => new ServiceResponse<T>(true, statusCode, data, null);

        public static ServiceResponse<T> Fail(HttpStatusCode statusCode, string message, T data, string extendedMessage = null) 
            => new ServiceResponse<T>(false, statusCode, data, message, extendedMessage);
    }
}