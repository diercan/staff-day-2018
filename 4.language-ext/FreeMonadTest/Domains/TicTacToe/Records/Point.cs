using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;

namespace FreeMonadTest.Domains.TicTacToe.Records
{
    public partial class Board
    {
        public class Point : Record<Point>
        {
            public readonly Coordinate X;
            public readonly Coordinate Y;

            internal Point(Coordinate x, Coordinate y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
