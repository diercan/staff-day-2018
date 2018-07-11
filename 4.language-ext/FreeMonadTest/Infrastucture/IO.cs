using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IOExt;


public interface IO<A>
{
    IO<B> Bind<B>(Func<A, IO<B>> f);
}

public class IO<O, R, A> : IO<A>
{
    public readonly O Op;
    public readonly Func<R, IO<A>> Do;
    public IO(O op, Func<R, IO<A>> @do)
    {
        Op = op;
        Do = @do;
    }
    public IO<B> Bind<B>(Func<A, IO<B>> f) => new IO<O, R, B>(Op, r => Do(r).Bind(f));
}


public class Return<A> : IO<A>
{
    public readonly A Value;
    public Return(A value) =>
        Value = value;

    public IO<B> Bind<B>(Func<A, IO<B>> f) => f(Value);
}

