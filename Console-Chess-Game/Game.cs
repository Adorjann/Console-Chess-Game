using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game
{
    public class Game
    {
        private Player player1;
        private Player player2;
        private ChessBoard chessBoard;

        public Game(Player player1, Player player2, ChessBoard chessBoard)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.ChessBoard = chessBoard;
        }

        internal Player Player1 { get => player1; set => player1 = value; }
        internal Player Player2 { get => player2; set => player2 = value; }
        internal ChessBoard ChessBoard { get => chessBoard; set => chessBoard = value; }

        public void AssignPlayersTheirPieces()
        {

            this.chessBoard.alivePieces.ForEach(piece => { 
                
                piece.CalcPossibleMoves();
                if(piece.Color == this.player1.Name)
                {
                    this.player1.AlivePieces.Add(piece);
                }
                else
                {
                    this.player2.AlivePieces.Add(piece);
                }
            });
        }
        public void StartTheGame()
        {
            HowToPlay();
            int playing = 1;

            while(playing != 0)
            {
                //TestingPresentThePossibleMoves();
                string move = null;
                PrintTheChessBoard(chessBoard);

                switch (playing)
                {
                    case 0:
                        Console.WriteLine("Finnished Game! ");
                        break;

                    case 1:
                        bool retVal = false;
                        while (!retVal)
                        {
                            if (!retVal)
                            {
                              move =  moveInput(playing);
                            }
                            retVal = this.Player1.MakeAMove(move);
                            
                        }
                        playing = 2;
                        break;

                    case 2:
                        bool retVal2 = false;
                        while (!retVal2)
                        {
                            if (!retVal2)
                            {
                                move = moveInput(playing);
                            }
                            retVal2 = this.Player2.MakeAMove(move);
                        }
                        playing = 1;
                        break;

                    default:
                        Console.WriteLine("Unreachable"); 
                        break;

                }
                this.Player1.AlivePieces.ForEach(piece => { piece.CalcPossibleMoves(); });
                this.Player1.AlivePieces.ForEach(piece => { piece.CalcPossibleMoves(); });
                
            }
        }
        public string moveInput(int playing)
        {
            PrintWhoIsPlaying(playing);
            string move = Console.ReadLine();

            return move;
        }
        public void TestingPresentThePossibleMoves()
        {
            this.chessBoard.alivePieces.ForEach(piece =>
            {

                
                piece.CalcPossibleMoves();
                Console.Write(piece + piece.CurrentPlacement.Name+"| ");
                piece.PossibleMoves.ForEach(move =>
                {
                   Console.Write(move.Name+", ");
                });
                Console.WriteLine();



            });
        }


        public  void PrintTheChessBoard(ChessBoard board)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            List<Square> allSquares = board.allSquares;
            Console.WriteLine("\n");
            Console.WriteLine("\t   a     b     c     d     e     f     g     h");
            
            Console.WriteLine("\t.-----.-----.-----.-----.-----.-----.-----.-----.");

            for (int j = 8; j > 0; j--)
            {
                Console.Write($"      {j} |");
                printThePieces(j, allSquares);
                Console.WriteLine($" {j}");

                Console.WriteLine("\t.-----.-----.-----.-----.-----.-----.-----.-----.");
            }
            Console.WriteLine("\t   a     b     c     d     e     f     g     h");
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.White;
        }

        private  void printThePieces(int j, List<Square> allSquares)
        {

            List<Square> lineSq = allSquares.FindAll(sq => sq.Name.EndsWith($"{j}"));

            string entrance = "  ";
            lineSq.ForEach(sq => {
                if (sq.PiecePlaced != null)
                {
                    string piece = $"{sq.PiecePlaced.Name}";
                    
                    if(sq.PiecePlaced.Color == "black")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(entrance + piece);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("  |");
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(entrance + piece);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("  |");
                    }
                }
                else
                {
                    string emptySquare = $"   |";
                    Console.Write(entrance+emptySquare);
                }
            });
            //Console.Write(sb.ToString());

        }

        private void PrintWhoIsPlaying(int playing)
        {
            if(playing == 1)
            {
                Console.WriteLine("Player one is on the move! ");
            }
            else
            {
                Console.WriteLine("Player two is on the move! ");
            }

            

        }

        private void HowToPlay()
        {
            Console.WriteLine("\n\t How to move the Piece: ");
            Console.WriteLine("\t you need to type: location where piece to be moved is located");
            Console.WriteLine("\t you need to type: >> ");
            Console.WriteLine("\t you need to type: Location where you want to move the piece to");
            Console.WriteLine("\n\t Example:  a2>a4 ");
            Console.WriteLine("\t Meaning: I want too move the piece(pawn) from a1 to a4. ");
        }
    }
}
