using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public  class Piece
    {
        private string color;
        private string name;
        private ChessBoard chessBoard;
        private Square currentPlacement;
        List<Square> possibleMoves = new List<Square>();
        List<Square> possibleThreats = new List<Square> ();
        List<Piece> currentThreats = new List<Piece>();

        //movementHistory
        private Stack<string> movesHistory = new Stack<string>();

        public string Color { get => color; set => color = value; }
        public string Name { get => name; set => name = value; }
        internal Square CurrentPlacement { get => currentPlacement; set => currentPlacement = value; }
        internal List<Square> PossibleMoves { get => possibleMoves; set => possibleMoves = value; }
        internal List<Square> PossibleThreats { get => possibleThreats; set => possibleThreats = value; }
        internal List<Piece> CurrentThreats { get => currentThreats; set => currentThreats = value; }
        public Stack<string> MovesHistory { get => movesHistory; set => movesHistory = value; }
        internal ChessBoard ChessBoard { get => chessBoard; set => chessBoard = value; }

        public Piece(string color, string name,  Square currentPlacement)
        {
            this.Color = color;
            this.Name = name;
            this.CurrentPlacement = currentPlacement;
        }

       
        public void CalcPossibleMoves()
        {
           //finding Squares a Piece can move to
            this.possibleMoves = Movements.Moving(this);
             

        }
        


        public void CalcCurrentThreats()
        {
            // checking if this.CurrentPlacement is some enemy Piece's possible move
            this.CurrentThreats = Movements.Threats(this);


        }

        public override string ToString()
        {
            return $"piece name: | {this.Color} {this.Name} |";
        }
    }
}
