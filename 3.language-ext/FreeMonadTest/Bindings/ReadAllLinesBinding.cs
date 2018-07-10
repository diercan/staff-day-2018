using ConsoleApp8;
using LanguageExt;
using System;

namespace FreeMonadTest.Interpreters
{
    public class ReadAllLinesBinding : IBinding
    {
        public bool AppliesTo<A>(IO<A> ma) => ma is IO<A>.ReadAllLines;
        public IO<B> Bind<A, B>(IO<A> ma, Func<A, IO<B>> f)
        {
            var ra = (IO<A>.ReadAllLines)ma;
            return new IO<B>.ReadAllLines(ra.Path, n => ra.Next(n).Bind(f));
        }
    }
}