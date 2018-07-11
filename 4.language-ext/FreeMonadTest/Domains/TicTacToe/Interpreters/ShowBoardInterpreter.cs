using FreeMonadTest.Domains.TicTacToe.Operations;
using FreeMonadTest.Interpreters;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace FreeMonadTest.Domains.TicTacToe.Interpreters
{
    public class ShowBoardInterpreter : OpInterpreter<ShowBoard, Unit>
    {
        public override Unit Mock(ShowBoard Op)
        {
            return unit;
        }

        public override Unit Work(ShowBoard Op)
        {
            Console.Write(Op.Board.ToString());
            return unit;
        }
    }
}
