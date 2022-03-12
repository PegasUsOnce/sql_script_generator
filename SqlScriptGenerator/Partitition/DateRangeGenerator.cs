using System;
using System.Collections.Generic;

namespace SqlScriptGenerator.Partitition
{
    public static class DateRangeGenerator
    {
        public static ICollection<DateTime> GenerateDatesByMonth(int quantity, DateTime startDate)
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
