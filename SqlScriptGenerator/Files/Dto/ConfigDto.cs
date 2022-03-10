using Newtonsoft.Json;
using System.Collections.Generic;

namespace SqlScriptGenerator.Files.Dto
{
    internal class ConfigDto
    {
        [JsonProperty("databases")]
        public ICollection<DatabaseDto> Databases { get; set; }

        [JsonProperty("partitition")]
        public PartititionDto Partitition { get; set; }
    }
}
