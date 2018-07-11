using ConsoleApp8;
using LanguageExt;
using System;

namespace FreeMonadTest.Interpreters
{
    public class LogBinding : IBinding
    {
        public bool AppliesTo<A>(IO<A> ma)
        {
            return ma is IO<A>.Log;
        }

        public IO<B> Bind<A, B>(IO<A> ma, Func<A, IO<B>> f)
        {
            var log = (IO<A>.Log)ma;
            return new IO<B>.Log(log.Output, n => log.Next(n).Bind(f));
        }
    }
}