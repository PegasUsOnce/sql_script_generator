using Newtonsoft.Json;
using System.IO;

namespace SqlScriptGenerator.Files
{
    internal class FileManager
    {
        public static T Read<T>(string path)
        {
            var text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
