using Newtonsoft.Json;
using System.Collections.Generic;

namespace SqlScriptGenerator.Files.Dto
{
    internal class PartititionDto
    {
        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("schemes")]
        public ICollection<string> Schemes { get; set; }
    }
}
