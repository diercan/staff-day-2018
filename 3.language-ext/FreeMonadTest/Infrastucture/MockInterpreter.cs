using FreeMonadTest.Interpreters;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMonadTest.Infrastucture
{
    public class MockInterpreter
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
                     .Mock(ma, Interpret);
        }
    }
}
