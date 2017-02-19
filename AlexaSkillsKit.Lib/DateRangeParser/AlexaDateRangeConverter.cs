using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AlexaSkillsKit.DateRangeParser
{
    public static class AlexaDateRangeConverter
    {
        private const string Year = "year";
        private const string Week = "week";
        private const string Month = "month";
        private const string Day = "day";
        private const string Quarter = "quarter";

        private static readonly string DecadeRegX = $"^(?<{Year}>\\d{{3}})X$";
        private static readonly string WeekRegX = $"^(?<{Year}>\\d{{4}})-W(?<{Week}>\\d+)$";
        private static readonly string WeekendRegX = $"^(?<{Year}>\\d{{4}})-W(?<{Week}>\\d+)-WE$";
        private static readonly string QuarterRegX = $"^(?<{Year}>\\d{{4}})-Q(?<{Quarter}>[1234])$";
        private static readonly string MonthRegX = $"^(?<{Year}>\\d{{4}})-(?<{Month}>\\d+)$";
        private static readonly string DateRegX = $"^(?<{Year}>\\d{{4}})-(?<{Month}>\\d+)-(?<{Day}>\\d+)$";

        private static AlexaDateRange ConvertDecade(Match match, TimeSpan offset)
        {
            if (!match.Success) throw new AlexaDateRangeConvertException("Expected successful regex match.");
            var yearValue = System.Convert.ToInt32(match.Groups[Year].Value);
            return new AlexaDateRange(
                CreateDate(yearValue * 10, 1, 1, offset, false),
                CreateDate((yearValue * 10) + 9, 12, 31, offset, true),
                "Decade");
        }

        private static AlexaDateRange ConvertMonth(Match match, TimeSpan offset)
        {
            if (!match.Success) throw new AlexaDateRangeConvertException("Expected successful regex match.");
            var yearValue = System.Convert.ToInt32(match.Groups[Year].Value);
            var monthValue = System.Convert.ToInt32(match.Groups[Month].Value);

            var start = CreateDate(yearValue, monthValue, 1, offset, false);
            var end = CreateDate(yearValue, monthValue, 1, offset, true).AddMonths(1).AddDays(-1);
            return new AlexaDateRange(start, end, "Month");
        }

        private static AlexaDateRange ConvertDate(Match match, TimeSpan offset)
        {
            if (!match.Success) throw new AlexaDateRangeConvertException("Expected successful regex match.");
            var yearValue = System.Convert.ToInt32(match.Groups[Year].Value);
            var monthValue = System.Convert.ToInt32(match.Groups[Month].Value);
            var dayValue = System.Convert.ToInt32(match.Groups[Day].Value);

            var start = CreateDate(yearValue, monthValue, dayValue, offset, false);
            var end = CreateDate(yearValue, monthValue, dayValue, offset, true);
            return new AlexaDateRange(start, end, "Date");
        }

        private static AlexaDateRange ConvertWeek(Match match, TimeSpan offset)
        {
            if (!match.Success) throw new AlexaDateRangeConvertException("Expected successful regex match.");
            var yearValue = System.Convert.ToInt32(match.Groups[Year].Value);
            var weekValue = System.Convert.ToInt32(match.Groups[Week].Value);

            var start = CreateDate(yearValue, 1, 1, offset, false);
            while (start.DayOfWeek != DayOfWeek.Monday)
                start = start.AddDays(1);
            start = start.AddDays(7 * (weekValue - 1));
            var end = CreateDate(start.Year, start.Month, start.Day, offset, true).AddDays(6);

            return new AlexaDateRange(start, end, "Week");
        }

        private static AlexaDateRange ConvertWeekend(Match match, TimeSpan offset)
        {
            var week = ConvertWeek(match, offset);
            return new AlexaDateRange(week.StartDate.AddDays(5), week.EndDate, "Weekend");
        }

        private static AlexaDateRange ConvertQuarter(Match match, TimeSpan offset)
        {
            if (!match.Success) throw new AlexaDateRangeConvertException("Expected successful regex match.");
            var yearValue = System.Convert.ToInt32(match.Groups[Year].Value);
            var quarterValue = System.Convert.ToInt32(match.Groups[Quarter].Value);
            var months = new[] { 1, 4, 7, 10 };
            var start = CreateDate(yearValue, months[quarterValue - 1], 1, offset, false);
            var end = CreateDate(yearValue, months[quarterValue - 1], 1, offset, true).AddMonths(3).AddDays(-1);
            return new AlexaDateRange(start, end, "Quarter");
        }

        private static DateTime CreateDate(int year, int month, int day, TimeSpan offset, bool endOfDay)
        {
            var tm = new DateTime(2017, 1, 1, 0, 0, 0).Add(-offset);
            var tz = new DateTime(
                year, month, day,
                tm.Hour, tm.Minute, tm.Second, tm.Millisecond,
                DateTimeKind.Utc);
            return endOfDay ? tz.Add(new TimeSpan(0, 23, 59, 59, 999)) : tz;
        }

        public class RegExTry
        {
            private readonly string _regEx;
            private readonly Func<Match, TimeSpan, AlexaDateRange> _converter;
            public string Name { get; set; }

            public AlexaDateRange TryConvert(string dateString, TimeSpan offset)
            {
                if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException(nameof(dateString));

                var regex = new Regex(_regEx);
                var match = regex.Match(dateString);
                if (!match.Success)
                    return null;

                var converted = _converter(match, offset);
                Debug.WriteLine($"Found match for {Name}, date range {converted.StartDate} to {converted.EndDate}");
                return converted;
            }

            public RegExTry(string regEx, Func<Match, TimeSpan, AlexaDateRange> converter, string name)
            {
                _regEx = regEx;
                _converter = converter;
                Name = name;
            }
        }

        public static readonly RegExTry[] RegexesToTry =
        {
            new RegExTry(DecadeRegX, ConvertDecade, "Decade"),
            new RegExTry(WeekRegX, ConvertWeek, "Week"),
            new RegExTry(WeekendRegX, ConvertWeekend, "Weekend"),
            new RegExTry(MonthRegX, ConvertMonth, "Month"),
            new RegExTry(DateRegX, ConvertDate, "Date"),
            new RegExTry(QuarterRegX, ConvertQuarter, "Quarter"),
        };

        /// <summary>
        /// Convert an Alexa date format to a date range.
        /// </summary>
        /// <param name="date">The date string from Alexa</param>
        /// <returns>AlexaDateRange</returns>
        public static AlexaDateRange Convert(string date)
        {
            return Convert(date, TimeSpan.Zero);
        }

        /// <summary>
        /// Convert an Alexa date format to a date range.
        /// </summary>
        /// <param name="date">The date string from Alexa</param>
        /// <param name="offset">The timezone offset from UTC time</param>
        /// <returns>AlexaDateRange</returns>
        public static AlexaDateRange Convert(string date, TimeSpan offset)
        {
            if (string.IsNullOrEmpty(date)) throw new ArgumentNullException(nameof(date));

            foreach (var rex in RegexesToTry)
            {
                AlexaDateRange gotRange;
                if ((gotRange = rex.TryConvert(date, offset)) != null)
                    return gotRange;
            }

            return null;
        }
    }
}
