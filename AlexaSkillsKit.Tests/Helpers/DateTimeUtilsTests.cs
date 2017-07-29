using System;
using AlexaSkillsKit.Helpers;
using Xunit;

namespace AlexaSkillsKit.Tests.Helpers
{
    public class DateTimeUtilsTests
    {
        [Fact]
        public void FromAmazonRequestIso8601Test()
        {
            var datetime = new DateTime(2017, 7, 14, 13, 57, 18, 208);
            var iso8601 = datetime.ToString("o");

            var returnedDate = DateTimeUtils.FromAmazonRequest(iso8601);
            Assert.Equal(datetime, returnedDate);
        }

        [Fact]
        public void FromAmazonRequestStringTicksTest()
        {
            var ticks = "1500040638208";
            var tickDate = new DateTime(2017, 7, 14, 13, 57, 18, 208, DateTimeKind.Utc);

            var returnedDate = DateTimeUtils.FromAmazonRequest(ticks);            
            Assert.Equal(tickDate, returnedDate);
        }

        [Fact]
        public void FromAmazonRequestSortableOutputFailTest()
        {
            var datetime = new DateTime(2017, 7, 14, 13, 57, 18, 208);
            var sortable = datetime.ToString("s");

            var returnedDate = DateTimeUtils.FromAmazonRequest(sortable);
            Assert.NotEqual(datetime, returnedDate);
        }

        [Fact]
        public void FromAmazonRequestBadStringFailTest()
        {
            var datetime = new DateTime(2017, 7, 14, 13, 57, 18, 208);
            var garbage = "2017/7.14";

            var returnedDate = DateTimeUtils.FromAmazonRequest(garbage);
            Assert.NotEqual(datetime, returnedDate);
        }
    }
}
