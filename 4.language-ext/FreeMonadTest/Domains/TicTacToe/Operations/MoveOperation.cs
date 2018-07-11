using FreeMonadTest.Domains.TicTacToe.Records;
using LanguageExt;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using static LanguageExt.Prelude;

namespace FreeMonadTest.Domains.TicTacToe.Operations
{
    public static class MoveOperation
    {
        public static Validation<string, Board> Move(this Board board, Validation<string, Point> point) =>
             from p in point
             from validatedBoard in CheckValidGameStatus(board)
             from validatedeCell in CheckCellStatus(board.GetCell(p))
             select UpdateBoard(validatedBoard, p);


        private static Lst<Option<Shapes>> GetNewRow(Board b, Point p) =>
            b.Cells[p.X.Value - 1]
            .Map((idx, cell) => idx == p.Y.Value - 1 ? Some(b.NextMoveShape) : cell)
            .Freeze();

        private static Lst<Lst<Option<Shapes>>> GetNewCells(Lst<Option<Shapes>> newRow, Board b, Point p) =>
            b.Cells
            .Map((idx, row) => idx == p.X.Value - 1 ? newRow : row)
            .Freeze();

        private static Board UpdateBoard(Board b, Point p)
            => new Board(GetNewCells(GetNewRow(b, p), b, p));

        private static Validation<string, Board> CheckValidGameStatus(Board b) =>
             b.Status == Board.GameStatus.InProgress ?
                Success<string, Board>(b) :
                Fail<string, Board>("Move is not possible when game is over");

        private static Validation<string, Option<Shapes>> CheckCellStatus(Option<Shapes> cell) =>
            cell.IsNone ?
                Success<string, Option<Shapes>>(cell) :
                Fail<string, Option<Shapes>>("Move is not possible when cell is taken");
    }
}
