using ConsoleApp8;
using System;

namespace FreeMonadTest.Interpreters
{
    public class ReturnBinding : IBinding
    {
        public bool AppliesTo<A>(IO<A> ma) => ma is IO<A>.Return;
        public IO<B> Bind<A, B>(IO<A> ma, Func<A, IO<B>> f)
        {
            var ret = (IO<A>.Return)ma;
            return f(ret.Value);
        }
    }
}