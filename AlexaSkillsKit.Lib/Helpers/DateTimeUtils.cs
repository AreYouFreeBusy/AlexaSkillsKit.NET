using System;
using System.Globalization;

namespace AlexaSkillsKit.Helpers
{
    public class DateTimeUtils
    {
        public static DateTime FromAmazonRequest(string datetimeString)
        {
            try
            {
                DateTime datetime;

                //Try a generic parse
                if (DateTime.TryParse(datetimeString, CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime))
                {
                    return datetime;
                }

                //Try a ticks parse
                long ticks;
                long.TryParse(datetimeString, out ticks);
                if (ticks > 0)
                {
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    datetime = epoch.AddMilliseconds(ticks);
                    return datetime;
                }

                //Try an offset parse for the Iso format
                var offset = DateTimeOffset.Parse(datetimeString);
                return offset.DateTime;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }
    }
}
