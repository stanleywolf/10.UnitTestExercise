using System;

namespace _09.DateTime
{
    public class MyDateTime:IDateTime
    {
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }

        public void AddDays(System.DateTime date, double daysToAdd)
        {
            date.AddDays(daysToAdd);
        }

        public TimeSpan SubstractDays(System.DateTime date, int daysToSub)
        {
            return date.Subtract(System.DateTime.Parse($"{daysToSub}",System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
