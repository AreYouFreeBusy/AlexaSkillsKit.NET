using System;
using System.Linq;
using AlexaSkillsKit.DateRangeParser;
using Xunit;

namespace AlexaSkillsKit.Tests.DateRangeParser
{
    public class AmazonDateConvertTests
    {
        [Fact]
        public void TestDateRangeConverters()
        {
            var r = AlexaDateRangeConverter.Convert("201X");
            Assert.True(new DateTime(2010, 1, 1, 0, 0, 0) == r.StartDate);

            r = AlexaDateRangeConverter.Convert("2017-W01");
            Assert.True(new DateTime(2017, 1, 2, 0, 0, 0) == r.StartDate);

            r = AlexaDateRangeConverter.Convert("2017-01");
            Assert.True(new DateTime(2017, 1, 1, 0, 0, 0) == r.StartDate);

            r = AlexaDateRangeConverter.Convert("2017-02");
            Assert.True(new DateTime(2017, 2, 1, 0, 0, 0) == r.StartDate);

            r = AlexaDateRangeConverter.Convert("2016-Q4");
            Assert.True(new DateTime(2016, 10, 1, 0, 0, 0) == r.StartDate);

            r = AlexaDateRangeConverter.Convert("2017-W02-WE");
            Assert.True(new DateTime(2017, 1, 14, 0, 0, 0) == r.StartDate);
        }

        [Fact]
        public void TestDecade()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Decade");
            var r = rexTry.TryConvert("201X", TimeSpan.Zero);
            Assert.True(new DateTime(2010, 1, 1, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2019, 12, 31, 23, 59, 59, 999) == r.EndDate);
        }

        [Fact]
        public void TestWeek()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Week");
            var r = rexTry.TryConvert("2017-W01", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 2, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 8, 23, 59, 59, 999) == r.EndDate);

            rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Week");
            r = rexTry.TryConvert("2017-W02", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 9, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 15, 23, 59, 59, 999) == r.EndDate);
        }

        [Fact]
        public void TestWeekend()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Weekend");
            var r = rexTry.TryConvert("2017-W01-WE", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 7, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 8, 23, 59, 59, 999) == r.EndDate);

            rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Weekend");
            r = rexTry.TryConvert("2017-W02-WE", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 14, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 15, 23, 59, 59, 999) == r.EndDate);
        }

        [Fact]
        public void TestMonth()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Month");
            var r = rexTry.TryConvert("2017-01", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 1, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 31, 23, 59, 59, 999) == r.EndDate);

            rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Month");
            r = rexTry.TryConvert("2017-02", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 2, 1, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 2, 28, 23, 59, 59, 999) == r.EndDate);
        }

        [Fact]
        public void TestDate()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Date");
            var r = rexTry.TryConvert("2017-01-02", TimeSpan.Zero);
            Assert.True(new DateTime(2017, 1, 2, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2017, 1, 2, 23, 59, 59, 999) == r.EndDate);
        }

        [Fact]
        public void TestQuarter()
        {
            var rexTry = AlexaDateRangeConverter.RegexesToTry.First(rt => rt.Name == "Quarter");
            var r = rexTry.TryConvert("2016-Q4", TimeSpan.Zero);
            Assert.True(new DateTime(2016, 10, 1, 0, 0, 0) == r.StartDate);
            Assert.True(new DateTime(2016, 12, 31, 23, 59, 59, 999) == r.EndDate);
        }
    }
}
