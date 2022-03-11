using SqlScriptGenerator.Files;
using SqlScriptGenerator.Files.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptGenerator.Partitition
{
    internal class PartititionScriptGenerator
    {
        private readonly PartititionDto _partitition;
        private readonly ICollection<DatabaseDto> _databases;

        public PartititionScriptGenerator(PartititionDto partitition, ICollection<DatabaseDto> databases)
        {
            _partitition = partitition;
            _databases = databases;
        }

        /// <summary>
        /// Генерирует скрипт: на один месяц одна файловая группа, один файл и одна секция
        /// </summary>
        public ICollection<Script> Generate(int quantity, DateTime startDate)
        {
            var dates = Enumerable.Range(1, quantity - 1)
                .Select(x => new DateTime(
                    startDate.Year + (int)((startDate.Month + x) / 13),
                    (startDate.Month + x) % 13 + ((startDate.Month + x) / 13),
                    1))
                .Prepend(startDate)
                .ToArray();

            var result = new List<Script>(_databases.Count);

            foreach (var database in _databases)
            {
                var resultDatabase = new StringBuilder();

                foreach (var date in dates)
                {
                    resultDatabase.AddFileGroupAndFile(database, date)
                        .AddRunScripts(_partitition, date);
                }

                result.Add(new Script
                {
                    Name = $"{database.Name}_script",
                    Text = resultDatabase.ToString()
                });
            }
            
            return result;
        }        
    }

    internal static class GeneratorExtensions
    {
        public static StringBuilder AddFileGroupAndFile(this StringBuilder sb, DatabaseDto database, DateTime date)
        {
            var fileGroupName = $"Period_{date.Year}_{date.Month}";
            sb.AppendLine($"ALTER DATABASE {database.Name}");
            sb.AppendLine($"ADD FILEGROUP {fileGroupName}");
            sb.AppendLine("GO");
            sb.AppendLine($"ALTER DATABASE {database.Name}");
            sb.AppendLine($"ADD FILE");
            sb.AppendLine("(");
            sb.AppendLine($"    NAME = {fileGroupName},");
            sb.AppendLine($"    FILENAME = N'{database.Location}\\{database.Name}_{fileGroupName}.ndf',");
            sb.AppendLine($"    SIZE = 8192KB,");
            sb.AppendLine($"    MAXSIZE = UNLIMITED,");
            sb.AppendLine($"    FILEGROWTH = 50MB");
            sb.AppendLine(")");
            sb.AppendLine($"TO FILEGROUP {fileGroupName}");
            sb.AppendLine("GO");
            sb.AppendLine("");

            return sb;
        }

        public static StringBuilder AddRunScripts(this StringBuilder sb, PartititionDto partitition, DateTime date)
        {
            var fileName = $"Period_{date.Year}_{date.Month}";

            foreach (var scheme in partitition.Schemes)
                sb.AppendLine($"ALTER PARTITION SCHEME [{scheme}] NEXT USED [{fileName}];");

            sb.AppendLine("GO");
            sb.AppendLine($"ALTER PARTITION FUNCTION {partitition.Function}() SPLIT RANGE (N'{date.Year}-{date.Month.ToString("00")}-{date.Day.ToString("00")}T00:00:00.000');");
            sb.AppendLine("GO");
            sb.AppendLine("");

            return sb;
        }
    }
}
