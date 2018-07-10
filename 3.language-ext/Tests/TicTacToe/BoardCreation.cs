using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreeMonadTest.Domains.TicTacToe;
using FreeMonadTest.Infrastucture;
using static LanguageExt.Prelude;
using LanguageExt;
using FreeMonadTest.Domains.TicTacToe.Records;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;
using Tests.TicTacToe;

namespace Tests
{
    /// <summary>
    /// Tests for the TicTac Toe logic
    /// </summary>
    [TestClass]
    public class BoardCreation
    {
        [TestMethod]
        public void NineXNineBoardGeneration()
        {
            var board = TicTacToeGame.GenerateBoard(new Dimension(9));

            Assert.AreEqual(9, board.Cells.Count, "Incorrect board dimension");
            Assert.IsTrue((from row in board.Cells
                           select row.Count == 9)
                          .ForAll(isCorrect => isCorrect), "Incorrect row dimension");
            var allCellEmpty = from row in board.Cells
                               from cel in row
                               select cel.IsNone;
            Assert.IsTrue(allCellEmpty.ForAll(isNone => isNone), "Board is not empty");
            Assert.AreEqual(GameStatus.InProgress, board.Status, "For empty board game should be in progress");
            Assert.IsTrue(board.Winner.IsNone, "For empty board there should be no winner");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidBoardDimension()
        {
            var board = TicTacToeGame.GenerateBoard(new Dimension(11));
        }

        [TestMethod]
        public void EmptyBoardXMovesNext()
        {
            var board = TicTacToeGame.GenerateBoard(new Dimension(3));
            Assert.AreEqual(Shapes.X, board.NextMoveShape, "X should move next");
        }

        [TestMethod]
        public void EqualXandOMovesThenXMovesNext()
        {
            var board = TestBoards.XO_UpperCorner.Board;
            Assert.AreEqual(Shapes.X, board.NextMoveShape, "X should move next");
        }

        [TestMethod]
        public void XMovesMoreThanOMovesThenOMovesNext()
        {
            var board = TestBoards.X_UpperCorner.Board;
            Assert.AreEqual(Shapes.O, board.NextMoveShape, "O should move next");
        }

        [TestMethod]
        public void OMovesMoreThanXMovesThenXMovesNext()
        {
            var board = TestBoards.O_UpperCorner.Board;
            Assert.AreEqual(Shapes.X, board.NextMoveShape, "X should move next");
        }

        [TestMethod]
        public void GameIsInprogress()
        {
            var board = TestBoards.O_UpperCorner.Board;
            Assert.AreEqual(GameStatus.InProgress, board.Status, "Game should be in progress there are moves left");
        }

        [TestMethod]
        public void GameIsOverAndXWinsRow()
        {
            var board = TestBoards.X_Wins_1st_Line.Board;
            Assert.AreEqual(GameStatus.Over, board.Status, "Game should be over");
            Assert.AreEqual("X", board.WinnerShape, "Winner is X");
        }

        [TestMethod]
        public void GameIsOverAndOWinsColumn()
        {
            var board = TestBoards.O_Wins_1st_Column.Board;
            Assert.AreEqual(GameStatus.Over, board.Status, "Game should be over");
            Assert.AreEqual("O", board.WinnerShape, "Winner is O");
        }

        [TestMethod]
        public void GameIsOverAndOWinsDiagonal()
        {
            var board = TestBoards.O_Wins_1st_Diagonal.Board;
            Assert.AreEqual(GameStatus.Over, board.Status, "Game should be over");
            Assert.AreEqual("O", board.WinnerShape, "Winner is O");
        }

        [TestMethod]
        public void GameIsOverNoWinner()
        {
            var board = TestBoards.Draw.Board;
            Assert.AreEqual(GameStatus.Over, board.Status, "Game should be over");
            Assert.AreEqual(true, board.WinnerShape.IsNone, "No winner");
        }
    }
}
