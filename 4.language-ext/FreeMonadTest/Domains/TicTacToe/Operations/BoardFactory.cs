using FreeMonadTest.Domains.TicTacToe.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;

namespace FreeMonadTest.Domains.TicTacToe.Operations
{
    public static class BoardFactory
    {
        public static Board CreateBoard(Dimension size)
        {
            return new Board(size);
        }
    }
}
