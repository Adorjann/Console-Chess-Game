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

            Square square1 = theBoard.allSquares.Find(square => square.Name == "b1");
            Piece piece1 = new Piece("white", "K", square1);
            square1.PiecePlaced = piece1;
            piece1.ChessBoard = theBoard;
            theBoard.alivePieces.Add(piece1);

            Square square2 = theBoard.allSquares.Find(square => square.Name == "g3");
            Piece piece2 = new Piece("black", "Q", square2);
            square2.PiecePlaced = piece2;
            piece2.ChessBoard = theBoard;
            theBoard.alivePieces.Add(piece2);

            Square square3 = theBoard.allSquares.Find(square => square.Name == "f7");
            Piece piece3 = new Piece("white", "P", square3);
            square3.PiecePlaced = piece3;
            piece3.ChessBoard = theBoard;
            theBoard.alivePieces.Add(piece3);

            Square square4 = theBoard.allSquares.Find(square => square.Name == "d2");
            Piece piece4 = new Piece("black", "P", square4);
            square4.PiecePlaced = piece4;
            piece4.ChessBoard = theBoard;
            theBoard.alivePieces.Add(piece4);


            Console.WriteLine("Expecting to write 3.0 on the next line: ");
            Console.WriteLine();
            Console.WriteLine(EvaluateBoard.CurrentPositionValue(piece2));
        }
    }
}
