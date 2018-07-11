using FreeMonadTest.Domains.TicTacToe.Records;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using static LanguageExt.Prelude;

namespace Tests.TicTacToe
{
    public static class TestBoards
    {
        public static class Empty
        {
            public static readonly Board Board = new Board(List(
                        List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None)
                        ));
            public const string Preview =
@"------------------
| | | |
| | | |
| | | |
------------------
";
        }

        public static class X_UpperCorner
        {
            public static readonly Board Board = new Board(List(
                    List(Some(Shapes.X), Option<Shapes>.None, Option<Shapes>.None),
                    List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None),
                    List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None)
                    ));

            public const string Preview =
@"------------------
|X| | |
| | | |
| | | |
------------------
";
        }

        public static class O_UpperCorner
        {
            public static readonly Board Board = new Board(List(
                    List(Some(Shapes.O), Option<Shapes>.None, Option<Shapes>.None),
                    List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None),
                    List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None)
                    ));

            public const string Preview =
@"------------------
|O| | |
| | | |
| | | |
------------------
";
        }

        public static class XO_UpperCorner {
            public static readonly Board Board = new Board(List(
                        List(Some(Shapes.X), Some(Shapes.O), Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Option<Shapes>.None)
                        ));
            public const string Preview = 
@"------------------
|X|O| |
| | | |
| | | |
------------------
";
        }      
        
        public static class X_Wins_1st_Line
        {
            public static readonly Board Board = new Board(List(
                        List(Some(Shapes.X), Some(Shapes.X), Some(Shapes.X)),
                        List(Option<Shapes>.None, Some(Shapes.O), Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Some(Shapes.O))
                        ));
            public const string Preview =
@"------------------
|X|X|X|
| |O| |
| | |O|
------------------
GAME OVER! X wins!
";
        }

        public static class O_Wins_1st_Column
        {
            public static readonly Board Board = new Board(List(
                        List(Some(Shapes.O), Some(Shapes.X), Some(Shapes.X)),
                        List(Some(Shapes.O), Some(Shapes.X), Option<Shapes>.None),
                        List(Some(Shapes.O), Option<Shapes>.None, Option<Shapes>.None)
                        ));
            public const string Preview =
@"------------------
|O|X|X|
|O|X| |
|O| | |
------------------
GAME OVER! O wins!
";
        }

        public static class O_Wins_1st_Diagonal
        {
            public static readonly Board Board = new Board(List(
                        List(Some(Shapes.O), Some(Shapes.X), Some(Shapes.X)),
                        List(Some(Shapes.X), Some(Shapes.O), Option<Shapes>.None),
                        List(Option<Shapes>.None, Option<Shapes>.None, Some(Shapes.O))
                        ));
            public const string Preview =
@"------------------
|O|X|X|
|X|O| |
| | |O|
------------------
GAME OVER! O wins!
";
        }

        public static class Draw
        {
            public static readonly Board Board = new Board(List(
                        List(Some(Shapes.O), Some(Shapes.X), Some(Shapes.X)),
                        List(Some(Shapes.X), Some(Shapes.X), Some(Shapes.O)),
                        List(Some(Shapes.O), Some(Shapes.O), Some(Shapes.X))
                        ));
            public const string Preview =
@"------------------
|O|X|X|
|X|X|O|
|O|O|X|
------------------
GAME OVER! Nobody wins!
";
        }
    }
}
