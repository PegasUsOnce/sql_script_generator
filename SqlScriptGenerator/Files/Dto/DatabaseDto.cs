using Newtonsoft.Json;

namespace SqlScriptGenerator.Files.Dto
{
    internal class DatabaseDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
