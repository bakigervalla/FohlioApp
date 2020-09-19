namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class FohlioAuthentificationOptions
    {
        public FohlioAuthentificationOptions(string email, string password)
        {
            Email = email;

            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}