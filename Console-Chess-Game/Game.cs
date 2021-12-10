using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                
                piece.MovesCalc.CalcPossibleMoves();
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
                
                PrintTheChessBoard(chessBoard);

                switch (playing)
                {
                    case 0:
                        Console.WriteLine("Finnished Game! ");
                        break;

                    case 1:
                        PlayerPlaying(playing, this.Player1);
                        playing = 2;
                        break;

                    case 2:
                        PlayerPlaying(playing, this.Player2);
                        playing = 1;
                        break;

                    default:
                        Console.WriteLine("Unreachable"); 
                        break;

                }
                this.Player1.AlivePieces.ForEach(piece => { piece.MovesCalc.CalcPossibleMoves(); });
                this.Player2.AlivePieces.ForEach(piece => { piece.MovesCalc.CalcPossibleMoves(); });
                
            }
        }

        private void PlayerPlaying(int playing, Player player)
        {
            string move = null;
            bool validMove = false;
            while (!validMove)
            {
                if (IsCheck(player))
                {
                    Console.WriteLine($">> CHECK <<");
                    Console.WriteLine($"\nYour King is Threatened! Save your king!");
                    moveInputWhenChecked(playing, player);
                    break;
                }

                if (!validMove)
                {
                    move = moveInput(playing);
                }
                validMove = player.MakeAMove(move);
            }
        }

        private void moveInputWhenChecked(int playing,Player player)
        {
            bool kingSaved = false;
            while (!kingSaved)
            {
                string move = moveInput(playing); //input
                bool validMove = player.MakeAMove(move); //try the move
                
                if (validMove && !IsCheck(player))    // is check?
                {
                    kingSaved = true;
                }
                else
                {
                    PrintTheChessBoard(chessBoard);
                    Thread.Sleep(1000);
                    Console.WriteLine("\t>> Check is still ON <<");
                    Thread.Sleep(1000);
                    Console.WriteLine("\t>> Save your KING <<");
                    Thread.Sleep(2000);
                    reverseMove(move);   //back to the check position
                    PrintTheChessBoard(chessBoard);
                }
            }
        }

        private void reverseMove(string move)
        {
            //reversing the piece to original square

            string[] squares = move.Split('>');
            Square originalSquare = chessBoard.allSquares.Find(sq => sq.Name == squares[0]);
            Square newSquare = chessBoard.allSquares.Find(sq => sq.Name == squares[1]);

            originalSquare.PiecePlaced = newSquare.PiecePlaced;
            originalSquare.PiecePlaced.CurrentPlacement = originalSquare;
            newSquare.PiecePlaced = null;

        }

        private void printCurrentThreats(Player player) 
        {
            player.AlivePieces.ForEach(piece => { 
                    
                piece.MovesCalc.CalcCurrentThreats(); 
                if(piece.MovesCalc.CurrentThreats.Count > 0)
                {
                    Console.WriteLine($">> pay attention! <<");
                    piece.MovesCalc.CurrentThreats.ForEach(treath => {
                        Console.WriteLine($"Your {piece.Name} on {piece.CurrentPlacement} is under threat from {treath.Name} on {treath.CurrentPlacement}");
                    });

                    
                }
            });
        }

        private bool IsCheck(Player player)
        {
            bool retVal = false;
            player.AlivePieces.ForEach(piece => {

                piece.MovesCalc.CalcCurrentThreats();
                if (piece.MovesCalc.CurrentThreats.Count > 0 && piece.Name == "K")
                {
                    retVal = true;
                }
            });
            return retVal;
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
                piece.MovesCalc.CalcPossibleMoves();
                
                if (piece.MovesCalc.PossibleMoves.Count > 0)
                {
                    Console.Write(piece + piece.CurrentPlacement.Name + "| ");
                    piece.MovesCalc.PossibleMoves.ForEach(move =>
                    {
                        Console.Write(move.Name + ", ");
                    });
                    Console.WriteLine();
                }
                



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
            Console.WriteLine("\n\t Example:  a2>a4 ");
            Console.WriteLine("\t Meaning: I want to move the piece from a2 to a4. ");
        }
    }
}
