using Demo.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTypes();
        }

        private static void TestTypes()
        {
            #region standard
            var year = 2017;
            var month = 5;
            var day = 6;
            var date = Primitive.CreateDate(year, month, day);
            var date2 = Primitive.CreateDate(year, day, month);
            #endregion standard

            #region domain specific
            var y = Year.New(2017);
            var m = Month.New(5);
            var d = Day.New(6);
            var date4 = Functional.CreateDate(y, m, d);
            #endregion domain specific
        }

        private static void NullValues()
        {
            #region standard
            var dateStr = Primitive.GetDateString(2017, 16, 10);
            if (dateStr != null)
            {
                Console.WriteLine($"There are {dateStr.Length} characters.");
            }
            #endregion standard

            #region functional
            var dateStr2 = Functional.GetDateString(Year.New(2017), Month.New(2), Day.New(29));
            dateStr2.Match(
                    Some: str => Console.WriteLine($"There are {str.Length} characters."),
                    None: () => Console.WriteLine("Date is invalid")
                );
            #endregion functional
        }

        private static void FaultyCases()
        {
            #region standard
            var year = 2017;
            var month = 2;
            var day = 29;
            try
            {
                var date3 = Primitive.CreateDate(year, day, month);
            }
            catch
            {
                Console.WriteLine("Invalid date");
            }
            #endregion standard

            #region functional
            var y = Year.New(2017);
            var m = Month.New(5);
            var d = Day.New(6);

            #region v1
            var date4 = Functional.TryCreateDate(y, m, d);
            date4.Match(
                    Succ: value => Console.WriteLine(value.ToShortDateString()),
                    Fail: ex=>Console.WriteLine("Invalid date")
                );
            #endregion v1

            #region v2
            var date5 = Functional.ValidateAndCreateDate(y, m, d);
            date5.Match(
                    Succ: value => Console.WriteLine(value.ToShortDateString()),
                    Fail: messages => messages.Iter(x=> Console.WriteLine(x))                      
                );
            #endregion v2
            #endregion functional
        }

        private static void ReduceBoilerPlate()
        {
            #region standard
            var date1 = Primitive.TryCreateDate(2017, 1, 1);
            if (date1 != null)
            {
                var date2 = Primitive.TryCreateDate(2017, 1, 2);
                if (date2 != null)
                {
                    var diff = date2 - date1;
                }
            }
            #endregion standard

            #region functional
            #region V1
            var period = from d1 in Functional.ValidateAndCreateDate(Year.New(2017), Month.New(1), Day.New(1))
                         from d2 in Functional.ValidateAndCreateDate(Year.New(2017), Month.New(1), Day.New(2))
                         select d2 - d1;
            period.Match(
                    Succ: v=>Console.WriteLine(v.ToString()),
                    Fail: messages=>messages.Iter(msg=>Console.WriteLine(msg))
                );
            #endregion V1

            #region V2
            var period2 = from d1 in Functional.TryCreateDate(Year.New(2017), Month.New(1), Day.New(1))
                         from d2 in Functional.TryCreateDate(Year.New(2017), Month.New(1), Day.New(2))
                         select d2 - d1;
            period2.Match(
                    Succ: v => Console.WriteLine(v.ToString()),
                    Fail: ex => Console.WriteLine(ex.ToString())
                );
            #endregion V2
            #endregion functional

        }
    }
}
