using Newtonsoft.Json;
using System.Collections.Generic;

namespace SqlScriptGenerator.Serializer.Dto
{
    internal class Config
    {
        [JsonProperty("databases")]
        public ICollection<Database> Databases { get; set; }

        [JsonProperty("partitition")]
        public Partitition Partitition { get; set; }
    }
}
