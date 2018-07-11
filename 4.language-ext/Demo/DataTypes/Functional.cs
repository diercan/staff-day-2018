using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.TypeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace Demo.DataTypes
{
    static class Functional
    {
        public static DateTime CreateDate(Year year, Month month, Day day)
        {
            return new DateTime(year.Value, month.Value, day.Value);
        }

        public static Option<string> GetDateString(Year year, Month month, Day day)
        {
            if(year.Value >=2000 && year.Value<=2100 &&
               month.Value>=1 && month.Value <= 12 &&
               day.Value >=1 && day.Value<=DateTime.DaysInMonth(year.Value, month.Value))
            {
                return new DateTime(year.Value, month.Value, day.Value).ToShortDateString();
            }
            else
            {
                return Option<string>.None;
            }
        }

        public static Try<DateTime> TryCreateDate(Year year, Month month, Day day)
        => () =>
        {
            return new DateTime(year.Value, month.Value, day.Value);
        };

        public static Validation<string, DateTime> ValidateAndCreateDate(Year year, Month month, Day day)
        {
            if (year.Value >= 2000 && year.Value <= 2100 &&
               month.Value >= 1 && month.Value <= 12 &&
               day.Value >= 1 && day.Value <= DateTime.DaysInMonth(year.Value, month.Value))
            {
                return Success<string, DateTime>(new DateTime(year.Value, month.Value, day.Value));
            }
            else
            {
                return Fail<string, DateTime>("Invalid date");
            }
        }
    }

    public class Year : NumType<Year, TInt, int, Year.Validator>
    {
        Year(int x) : base(x) { }

        public struct Validator : Pred<int>
        {
            public bool True(int value)
            {
                return value >= 2000 && value <= 2100;
            }
        }
    }

    public class Month : NumType<Month, TInt, int, Month.Validator>
    {
        Month(int x) : base(x) { }
        public struct Validator : Pred<int>
        {
            public bool True(int value)
            {
                return value >= 1 && value <= 12;
            }
        }
    }

    public class Day : NumType<Day, TInt, int, Day.Validator>
    {
        Day(int x) : base(x) { }

        public struct Validator : Pred<int>
        {
            public bool True(int value)
            {
                return value >= 1 && value <= 31;
            }
        }
    }
}
