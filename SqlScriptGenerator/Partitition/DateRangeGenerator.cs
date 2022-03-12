using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlScriptGenerator.Partitition
{
    public static class DateRangeGenerator
    {
        public static ICollection<DateTime> GenerateDates(int quantity, DateTime startDate)
        {
            return Enumerable.Range(1, quantity - 1)
                .Select(x => new DateTime(
                    startDate.Year + (int)((startDate.Month + x) / 13),
                    (startDate.Month + x) % 13 + ((startDate.Month + x) / 13),
                    1))
                .Prepend(startDate)
                .ToArray();
        }

        public static ICollection<DateTime> GenerateDatesByMonth(int quantity, DateTime startDate)
        {
            var dates = new List<DateTime>(quantity);
            dates.Add(startDate);

            var start = new DateTime(startDate.Year, startDate.Month, 1);
            for (int i = 1; i < quantity; i++)
            {
                dates.Add(start.AddMonths(i));
            }

            return dates;
        }


        public static ICollection<DateTime> GenerateDatesByMonthArray(int quantity, DateTime startDate)
        {
            var dates = new DateTime[quantity];
            dates[0] = startDate;

            var start = new DateTime(startDate.Year, startDate.Month, 1);
            for (int i = 1; i < quantity; i++)
            {
                dates[i] = start.AddMonths(i);
            }

            return dates;
        }
    }
}
