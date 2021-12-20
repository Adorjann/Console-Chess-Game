using System;

namespace Console_Chess_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            //create the chessboard

            ChessBoard theBoard = new ChessBoard();
            theBoard.positionAllThePieces();
            //testingData(theBoard);

            Player player1 = new Player("white");
            Player player2 = new Player("black");

            Game game = new Game(player1, player2, theBoard);
            player1.Game = game;
            player2.Game = game;

            game.AssignPlayersTheirPieces();


            game.StartTheGame();
        }


        public static void testingData(ChessBoard theBoard)
        {
            //TESTING PAWN's possibleMoves() 
            //theBoard.alivePieces.ForEach(piece =>
            //{
            //    if (piece.Name == "P")
            //    {
            //        piece.CalcPossibleMoves();
            //        piece.PossibleMoves.ForEach(move =>
            //        {
            //            Console.WriteLine(move.Name);
            //        });
            //    }
            //});

            //theBoard.alivePieces.RemoveAll(piece => piece.Name == "P" ); //Removing Pawns to see other piece possible moves

            //theBoard.allSquares.ForEach(square => { 

            //    if (square.PiecePlaced != null && square.PiecePlaced.Name == "P")
            //    {
            //        square.PiecePlaced = null;
            //    }
            //});

            Square d3 = theBoard.allSquares.Find(square => square.Name == "d8");
            Piece bishop1 = new Piece("white", "N", d3);
            d3.PiecePlaced = bishop1;
            bishop1.ChessBoard = theBoard;
            theBoard.alivePieces.Add(bishop1);

            Square d7 = theBoard.allSquares.Find(square => square.Name == "d4");
            Piece rock4 = new Piece("black", "N", d7);
            d7.PiecePlaced = rock4;
            rock4.ChessBoard = theBoard;
            theBoard.alivePieces.Add(rock4);
        }
    }
}
