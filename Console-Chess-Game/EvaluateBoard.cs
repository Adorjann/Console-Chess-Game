using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    internal class EvaluateBoard
    {
        /*
         * calculating the board value, for the MiniMax algorithm
         */
        private static Dictionary<string,double[,]> positionValues = new();


        public static double GetBoardEvaluation(ChessBoard board)
        {
            double totalEvaluation = 0;

            board.alivePieces.ForEach(piece =>
            {

                totalEvaluation += getPieceValue(piece);
            });


            return totalEvaluation;
        }

        private static double getPieceValue(Piece piece)
        {
            if (piece == null) { return 0; }

            double absoluteValue = 0;
            if (piece.Name == "P")
            {
                absoluteValue = 10 + CurrentPositionValue(piece);
            }
            else if (piece.Name == "R")
            {
                absoluteValue = 50 + CurrentPositionValue(piece);
            }
            else if (piece.Name == "N")
            {
                absoluteValue = 30 + CurrentPositionValue(piece);
            }
            else if (piece.Name == "B")
            {
                absoluteValue = 30 + CurrentPositionValue(piece);
            }
            else if (piece.Name == "Q")
            {
                absoluteValue = 90 + CurrentPositionValue(piece);

            } else if (piece.Name == "K")
            {
                absoluteValue = 900 + CurrentPositionValue(piece);
            }

            if (piece.Color == "white") { return absoluteValue; }
            else { return -absoluteValue; }
        }

        private static void PositionValues()
        {
            double[,] P =
            {
                { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                { 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0},
                {1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0},
                {0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5},
                { 0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0},
                { 0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5},
                { 0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5},
                { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0}
            };

            double[,] P_black = reverseArray(P);

            double[,] N =

            {
                { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
                { -4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0},
                { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0},
                { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0},
                { -3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0},
                { -3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0},
                { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0},
                {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
            };

            double[,] B =
            {
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 },
            { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 },
            { -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 },
            { -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 },
            { -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 },
            { -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 },
            { -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 },
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 }
            };

            double[,] B_black = reverseArray(B);

            double[,] R =
            {
            { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            { 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 },
            { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
            { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
            { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
            { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
            { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
            { 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 }
            };

            double[,] R_black = reverseArray(R);

            double[,] Q =
            {
            {  -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 },
            { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 },
            { -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0},
            { -0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 },
            { 0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 },
            { -1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 },
            { -1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0 },
            { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 }
            };

            double[,] K =
            {
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 },
            { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 },
            { 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 },
            { 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 }
            };

            double[,] K_black = reverseArray(K);
            

            if (!positionValues.ContainsKey("P")) { positionValues.Add("P",P); }
            if (!positionValues.ContainsKey("P_black")) { positionValues.Add("P_black", P_black); }
            if (!positionValues.ContainsKey("N")) { positionValues.Add("N",N); }
            if (!positionValues.ContainsKey("B")) { positionValues.Add("B",B); }
            if (!positionValues.ContainsKey("B_black")) { positionValues.Add("B_black", B_black); }
            if (!positionValues.ContainsKey("R")) { positionValues.Add("R",R); }
            if (!positionValues.ContainsKey("R_black")) { positionValues.Add("R_black", R_black); }
            if (!positionValues.ContainsKey("Q")) { positionValues.Add("Q",Q); }
            if (!positionValues.ContainsKey("K")) { positionValues.Add("K",K); }
            if (!positionValues.ContainsKey("K_black")) { positionValues.Add("K_black", K_black); }
        }

        public static double CurrentPositionValue(Piece piece)
        {
            PositionValues();

            double[,] piecesPositionValueArray = null;
                    
            if(piece.Color == "black")      //finding the apropriate array
            {
                string blackPieceArrName = $"{piece.Name}_{piece.Color}";
                piecesPositionValueArray = positionValues.GetValueOrDefault(blackPieceArrName);

                if (piecesPositionValueArray == null) { piecesPositionValueArray = positionValues.GetValueOrDefault($"{piece.Name}"); }
            }else if(piece.Color == "white")
            {
                string whitePieceArrName = piece.Name;
                piecesPositionValueArray = positionValues.GetValueOrDefault(whitePieceArrName);
            }

            if(piecesPositionValueArray != null)
            {

                int positionCol = Convert.ToInt32(piece.CurrentPlacement.Name[0] - 97);        // a(97) - 97 = 0 -> column 0
                int positionRow = 8 - Convert.ToInt32(piece.CurrentPlacement.Name[1] - '0');   // positionRow = 8 - positionRowInChessBoard

                return piecesPositionValueArray[positionRow, positionCol];
               
            }
            return -100000000;

        }




        private static double[,] reverseArray(double[,] positions)
        {
            double[,] reversed = new double[8, 8];

            int rowR = 0;
            for (int row = 7; row >= 0; row--)
            {
                for (int col = 0; col < 8; col++)
                {
                    reversed[rowR, col] = positions[row, col];
                    
                }
                
                rowR++;
            }
            return reversed;
        }
    }
}
