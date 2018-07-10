using FreeMonadTest.Domains.TicTacToe.Operations;
using FreeMonadTest.Interpreters;
using LanguageExt;
using System;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using static LanguageExt.Prelude;

namespace FreeMonadTest.Domains.TicTacToe.Interpreters
{
    public class ReadPointInterpreter : OpInterpreter<ReadPoint, Validation<string, Point>>
    {
        public override Validation<string, Point> Mock(ReadPoint Op) =>
             Op.Board.CreateBoardPoint(new Coordinate(1), new Coordinate(1));

        public override Validation<string, Point> Work(ReadPoint Op) =>
             from x in ReadValidCoorindate("X", Op).ToValidation("Could not read X point")
             from y in ReadValidCoorindate("Y", Op).ToValidation("Could not read Y point")
             from point in Op.Board.CreateBoardPoint(x, y)
             select point;

        private Option<Coordinate> ReadValidCoorindate(string label, ReadPoint op)
        {
            var x = Option<Coordinate>.None;
            int tries = 0;
            do
            {
                Console.Write($"{label}:");
                x = parseInt(Console.ReadLine())
                       .Where(xVal => xVal >= 1 && xVal <= op.Board.Size.Value)
                       .Map(xVal => new Coordinate(xVal));

            } while (x.IsNone && ++tries < 4);
            return x;
        }
    }
}
