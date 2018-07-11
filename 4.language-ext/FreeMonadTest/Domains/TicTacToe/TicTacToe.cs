using LanguageExt;
using static IOExt;
using FreeMonadTest.Domains.TicTacToe.Operations;
using FreeMonadTest.Domains.TicTacToe.Records;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using static FreeMonadTest.Domains.TicTacToe.Operations.BoardFactory;

namespace FreeMonadTest.Domains.TicTacToe
{
    public static class TicTacToeGame
    {
        public static Board GenerateBoard(Dimension dimension)
            => CreateBoard(dimension);
        public static Validation<string, Board> Move(Board board, Validation<string, Point> point)
            => board.Move(point);

        #region IO
        public static IO<Unit> ShowBoard(Board board) 
            => NewIO(new ShowBoard(board));
        public static IO<Validation<string, Point>> ReadPoint(Board board) 
            => NewIO<ReadPoint, Validation<string, Point>>(new ReadPoint(board));
        #endregion
    }
}
