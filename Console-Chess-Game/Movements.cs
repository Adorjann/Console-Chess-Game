using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class Movements
    {

        

        internal static List<Square> Moving(Piece piece)
        {
            switch (piece.Name)
            {
                case "P":
                    return PawnMovements(piece);
                    break;
                case "N":
                    return KnightMovements(piece);
                    break;
                case "B":
                    return BishopMovement(piece);
                    break;
                case "R":
                    return RockMovements(piece);
                    break;
                case "Q":
                    return QueenMovements(piece);
                    break;
                case "K":
                    return KingMovements(piece);
                    break;
               default:
                    Console.WriteLine("*** Movements.moving default activated.");
                    return null;
                    break;
            }

        }

        public static List<Piece> Threats(Piece piece)
        {
            //checking if some enemy piece's possible move is this.Piece current position

            List<Piece> retVal = new List<Piece>();
            List<Piece> allPieces = piece.ChessBoard.alivePieces;

            allPieces.ForEach(p =>
            {
                if (p.Color != piece.Color)
                {
                    p.MovesCalc.CalcPossibleMoves();
                    p.MovesCalc.PossibleMoves.ForEach(sq =>
                    {
                        if (sq.Equals(piece.CurrentPlacement))
                        {
                            retVal.Add(p);
                        }

                    });
                    
                }
            });



            return retVal;
        }

        public static List<Square> PossibleThreats(Piece thisPiece)
        {
            //checking if this.Piece's possible move is some enemy piece's possible move
            //therefore it's a possible Threat

            List<Square> retVal = new List<Square>();
            List<Piece> allPieces = thisPiece.ChessBoard.alivePieces;
            thisPiece.MovesCalc.CalcPossibleMoves();
            List<Square> allPossibleMoves = thisPiece.MovesCalc.PossibleMoves;
            
            allPossibleMoves.ForEach(pMove => {

                allPieces.ForEach(piece =>
                {
                    if (piece.Color != thisPiece.Color)
                    {
                        piece.MovesCalc.CalcPossibleMoves();
                        piece.MovesCalc.PossibleMoves.ForEach(sq =>
                        {
                            if (sq.Equals(pMove))
                            {
                                retVal.Add(pMove);
                            }

                        });

                    }
                });
            });

            return retVal;
        }

        private static List<Square> KingMovements(Piece king)
        {
            List<Square> allSquares = king.ChessBoard.allSquares;
            List<Square> retVal = new List<Square>();

            char[] separatedName = king.CurrentPlacement.Name.ToCharArray(); //square name the piece is positioned at. example: a2 b3
            //current position
            int asciColumnPosition = Convert.ToInt32(separatedName[0]);
            int rowPosition = separatedName[1] - '0'; // convert Char to int

            castling(king,retVal);

            //Top
            int topRow = rowPosition + 1;
            if (topRow <= 8)
            {
                Square topSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(asciColumnPosition)}{topRow}");
                if (topSquare != null)
                {
                    retVal.Add(topSquare);
                }
            }
            //Top-Left
            int leftColumn = asciColumnPosition - 1;
            if (topRow <= 8 && leftColumn >= 97)
            {
                Square topLeftSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(leftColumn)}{topRow}");
                if (topLeftSquare != null)
                {
                    retVal.Add(topLeftSquare);
                }
            }
            //Top-Right
            int rightColumn = asciColumnPosition + 1;
            if (topRow <= 8 && rightColumn <= 104)
            {
                Square topRightSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(rightColumn)}{topRow}");
                if (topRightSquare != null)
                {
                    retVal.Add(topRightSquare);
                }
            }
            //Left
            if ( leftColumn >= 97)
            {
                Square LeftSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(leftColumn)}{rowPosition}");
                if (LeftSquare != null)
                {
                    retVal.Add(LeftSquare);
                }
            }
            //Right
            if (rightColumn <= 104)
            {
                Square rightSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(rightColumn)}{rowPosition}");
                if (rightSquare != null)
                {
                    retVal.Add(rightSquare);
                }
            }
            //Bottom
            int bottomRow = rowPosition - 1;
            if (bottomRow >= 1)
            {
                Square bottomSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(asciColumnPosition)}{bottomRow}");
                if (bottomSquare != null)
                {
                    retVal.Add(bottomSquare);
                }
            }
            //Bottom-Left
            if (bottomRow >= 1 && leftColumn >= 97)
            {
                Square bottomLeftSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(leftColumn)}{bottomRow}");
                if (bottomLeftSquare != null)
                {
                    retVal.Add(bottomLeftSquare);
                }
            }
            //Bottom-Right
            if (bottomRow >= 1 && rightColumn <= 104)
            {
                Square bottomRightSquare = PiecePossibleMovesChecker(king, $"{Convert.ToChar(rightColumn)}{bottomRow}");
                if (bottomRightSquare != null)
                {
                    retVal.Add(bottomRightSquare);
                }
            }

            return retVal;
        }

        private static void castling(Piece king, List<Square> retVal)
        {
            List<Square> allSquares = king.ChessBoard.allSquares;

            
            if (king.Color == "white")
            {
                Square f1 = FindSquareByName(allSquares, "f1");
                Square g1 = FindSquareByName(allSquares, "g1");
                Square h1 = FindSquareByName(allSquares,"h1");

                if(f1.PiecePlaced == null &&
                    g1.PiecePlaced == null && 
                    h1.PiecePlaced.Name == "R" &&
                    h1.PiecePlaced.MovesHistory.Count == 0 &&
                    king.MovesHistory.Count == 0)
                {
                    retVal.Add(g1);
                }
                Square b1 = FindSquareByName(allSquares, "b1");
                Square c1 = FindSquareByName(allSquares, "c1");
                Square d1 = FindSquareByName(allSquares, "d1");
                Square a1 = FindSquareByName(allSquares, "a1");
                if (b1.PiecePlaced == null &&
                    c1.PiecePlaced == null &&
                    d1.PiecePlaced == null &&
                    a1.PiecePlaced.Name == "R" &&
                    a1.PiecePlaced.MovesHistory.Count == 0 &&
                    king.MovesHistory.Count == 0)
                {
                    retVal.Add(c1);
                }
            }else
            {
                Square f8 = FindSquareByName(allSquares, "f8");
                Square g8 = FindSquareByName(allSquares, "g8");
                Square h8 = FindSquareByName(allSquares, "h8");

                if (f8.PiecePlaced == null &&
                    g8.PiecePlaced == null &&
                    h8.PiecePlaced.Name == "R" &&
                    h8.PiecePlaced.MovesHistory.Count == 0 &&
                    king.MovesHistory.Count == 0)
                {
                    retVal.Add(g8);
                }
                Square b8 = FindSquareByName(allSquares, "b8");
                Square c8 = FindSquareByName(allSquares, "c8");
                Square d8 = FindSquareByName(allSquares, "d8");
                Square a8 = FindSquareByName(allSquares, "a8");
                if (b8.PiecePlaced == null &&
                    c8.PiecePlaced == null &&
                    d8.PiecePlaced == null &&
                    a8.PiecePlaced.Name == "R" &&
                    a8.PiecePlaced.MovesHistory.Count == 0 &&
                    king.MovesHistory.Count == 0)
                {
                    retVal.Add(c8);
                }
            }

        }

        private static List<Square> QueenMovements(Piece queen)
        {
            List<Square> rockMoves = RockMovements(queen);
            List<Square> bishopMoves = BishopMovement(queen);

            if (rockMoves == null) { return bishopMoves; }
            if (bishopMoves == null) { return rockMoves; }

            return rockMoves.Concat(bishopMoves).ToList();
        }

        private static List<Square> RockMovements(Piece rock)
        {
            List<Square> allSquares = rock.ChessBoard.allSquares;
            List<Square> retVal = new List<Square>();

            char[] separatedName = rock.CurrentPlacement.Name.ToCharArray(); //square name the piece is positioned at. example: a2 b3
            //current position
            int asciColumnPosition = Convert.ToInt32(separatedName[0]);
            int rowPosition = separatedName[1] - '0'; // convert Char to int

            //UP
            int up = rowPosition + 1;
            while ( up <= 8)
            {
                Square square = PiecePossibleMovesChecker(rock, $"{Convert.ToChar(asciColumnPosition)}{up}");
                if (square != null)
                {
                    retVal.Add(square);
                    //If enemy is found, stop looking for possible moves;
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != rock.Color) { break; }
                }
                else { break; }
                up++;
            }
            //Down
            int down = rowPosition - 1;
            while (down >= 1)
            {
                Square square = PiecePossibleMovesChecker(rock, $"{Convert.ToChar(asciColumnPosition)}{down}");
                if (square != null)
                {
                    retVal.Add(square);
                    //If enemy is found, stop looking for possible moves;
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != rock.Color) { break; }
                }
                else { break; }
                down--;
            }
            //Left
            int left = asciColumnPosition - 1;
            while (left >= 97)
            {
                Square square = PiecePossibleMovesChecker(rock, $"{Convert.ToChar(left)}{rowPosition}");
                if (square != null)
                {
                    retVal.Add(square);
                    //If enemy is found, stop looking for possible moves;
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != rock.Color) { break; }
                }
                else { break; }
                left--;
            }
            //Right
            int right = asciColumnPosition + 1;
            while (right <= 104)
            {
                Square square = PiecePossibleMovesChecker(rock, $"{Convert.ToChar(right)}{rowPosition}");
                if (square != null)
                {
                    retVal.Add(square);
                    //If enemy is found, stop looking for possible moves;
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != rock.Color) { break; }
                }
                else { break; }
                right++;
            }
            return retVal;

        }

        private static List<Square> BishopMovement(Piece bishop)
        {
            List<Square> allSquares = bishop.ChessBoard.allSquares;
            List<Square> retVal = new List<Square>();

            char[] separatedName = bishop.CurrentPlacement.Name.ToCharArray(); //square name the piece is positioned at. example: a2 b3
            //current position
            int asciColumnPosition = Convert.ToInt32(separatedName[0]);
            int rowPosition = separatedName[1] - '0'; // convert Char to int

            //Up-Left
            int upLeft = rowPosition + 1;
            int leftUp = asciColumnPosition - 1;
            while(leftUp >= 97 && upLeft <= 8)
            {
                Square square = PiecePossibleMovesChecker(bishop, $"{Convert.ToChar(leftUp)}{upLeft}");
                if (square != null) 
                { 
                    retVal.Add(square);
                    //If enemy is found, stop looking for possible moves;
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != bishop.Color) { break; }  
                } 
                else{ break; }
                leftUp--; upLeft++;
            }
            //Down-Left
            int downLeft = rowPosition - 1;
            int leftDown = asciColumnPosition - 1;
            while (leftDown >= 97 && downLeft >= 1)
            {
                Square square = PiecePossibleMovesChecker(bishop, $"{Convert.ToChar(leftDown)}{downLeft}");
                if (square != null)
                {
                    retVal.Add(square);
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != bishop.Color) { break; }
                }
                else { break; }
                leftDown--; downLeft--;
            }
            //Up-Right
            int upRight = rowPosition + 1;
            int rightUp = asciColumnPosition + 1;
            while (rightUp <= 104 && upRight <= 8)
            {
                Square square = PiecePossibleMovesChecker(bishop, $"{Convert.ToChar(rightUp)}{upRight}");
                if (square != null)
                {
                    retVal.Add(square);
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != bishop.Color) { break; }
                }
                else { break; }
                rightUp++; upRight++;
            }
            //Down-Right
            int DownRight = rowPosition - 1;
            int rightDown = asciColumnPosition + 1;
            while (rightDown <= 104 && DownRight >= 1)
            {
                Square square = PiecePossibleMovesChecker(bishop, $"{Convert.ToChar(rightDown)}{DownRight}");
                if (square != null)
                {
                    retVal.Add(square);
                    if (square.PiecePlaced != null && square.PiecePlaced.Color != bishop.Color) { break; }
                }
                else { break; }
                rightDown++; DownRight--;
            }
            return retVal;
        }
        private static List<Square> KnightMovements(Piece knightPiece)
        {
            List<Square> allSquares = knightPiece.ChessBoard.allSquares;
            List<Square> retVal = new List<Square>();

            char[] separatedName = knightPiece.CurrentPlacement.Name.ToCharArray(); //square name the piece is positioned at. example: a2 b3
            //current position
            int asciColumnPosition = Convert.ToInt32(separatedName[0]);
            int rowPosition = separatedName[1] - '0'; // convert Char number to int

            int iterCount = 0;
            for (int i = asciColumnPosition-2; i <= asciColumnPosition + 2; i++)
            {
                iterCount++;
                if(i<97 || i>104 || i == asciColumnPosition) { continue;}

                //checking positions above the current position knight is at
                int topRow = 0; 
                if (iterCount == 1 || iterCount == 5) { topRow = rowPosition + 1; }
                if (iterCount == 2 || iterCount == 4) { topRow = rowPosition + 2; }
                if (topRow <= 8) {
                    Square square = PiecePossibleMovesChecker(knightPiece, $"{Convert.ToChar(i)}{topRow}");
                    if (square != null) { retVal.Add(square); }
                }
                //checking positions below the current position knight is at
                int bottomRow = 0;
                if (iterCount == 1 || iterCount == 5) { bottomRow = rowPosition - 1; }
                if (iterCount == 2 || iterCount == 4) { bottomRow = rowPosition - 2; }
                if (bottomRow >=1)
                {
                    Square square = PiecePossibleMovesChecker(knightPiece, $"{Convert.ToChar(i)}{bottomRow}");
                    if (square != null) { retVal.Add(square); }
                }
            }
            return retVal;
        }
        private static Square PiecePossibleMovesChecker(Piece Piece, string squarePosition)
        {
            List<Square> allSquares = Piece.ChessBoard.allSquares;

            Square square = FindSquareByName(allSquares,squarePosition);
            if ((square.PiecePlaced == null) || (square.PiecePlaced.Color != Piece.Color))  
            {   //a square is valid if it's empty or an enemy is placed on it.
                return square;
            }

                return null;
        }


        private static List<Square> PawnMovements(Piece pawnPiece)
        {
            List<Square> allSquares = pawnPiece.ChessBoard.allSquares;
            List<Square> retVal = new List<Square>();

            //square name example: a2,b3 [column,row]  
            char[] separatedName = pawnPiece.CurrentPlacement.Name.ToCharArray(); 

            if (pawnPiece.MovesHistory.Count == 0)
            {   //if it's pawns first move in the game pawn can move two squares forward

                Square square1 = null;
                Square square2 = null;
                if (pawnPiece.Color == "white")
                {
                    square1 = FindSquareByName(allSquares,$"{separatedName[0]}{3}");
                    square2 = FindSquareByName(allSquares,$"{separatedName[0]}{4}"); 

                }else if(pawnPiece.Color == "black")
                {
                    square1 = FindSquareByName(allSquares, $"{separatedName[0]}{6}");
                    square2 = FindSquareByName(allSquares, $"{separatedName[0]}{5}");
                }
                if(square1.PiecePlaced == null) //only if the first square is empty, both squares forward are avaiable
                {
                    if (square1 != null) { retVal.Add(square1); } 
                    if (square2 != null && square2.PiecePlaced == null) { retVal.Add(square2); } //second square is avaiable only if it's empty 
                }
                
            }
            else
            {
                int currentRow = separatedName[1] - '0';
                //white player moves 1st->8th row, black player moves 8th->1st row
                int nextSquare = pawnPiece.Color == "white" ? Convert.ToInt32(currentRow) + 1 : Convert.ToInt32(currentRow) - 1;
                if(nextSquare<=8 && nextSquare >= 1)
                {
                    Square square1 = FindSquareByName(allSquares, $"{separatedName[0]}{nextSquare}");
                    if (square1 != null && square1.PiecePlaced == null)
                    {
                        retVal.Add(square1);
                    }
                }
                
            }
            List<Square> pawnsFood = isThereAnithingPawnCanTake(separatedName,pawnPiece);
            if(pawnsFood.Count > 0)
            {
                //from pawnsFood relocate to retVall
                pawnsFood.ForEach(square => retVal.Add(allSquares.Find(s => s.Name == square.Name)));
            }

            return retVal;
        }

        public static List<Square> isThereAnithingPawnCanTake(char[] squareName, Piece thisPawn)
        {   //determining the square, where enemy needs to be, so the pawn would take it

            int currentRow = squareName[1] - '0';

            List<Square> retVal = new List<Square>();
            int nextRow;
            if (thisPawn.Color == "white")
            {
                nextRow = currentRow + 1;
            }else
            {
                nextRow = currentRow - 1;
            }
             
            int previousColumn = Convert.ToInt32(squareName[0]) -1;
            int nextColumn = Convert.ToInt32(squareName[0]) +1;

            string positionForEating1 = null;
            string positionForEating2 = null;
            string[] positionsForEating = { positionForEating1, positionForEating2 };

            if (previousColumn == 96)
            {       //96 is before column 'a', therefore only right diagonal is available
                positionsForEating[1] = $"{Convert.ToChar(nextColumn)}{nextRow}";
                
            }else if(nextColumn == 105)
            {       //105 is after column 'h', therefore only left diagonal is available
                positionsForEating[0] = $"{Convert.ToChar(previousColumn)}{nextRow}";
            }
            else
            {
                //both diagonal positions are available
                positionsForEating[0] = $"{Convert.ToChar(previousColumn)}{nextRow}";
                positionsForEating[1] = $"{Convert.ToChar(nextColumn)}{nextRow}";
            }
            foreach (string position in positionsForEating)
            {
                if(position != null && isEnemyPresent(thisPawn, position))
                {
                    Square s = FindSquareByName(thisPawn.ChessBoard.allSquares, position);
                    retVal.Add(s);
                }
            }



            return retVal;
        }
        private static bool isEnemyPresent(Piece thisPawn,string position)
        {   //checking is there an enemy on the diagonal position
            List<Piece> allAlivePieces = thisPawn.ChessBoard.alivePieces;
            Piece enemy = allAlivePieces.Find(piece => piece.CurrentPlacement.Name == position); 

            if(enemy != null && !enemy.Color.Equals(thisPawn.Color))
            {
                return true;
            }
            return false;
        }

        private static Square FindSquareByName(List<Square> allSquares, string squareName)
        {
           return allSquares.Find(square => square.Name == squareName);
        }
        

    }
}
