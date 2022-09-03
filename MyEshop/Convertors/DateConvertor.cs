using System.Globalization;

namespace MyEshop.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/"
                + pc.GetDayOfMonth(date).ToString("00");
        }
    }
}
