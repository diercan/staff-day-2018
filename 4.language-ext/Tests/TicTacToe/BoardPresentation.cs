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
    public class BoardPresentation
    {
        public BoardPresentation()
        {
        }


        [TestMethod]
        public void EmptyBoard()
        {
            var board = TestBoards.Empty.Board;
            var expected = TestBoards.Empty.Preview;
            Assert.AreEqual(expected, board.ToString());
        }

        [TestMethod]
        public void OneXAndOneOBoard()
        {
            var board = TestBoards.XO_UpperCorner.Board;
            var expected = TestBoards.XO_UpperCorner.Preview;
            Assert.AreEqual(expected, board.ToString());
        }

        [TestMethod]
        public void GameIsOverAndXWins()
        {
            var board = TestBoards.X_Wins_1st_Line.Board;
            var expected = TestBoards.X_Wins_1st_Line.Preview;
            Assert.AreEqual(expected, board.ToString());
        }

        [TestMethod]
        public void GameIsOverAndOWins()
        {
            var board = TestBoards.O_Wins_1st_Column.Board;
            var expected = TestBoards.O_Wins_1st_Column.Preview;
            Assert.AreEqual(expected, board.ToString());
        }

        [TestMethod]
        public void GameIsOverNoWinner()
        {
            var board = TestBoards.Draw.Board;
            var expected = TestBoards.Draw.Preview;
            Assert.AreEqual(expected, board.ToString());
        }
    }
}
