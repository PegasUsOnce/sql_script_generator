using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace SqlScriptGenerator.Files
{
    internal class FileManager
    {
        public static async Task<T> ReadAsync<T>(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static async Task WriteAsync(Script script)
        {
            var path = $"Generated/{script.Name}.sql";

            if (!Directory.Exists("Generated"))
                Directory.CreateDirectory("Generated");

            if (File.Exists(path))
                File.Delete(path);

            // Create a file to write to.
            using var sw = File.CreateText(path);
            await sw.WriteAsync(script.Text);
        }
    }
}
