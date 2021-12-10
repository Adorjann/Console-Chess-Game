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
        private MovementAnalysis movesCalc;

        //movementHistory
        private Stack<string> movesHistory = new Stack<string>();

        public string Color { get => color; set => color = value; }
        public string Name { get => name; set => name = value; }
        internal Square CurrentPlacement { get => currentPlacement; set => currentPlacement = value; }
        public Stack<string> MovesHistory { get => movesHistory; set => movesHistory = value; }
        internal ChessBoard ChessBoard { get => chessBoard; set => chessBoard = value; }
        public MovementAnalysis MovesCalc { get => movesCalc;  }

        public Piece(string color, string name,  Square currentPlacement)
        {
            this.Color = color;
            this.Name = name;
            this.CurrentPlacement = currentPlacement;

            this.movesCalc = new MovementAnalysis(this);
        }

       
        

        public override string ToString()
        {
            return $"piece name: | {this.Color} {this.Name} |";
        }
    }
}
