using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class ChessBoard
    {

        public List<Square> allSquares = new List<Square>();
        public List<Piece> alivePieces = new List<Piece>();
        public List<Piece> deadPieces = new List<Piece>();


        public ChessBoard()
        {
            //creating a chessboard
            for(int i = 97; i<105; i++)
            {
                for(int j = 1; j < 9; j++)
                {
                    Square square = new Square($"{Convert.ToChar(i)}{j}");
                    this.allSquares.Add(square);
                }
            }
        }

        public void positionAllThePieces()
        {
            //white pawns positioning on the line number 2
            List<Square> line2 = allSquares.FindAll(square => square.Name.EndsWith("2"));
            line2.ForEach(square =>
            {
                Piece pawn = new Piece("white", "P",  square);
                pawn.ChessBoard = this;
                square.PiecePlaced = pawn;

                alivePieces.Add(pawn);
            });

            //white Rocks
            Square a1 = allSquares.Find(square => square.Name == "a1");
            Piece rock1 = new Piece("white", "R",  a1);
            a1.PiecePlaced = rock1;
            rock1.ChessBoard = this;
            alivePieces.Add(rock1);

            Square h1 = allSquares.Find(square => square.Name == "h1");
            Piece rock2 = new Piece("white", "R",  h1);
            h1.PiecePlaced = rock2;
            rock2.ChessBoard = this;
            alivePieces.Add(rock2);

            //white Knight
            Square b1 = allSquares.Find(square => square.Name == "b1");
            Piece knight1 = new Piece("white", "N", b1);
            b1.PiecePlaced = knight1;
            knight1.ChessBoard = this;
            alivePieces.Add(knight1);

            Square g1 = allSquares.Find(square => square.Name == "g1");
            Piece knight2 = new Piece("white", "N",  g1);
            g1.PiecePlaced = knight2;
            knight2.ChessBoard = this;
            alivePieces.Add(knight2);

            //white Bishop
            Square c1 = allSquares.Find(square => square.Name == "c1");
            Piece bishop1 = new Piece("white", "B",  c1);
            c1.PiecePlaced = bishop1;
            bishop1.ChessBoard = this;
            alivePieces.Add(bishop1);

            Square f1 = allSquares.Find(square => square.Name == "f1");
            Piece bishop2 = new Piece("white", "B",  f1);
            f1.PiecePlaced = bishop2;
            bishop2.ChessBoard = this;
            alivePieces.Add(bishop2);

            //white Queen
            Square d1 = allSquares.Find(square => square.Name == "d1");
            Piece queen = new Piece("white", "Q",  d1);
            d1.PiecePlaced = queen;
            queen.ChessBoard = this;
            alivePieces.Add(queen);

            //White King
            Square e1 = allSquares.Find(square => square.Name == "e1");
            Piece king = new Piece("white", "K",  e1);
            e1.PiecePlaced = king;
            king.ChessBoard = this;
            alivePieces.Add(king);


            //black pawns positioning on the line number 7
            List<Square> line7 = allSquares.FindAll(square => square.Name.EndsWith("7"));
            line7.ForEach(square =>
            {
                Piece pawn = new Piece("black", "P", square);
                pawn.ChessBoard = this;
                square.PiecePlaced = pawn;
                alivePieces.Add(pawn);
            });

            //Black Rocks
            Square a8 = allSquares.Find(square => square.Name == "a8");
            Piece rock3 = new Piece("black", "R", a8);
            a8.PiecePlaced = rock3;
            rock3.ChessBoard = this;
            alivePieces.Add(rock3);

            Square h8 = allSquares.Find(square => square.Name == "h8");
            Piece rock4 = new Piece("black", "R", h8);
            h8.PiecePlaced = rock4;
            rock4.ChessBoard = this;
            alivePieces.Add(rock4);

            //black Knight
            Square b8 = allSquares.Find(square => square.Name == "b8");
            Piece knight3 = new Piece("black", "N", b8);
            b8.PiecePlaced = knight3;
            knight3.ChessBoard = this;
            alivePieces.Add(knight3);

            Square g8 = allSquares.Find(square => square.Name == "g8");
            Piece knight4 = new Piece("black", "N", g8);
            g8.PiecePlaced = knight4;
            knight4.ChessBoard = this;
            alivePieces.Add(knight4);

            //black Bishop
            Square c8 = allSquares.Find(square => square.Name == "c8");
            Piece bishop3 = new Piece("black", "B", c8);
            c8.PiecePlaced = bishop3;
            bishop3.ChessBoard = this;
            alivePieces.Add(bishop3);

            Square f8 = allSquares.Find(square => square.Name == "f8");
            Piece bishop4 = new Piece("black", "B", f8);
            f8.PiecePlaced = bishop4;
            bishop4.ChessBoard = this;
            alivePieces.Add(bishop4);

            //black Queen
            Square d8 = allSquares.Find(square => square.Name == "d8");
            Piece queen2 = new Piece("black", "Q", d8);
            d8.PiecePlaced = queen2;
            queen2.ChessBoard = this;
            alivePieces.Add(queen2);

            //black King
            Square e8 = allSquares.Find(square => square.Name == "e8");
            Piece king2 = new Piece("black", "K", e8);
            e8.PiecePlaced = king2;
            king2.ChessBoard = this;
            alivePieces.Add(king2);

        }
    }
}
