using FreeMonadTest.Domains.TicTacToe;
using FreeMonadTest.Infrastucture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using static LanguageExt.Prelude;

namespace Tests.TicTacToe
{
    [TestClass]
    public class BoardMoves
    {

        [TestMethod]
        public void X_CanMoveWhenCellIsFree()
        {
            var board = TicTacToeGame.GenerateBoard(new Dimension(3));
            var p = board.CreateBoardPoint(new Coordinate(1), new Coordinate(1));
            var result = TicTacToeGame.Move(board, p);
            Assert.IsTrue(result.IsSuccess, "Move should have been possible");
            var expectedBoardStatus = TestBoards.X_UpperCorner.Preview;
            var actualBoardStatus = result.Match(Succ: b => b.ToString(), Fail: m => m.First());
            Assert.AreEqual(expectedBoardStatus, actualBoardStatus, "[1, 1] should be the only cell marked with X");
        }

        [TestMethod]
        public void O_CanMoveWhenCellIsFree()
        {
            var board = TestBoards.X_UpperCorner.Board;
            var p = board.CreateBoardPoint(new Coordinate(1), new Coordinate(2));
            var result = TicTacToeGame.Move(board, p);
            Assert.IsTrue(result.IsSuccess, "Move should have been possible");
            var expectedBoardStatus = TestBoards.XO_UpperCorner.Preview;
            var actualBoardStatus = result.Match(Succ: b => b.ToString(), Fail: m => m.First());
            Assert.AreEqual(expectedBoardStatus, actualBoardStatus, "[1, 2] should be marked with O");
        }

        [TestMethod]
        public void O_CanNOTMoveWhenCellIsTeken()
        {
            var board = TestBoards.X_UpperCorner.Board;
            var p = board.CreateBoardPoint(new Coordinate(1), new Coordinate(1));
            var result = TicTacToeGame.Move(board, p);
            Assert.IsTrue(result.IsFail, "Move should not been possible");
            Assert.AreEqual("Move is not possible when cell is taken", result.Match(Succ: l => "", Fail: r => r.First()));
        }

        [TestMethod]
        public void O_CanNOTMoveWhenBoardIsFull()
        {
            var board = TestBoards.Draw.Board;
            var p = board.CreateBoardPoint(new Coordinate(1), new Coordinate(1));
            var result = TicTacToeGame.Move(board, p);
            Assert.IsTrue(result.IsFail, "Move should not been possible");
            Assert.AreEqual("Move is not possible when game is over", result.Match(Succ: l => "", Fail: r=>r.First()));
        }

        [TestMethod]
        public void O_CanNOTMoveWhenGameIsOver()
        {
            var board = TestBoards.X_Wins_1st_Line.Board;
            var p = board.CreateBoardPoint(new Coordinate(2), new Coordinate(1));
            var result = TicTacToeGame.Move(board, p);
            Assert.IsTrue(result.IsFail, "Move should not been possible");
            Assert.AreEqual("Move is not possible when game is over", result.Match(Succ: l => "", Fail: r => r.First()));
        }
    }
}
