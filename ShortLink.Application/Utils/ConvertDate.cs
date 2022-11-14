using System;
using System.Globalization;

namespace ShortLink.Application.Utils
{
    public static class ConvertDate
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString();
        }
    }
}
