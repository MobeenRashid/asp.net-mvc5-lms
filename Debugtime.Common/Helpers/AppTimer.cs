using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Helpers
{
    public class AppTimer
    {
        public static string LastUpdateTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now.Subtract(dateTime);

            var minutes = (int)timeSpan.TotalMinutes == 0 ? 1 : (int)timeSpan.TotalMinutes;
            var s = $"{minutes} mins";
            if (minutes < 60)
                return s + " ago";

            var hrs = minutes / 60;
            s = $"{hrs} hrs";
            if (hrs < 24)
                return s + " ago";

            var days = hrs / 24;
            s = $"{days} days";
            if (days < 30)
                return s + " ago";

            var months = days / 30;
            s = $"{months} months";
            if (months < 12)
                return s + " ago";

            var years = months / 12;
            s = $"{years} years";
            return s + " ago";
        }
    }
}
