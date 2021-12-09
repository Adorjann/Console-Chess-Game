using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class Player
    {
        private string name;
        private List<Piece> alivePieces = new List<Piece>();
        private List<Piece> deadPieces = new List<Piece>();
        private Game game;

        public Player(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        internal List<Piece> AlivePieces { get => alivePieces; set => alivePieces = value; }
        internal List<Piece> DeadPieces { get => deadPieces; set => deadPieces = value; }
        internal Game Game { get => game; set => game = value; }

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

            

            if (!piece.PossibleMoves.Contains(newSquarePosition))
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

            return true;
        }

        private void takeThePiece(Piece pieceToTake)
        {
            pieceToTake.CurrentPlacement = null;
            pieceToTake.PossibleMoves.Clear();
            pieceToTake.PossibleThreats.Clear();
            pieceToTake.CurrentThreats.Clear();

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
