namespace Fohlio.RevitReportsIntegration.Services.Services
{
    public interface ISerializer
    {
        string Serialize(object obj);

        dynamic Deserialize(string content);

        dynamic DeserializeArray(string content);
    }
}