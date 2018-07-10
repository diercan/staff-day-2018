using ConsoleApp8;
using LanguageExt;
using System;

namespace FreeMonadTest.Interpreters
{
    public class Domain1WriteAllLinesBinding : IBinding
    {
        public bool AppliesTo<A>(IO<A> ma)
        {
            return ma is IO<WriteAllLines, Unit, A>;
        }

        public IO<B> Bind<A, B>(IO<A> ma, Func<A, IO<B>> f)
        {
            var ra = (Domain1<A>.WriteAllLines)ma;
            return new Domain1<B>.WriteAllLines(ra.path, ra.lines, n => ra.next(n).Bind(f));
        }
    }
}