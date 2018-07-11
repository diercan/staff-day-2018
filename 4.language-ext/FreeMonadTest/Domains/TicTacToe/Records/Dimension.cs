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
        public class Dimension : NumType<Dimension, TInt, int, Dimension.ValidBoardDimension>
        {
            public const int MaxBoardDimension = 9;

            public Dimension(int value) : base(value)
            {
            }

            public struct ValidBoardDimension : Pred<int>
            {
                public bool True(int value)
                {
                    return value > 0 && value <= MaxBoardDimension;
                }
            }
        }
    }
}
