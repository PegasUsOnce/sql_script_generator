using Newtonsoft.Json;
using System.Collections.Generic;

namespace SqlScriptGenerator.Serializer.Dto
{
    internal class Partitition
    {
        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("schemes")]
        public ICollection<string> Schemes { get; set; }
    }
}
