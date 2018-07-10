using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.TypeClasses;

namespace FreeMonadTest.Domains.TicTacToe.Records
{
    public partial class Board
    {
        public class Coordinate : NumType<Coordinate, TInt, int, Dimension.ValidBoardDimension>
        {
            public Coordinate(int value) : base(value)
            {
            }
        }
    }
}
