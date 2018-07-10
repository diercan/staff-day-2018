using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeMonadTest.Interpreters
{
    public abstract class OpInterpreter<O, R> : IInterpreter
    {
        public bool AppliesTo<A>(IO<A> ma) => ma is IO<O, R, A>;
        protected IO<O, R, A> Cast<A>(IO<A> ma) => (IO<O, R, A>)ma;
        public A Interpret<A>(IO<A> ma, Func<IO<A>, A> interpret) => interpret(Cast(ma).Do(Work(Cast(ma).Op)));
        public A Mock<A>(IO<A> ma, Func<IO<A>, A> interpret)
            => interpret(Cast(ma).Do(Mock(Cast(ma).Op)));

        public abstract R Work(O Op);
        public abstract R Mock(O Op);
    }

    public interface IInterpreter
    {
        bool AppliesTo<A>(IO<A> ma);
        A Interpret<A>(IO<A> ma, Func<IO<A>, A> interpret);
        A Mock<A>(IO<A> ma, Func<IO<A>, A> interpret);
    }
}
