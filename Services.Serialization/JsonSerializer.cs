using Fohlio.RevitReportsIntegration.Services.Serialization.Entities;
using Fohlio.RevitReportsIntegration.Services.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fohlio.RevitReportsIntegration.Services.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public dynamic Deserialize(string content) => new DynamicJsonResponse(JObject.Parse(content));

        public dynamic DeserializeArray(string content) => new DynamicJsonResponse(JArray.Parse(content));
    }
}