using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataTypes
{
    static class Primitive
    {
        public static DateTime CreateDate(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        public static DateTime? TryCreateDate(int year, int month, int day)
        {
            try
            {
                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }

        public static string GetDateString(int year, int month, int day)
        {
            if (year >= 2000 && year <= 2100 &&
               month >= 1 && month <= 12 &&
               day >= 1 && day <= DateTime.DaysInMonth(year, month))
            {
                return new DateTime(year, month, day).ToShortDateString();
            }
            else
            {
                return null;
            }
        }
    }
}
