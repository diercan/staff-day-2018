using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using static LanguageExt.List;
using System.Collections.Generic;
using FreeMonadTest.Interpreters;
using FreeMonadTest;
using FreeMonadTest.Domains;
using FreeMonadTest.Domains.TicTacToe;
using static FreeMonadTest.Domains.TicTacToe.Records.Board;

namespace ConsoleApp8
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var board = TicTacToeGame.GenerateBoard(new Dimension(3));
            do
            {
                var program = from _ in TicTacToeGame.ShowBoard(board)
                              from p in TicTacToeGame.ReadPoint(board)
                              select TicTacToeGame.Move(board, p);
                var newBoard = LiveInterpreter.Interpret(program);
                board = newBoard.Match(Succ: b => b, Fail: msg => board);
                newBoard.IfFail(msg => Console.WriteLine(msg.First()));
            } while (board.Status == GameStatus.InProgress);

            var end = from _ in TicTacToeGame.ShowBoard(board)
                      select unit;
            LiveInterpreter.Interpret(end);

            Console.ReadLine();
        }
    }

}