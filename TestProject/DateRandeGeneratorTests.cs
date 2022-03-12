using NUnit.Framework;
using SqlScriptGenerator.Partitition;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class DateRandeGeneratorTests
    {
        [Test]
        public void GenerateDatesByMonth_Test(
            [Values(1, 15, 31)] int day,
            [Values(1, 5, 12, 54, 100, 1000)] int quantity)
        {
            var start = new DateTime(2022, 3, 1);
            var dates = new List<DateTime>(quantity);
            dates.Add(new DateTime(start.Year, start.Month, day));

            for (int i = 1; i < quantity; i++)
            {
                dates.Add(start.AddMonths(i));
            }

            var result = DateRangeGenerator.GenerateDatesByMonth(quantity, new DateTime(start.Year, start.Month, day)).ToList();

            Assert.AreEqual(dates.Count, result.Count);
            for (var i = 0; i < dates.Count; i++)
            {
                Assert.AreEqual(dates[i], result[i]);
                if (i > 0)
                    Assert.AreNotEqual(result[i - 1], result[i]);
            }
        }
    }
}
