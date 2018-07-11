using FreeMonadTest.Domains.TicTacToe.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMonadTest.Domains.TicTacToe.Operations
{
    public class ReadPoint
    {
        public readonly Board Board;

        public ReadPoint(Board board)
        {
            Board = board;
        }
    }
}
