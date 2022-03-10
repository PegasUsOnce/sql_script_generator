using Newtonsoft.Json;

namespace SqlScriptGenerator.Serializer.Dto
{
    internal class Database
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
