using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LanguageExt.Prelude;

namespace FreeMonadTest.Domains.TicTacToe.Records
{
    public partial class Board : Record<Board>
    {
        public readonly Lst<Lst<Option<Shapes>>> Cells;
        public readonly Shapes NextMoveShape;
        public GameStatus Status;
        public Option<Shapes> Winner;

        public Dimension Size =>
            new Dimension(Cells.Count);

        public Option<string> WinnerShape =>
            Winner.Map(s => (s == Shapes.X ? "X" : "O"));

        internal Board(Dimension n)
        {
            Cells = CreateEmptyBoard(n);
            NextMoveShape = Shapes.X;
            Status = GameStatus.InProgress;
            Winner = Option<Shapes>.None;
        }

        private Option<Shapes> GetWinner()
            => IsXWinner() ? Some(Shapes.X) : IsOWinner() ? Some(Shapes.O) : None;

        public Board(Lst<Lst<Option<Shapes>>> cells)
        {
            if (!cells.ForAll(row => row.Length() == cells.Length()) || cells.Length() > Dimension.MaxBoardDimension) throw new ArgumentOutOfRangeException("Invalid board");

            Cells = cells;
            NextMoveShape = CalculateNextMoveShape();
            Status = CalculateGameStatus();

            GetWinner().Iter(w =>
            {
                Status = GameStatus.Over;
                Winner = Some(w);
            });
        }

        public Validation<string, Point> CreateBoardPoint(Coordinate x, Coordinate y) =>
            (x.Value <= Size.Value && y.Value <= Size.Value) ?
                Success<string, Point>(new Point(x, y)) :
                Fail<string, Point>("Point is outside board boundaries.");

        public Option<Shapes> GetCell(Point P) =>
             Cells[P.X.Value - 1][P.Y.Value - 1];

        #region moves logic

        private int XMoveCount =>
            Cells.SelectMany(row => row).Count(cell => cell == Shapes.X);

        private int OMoveCount =>
            Cells.SelectMany(row => row).Count(cell => cell == Shapes.O);

        private Shapes CalculateNextMoveShape() =>
             XMoveCount <= OMoveCount ? Shapes.X : Shapes.O;

        private GameStatus CalculateGameStatus() =>
            Cells.SelectMany(row => row).Any(cell => cell.IsNone) ? GameStatus.InProgress : GameStatus.Over;

        #endregion moves logic

        #region winner detection
        private bool IsXWinner() =>
            CheckLineWiner(Shapes.X) ||
            CheckColumnWinner(Shapes.X) ||
            CheckDiagonalWinner(Shapes.X);

        private bool IsOWinner() =>
            CheckLineWiner(Shapes.O) ||
            CheckColumnWinner(Shapes.O) ||
            CheckDiagonalWinner(Shapes.O);

        private IEnumerable<Option<Shapes>> FirstDiagonal =>
            Range(1, Size.Value).Select(idx => Cells[idx - 1][idx - 1]);

        private IEnumerable<Option<Shapes>> SecondDiagonal =>
            Range(1, Size.Value).Select(idx => Cells[idx - 1][Size.Value - idx]);

        private bool CheckDiagonalWinner(Shapes shape) =>
            FirstDiagonal.ForAll(cell => cell == shape) ||
            SecondDiagonal.ForAll(cell => cell == shape);

        private IEnumerable<Lst<Option<Shapes>>> GetColumns() =>
            Range(1, Size.Value).Select(colNum => Cells.Select(row => row[colNum - 1]));

        private bool CheckColumnWinner(Shapes shape) =>
            GetColumns().Any(column => column.All(c => c == shape));

        private bool CheckLineWiner(Shapes shape) =>
            Cells.Any(row => row.ForAll(c => c == shape));

        #endregion winner detection

        #region board generation
        private Lst<Lst<Option<Shapes>>> CreateEmptyBoard(Dimension n) =>
             Lst<Lst<Option<Shapes>>>
                .Empty
                .AddRange(Range(1, n.Value).Select(r => CreateEmptyLine(n)));

        private static Lst<Option<Shapes>> CreateEmptyLine(Dimension n) =>
             Lst<Option<Shapes>>
                .Empty
                .AddRange(Range(1, n.Value).Select(c => Option<Shapes>.None));
        #endregion board generation

        #region toString
        public override string ToString()
        {
            var builder = Cells.Fold(
                    new StringBuilder().AppendLine("------------------"),
                    (s, x) => s.AppendLine(FoldRow(x))
                ).AppendLine("------------------");

            if (Status == GameStatus.Over)
            {
                WinnerShape.Match(
                        Some: s => builder.AppendLine($"GAME OVER! {s} wins!"),
                        None: () => builder.AppendLine("GAME OVER! Nobody wins!")
                    );
            }

            return builder.ToString();
        }

        private string FoldRow(Lst<Option<Shapes>> row) =>
            row.Fold(new StringBuilder().Append("|"),
                    (s, x) => s.Append(x.Match(
                            Some: shape => $"{shape.ToString()}|",
                            None: () => " |"))).ToString();
        #endregion toString
    }
}
