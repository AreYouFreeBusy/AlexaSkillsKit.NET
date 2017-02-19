using System;

namespace AlexaSkillsKit.DateRangeParser
{
    public class AlexaDateRange
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public string RangeType { get; }

        public AlexaDateRange(DateTime startDate, DateTime endDate, string rangeType)
        {
            StartDate = startDate;
            EndDate = endDate;
            RangeType = rangeType;
        }
    }
}
