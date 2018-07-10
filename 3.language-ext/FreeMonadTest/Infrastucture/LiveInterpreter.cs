using ConsoleApp8;
using FreeMonadTest.Interpreters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreeMonadTest
{
    public static class LiveInterpreter
    {
        private static readonly IEnumerable<IInterpreter> _interpreters =
            typeof(LiveInterpreter).Assembly.GetTypes()
            .Where(p => typeof(IInterpreter).IsAssignableFrom(p) && !p.IsInterface && !p.ContainsGenericParameters)
            .Select(p => (IInterpreter)Activator.CreateInstance(p))
            .AsEnumerable();

        public static A Interpret<A>(IO<A> ma)
        {
            return ma is Return<A> r ? r.Value
                : _interpreters.First(p => p.AppliesTo(ma))
                     .Interpret(ma, Interpret);
        }
    }
}
