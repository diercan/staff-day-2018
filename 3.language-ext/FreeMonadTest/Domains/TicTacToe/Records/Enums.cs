using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMonadTest.Domains.TicTacToe.Records
{
    public partial class Board
    {
        public enum GameStatus
        {
            InProgress,
            Over
        }

        public enum Shapes
        {
            X,
            O
        }
    }
}
