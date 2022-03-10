using Newtonsoft.Json;
using SqlScriptGenerator.Files;
using SqlScriptGenerator.Serializer.Dto;
using System;

namespace SqlScriptGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string configPath = "config.json";
            var config = FileManager.Read<Config>(configPath);
            Console.WriteLine(JsonConvert.SerializeObject(config));
        }
    }
}
