namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class AccessToken
    {
        internal AccessToken(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}