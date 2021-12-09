using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class Square
    {

        private string name;
        private Piece piecePlaced;

        public Square(string name, Piece piecePlaced)
        {
            this.name = name;
            this.piecePlaced = piecePlaced;
        }
        public Square (string name)
        {
            this.name = name;
        }

        public string Name { get => name; }
        internal Piece PiecePlaced { get => piecePlaced; set => piecePlaced = value; }

        public bool DoYouHavePiece()
        {
           if (piecePlaced != null)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"Square name: {this.name}";
        }
    }
}
