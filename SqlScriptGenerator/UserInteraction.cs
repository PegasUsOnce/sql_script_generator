using System;
using System.Globalization;
using System.Text;

namespace SqlScriptGenerator
{
    /// <summary>
    /// Взаимодействие с пользователем
    /// </summary>
    internal class UserInteraction
    {
        public UserInteraction()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public void Write(string text = null)
        {
            Console.WriteLine(text);
        }

        public int ReadInt()
        {
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        public DateTime ReadDate(string format)
        {
            var result = Console.ReadLine();
            DateTime date;
            while (!DateTime.TryParseExact(result, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Write($"Не удалось распознать дату, повторите ввод в формате {format}:");
                result = Console.ReadLine();
            }

            return date;
        }
    }
}
