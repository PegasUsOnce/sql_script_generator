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
            var interaction = new UserInteraction();

            const string configPath = "config.json";
            var config = await FileManager.ReadAsync<ConfigDto>(configPath);

            interaction.Write("**********************************************************");
            interaction.Write("Скрипт генерирует новую файловую группу и новый файл для одной секции на 1 месяц.");
            interaction.Write(@"Если начальная дата отлична от первого числа месяца, 
                то скрипт сгенерирует первую серцию от указанной даты до начала следующего месяца.");
            interaction.Write("Для каждой БД указанной в конфиге сгенерируеся новый файл в папке Generated");
            interaction.Write();
            interaction.Write("Если вам не нравится, то переписывайте генератор или правьте сгенерированные ручками");
            interaction.Write("**********************************************************");
            interaction.Write();

            const string requiredDateFormat = "dd.MM.yyyy";
            interaction.Write($" - Введите дату в формате {requiredDateFormat} с начала которой нужно генерировать скрипт:");
            var startDate = interaction.ReadDate(requiredDateFormat);

            interaction.Write(" - Введите количество месяцев для генерации скрипта:");
            var monthQuantity = interaction.ReadInt();
            if (monthQuantity == 0 || monthQuantity > 13)
            {
                interaction.Write("Я так не умею!");
                return;
            }

            var scripts = new PartititionScriptGenerator(config.Partitition, config.Databases)
                .Generate(monthQuantity, startDate);

            await Task.WhenAll(scripts.Select(s => FileManager.WriteAsync(s)).ToArray());
            interaction.Write("Я сделяль!");
        }
    }
}
