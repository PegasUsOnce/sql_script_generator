using SqlScriptGenerator.Files;
using SqlScriptGenerator.Files.Dto;
using SqlScriptGenerator.Partitition;
using System.Linq;
using System.Threading.Tasks;

namespace SqlScriptGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string configPath = "config.json";
            var config = await FileManager.ReadAsync<ConfigDto>(configPath);

            var scripts = new PartititionScriptGenerator(config.Partitition, config.Databases, 3, 12).Generate();

            await Task.WhenAll(scripts.Select(s => FileManager.WriteAsync(s)).ToArray());
        }
    }
}
