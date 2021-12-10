using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class Player
    {
        private string name;
        private List<Piece> alivePieces = new List<Piece>();
        private List<Piece> deadPieces = new List<Piece>();
        private Game game;

        private string checkPosition;

        public Player(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        internal List<Piece> AlivePieces { get => alivePieces; set => alivePieces = value; }
        internal List<Piece> DeadPieces { get => deadPieces; set => deadPieces = value; }
        internal Game Game { get => game; set => game = value; }
        public string CheckPosition { get => checkPosition; set => checkPosition = value; }

        public override string ToString()
        {
            return $"{this.name} player";
        }

        public void playerHasPieceInGame()
        {
            Console.WriteLine($"{this.name} player has these alive pieces: ");
            this.alivePieces.ForEach(piece => Console.WriteLine($"Piece {piece.Name}, on square: {piece.CurrentPlacement.Name}"));
        }

        internal bool MakeAMove(string move)
        {
            //move looks like this:   a2>a4

            string[] squares = move.Split('>');
            
            Piece piece = this.alivePieces.Find(piece => piece.CurrentPlacement.Name == squares[0] ); //find the piece to be moved
            if(piece == null)
            {
                Console.WriteLine($"*** There is no piece in square {squares[0]} ***");
                return false;
            }
            Square newSquarePosition = piece.ChessBoard.allSquares.Find(sq => sq.Name == squares[1] ); //find the new square position to move

            

            if (!piece.MovesCalc.PossibleMoves.Contains(newSquarePosition))
            {
                Console.WriteLine("*** Move is against the rules ***");
                return false;
            }

            //if a piece is about to be taken
            if(newSquarePosition.PiecePlaced != null)
            {   // Taking the Piece
                takeThePiece(newSquarePosition.PiecePlaced);
            }

            if(piece != null)
            {
                piece.MovesHistory.Push(piece.CurrentPlacement.Name);

                // leave empty square
                piece.CurrentPlacement.PiecePlaced = null;
                //move to the new Square
                piece.CurrentPlacement = newSquarePosition; 
                newSquarePosition.PiecePlaced = piece;

            }
            specialMoves(piece, move);

            return true;
        }

        private void specialMoves(Piece piece,string move)
        {
            List<Square> allSquares = piece.ChessBoard.allSquares;

            //Promotion
            if ((piece.Name == "P") && (piece.CurrentPlacement.Name.EndsWith('8') || piece.CurrentPlacement.Name.EndsWith('1')))
            {
                Thread.Sleep(1000);
                piece.Name = "Q";
            }

            if(piece.Name == "K" &&
                move == "e1>c1")
            {
                Square a1 = allSquares.Find(sq => sq.Name == "a1");
                Square d1 = allSquares.Find(sq => sq.Name == "d1");
                Piece rock = a1.PiecePlaced;

                a1.PiecePlaced = null;
                d1.PiecePlaced = rock;
                rock.CurrentPlacement = d1;
            }
            if (piece.Name == "K" &&
                move == "e1>g1")
            {
                Square h1 = allSquares.Find(sq => sq.Name == "h1");
                Square f1 = allSquares.Find(sq => sq.Name == "f1");
                Piece rock = h1.PiecePlaced;

                h1.PiecePlaced = null;
                f1.PiecePlaced = rock;
                rock.CurrentPlacement = f1;
            }
            if (piece.Name == "K" &&
                move == "e8>g8")
            {
                Square h8 = allSquares.Find(sq => sq.Name == "h8");
                Square f8 = allSquares.Find(sq => sq.Name == "f8");
                Piece rock = h8.PiecePlaced;

                h8.PiecePlaced = null;
                f8.PiecePlaced = rock;
                rock.CurrentPlacement = f8;
            }
            if (piece.Name == "K" &&
                move == "e8>c8")
            {
                Square a8 = allSquares.Find(sq => sq.Name == "a8");
                Square d8 = allSquares.Find(sq => sq.Name == "d8");
                Piece rock = a8.PiecePlaced;

                a8.PiecePlaced = null;
                d8.PiecePlaced = rock;
                rock.CurrentPlacement = d8;
            }



        }

        private void takeThePiece(Piece pieceToTake)
        {
            pieceToTake.CurrentPlacement = null;
            pieceToTake.MovesCalc.PossibleMoves.Clear();
            pieceToTake.MovesCalc.PossibleThreats.Clear();
            pieceToTake.MovesCalc.CurrentThreats.Clear();

            if (this.game.Player1.Equals(this))
            {
                this.game.Player2.alivePieces.Remove(pieceToTake);
                this.game.Player2.deadPieces.Add(pieceToTake);
            }
            else
            {
                this.game.Player1.alivePieces.Remove(pieceToTake);
                this.game.Player1.deadPieces.Add(pieceToTake);
            }

            this.game.ChessBoard.alivePieces.Remove(pieceToTake);
            this.game.ChessBoard.deadPieces.Add(pieceToTake);
        }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   name == player.name;
        }
    }
}
