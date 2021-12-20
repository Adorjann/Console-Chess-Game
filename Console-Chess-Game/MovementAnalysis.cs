using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class MovementAnalysis
    {
        private Piece piece;

        //avaiable moves
        private List<Square> possibleMoves = new List<Square>();
        //list of enemy pieces whose possible move is this piece's current position
        private List<Piece> currentThreats = new List<Piece>();
        //list of possible moves that are some enemy piece's possible move 
        private List<Square> possibleThreats = new List<Square>();
        //list of safe possible move
        private List<Square> safePossibleMove = new List<Square>();


        public Piece Piece { get => piece; set => piece = value; }
        public List<Square> PossibleMoves { get => possibleMoves;  }
        public List<Square> PossibleThreats { get => possibleThreats; }
        public List<Piece> CurrentThreats { get => currentThreats;  }
        public List<Square> SafePossibleMove { get => safePossibleMove;  }

        //constructor
        public MovementAnalysis(Piece piece) { 
            this.piece = piece;
        }


        public void CalcPossibleMoves()
        {
            //finding Squares a Piece can move to
            this.possibleMoves = Movements.Moving(this.piece);


        }



        public void CalcCurrentThreats()
        {
            // checking if this.CurrentPlacement is some enemy Piece's possible move
            this.currentThreats = Movements.Threats(this.piece);


        }
        public void calcPossibleThreats()
        {
            //checking if this.Piece's possible move is some enemy piece's possible move
            //therefore it's a possible Threat

            this.possibleThreats = Movements.PossibleThreats(this.piece);
        }

        public void calcSafePossibleMove()
        {
            CalcPossibleMoves();
            calcPossibleThreats();

            this.possibleMoves.ForEach(pMove => {

                if (!this.possibleThreats.Contains(pMove))
                {
                    this.safePossibleMove.Add(pMove);
                } 
            });
        }




    }







}
