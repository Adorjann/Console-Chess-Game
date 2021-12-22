using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console_Chess_Game.Comparator;

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
            int playerOrComputer = this.playerOrComputer();

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
                        playing = playerOrComputer;
                        break;

                    case 2:
                        PlayerPlaying(playing, this.Player2);
                        playing = 1;
                        break;
                    case 3:
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

        private int playerOrComputer()
        {
            Console.WriteLine("\nChose the game mode:");
            Console.WriteLine("type [1] to play >> Player VS Player <<");
            Console.WriteLine("type [2] to play >> Player VS Computer <<");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 2) { input = 3; } //in the switch, Computer is case 3
            if (input == 1) { input = 2; } //in the switch, Player2 is case 2
            

            return input;
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
                validMove = player.MakeAMove(move); //valid move == legal move (by the rules)
            }
        }
        private Stack<Piece> OponentsDeadPieces(Player player)
        {
            if (player.Equals(this.player1))
            {
                return  this.Player2.DeadPieces;
            }
            else if (player.Equals(this.player2))
            {
                return  this.Player1.DeadPieces;
            }

            return null;
        }

        private void moveInputWhenChecked(int playing,Player player)
        {
            //if the player is in check, he must play a move that's going to get him out of the check
            // otherwise the move is reversed

            Stack<Piece> enemyDeadPieces = OponentsDeadPieces(player);
            int numDeadPiecesBeforeMove = enemyDeadPieces.Count;
            int numDeadPiecesAfterMove = 0;

            bool kingSaved = false;
            while (!kingSaved)
            {
                string move = moveInput(playing); //input
                bool validMove = player.MakeAMove(move); //make the move
                numDeadPiecesAfterMove = enemyDeadPieces.Count;

                if (validMove && !IsCheck(player))    // is check?
                {
                    kingSaved = true;
                }
                else if (validMove && IsCheck(player))
                {
                    //PrintTheChessBoard(chessBoard);
                    Thread.Sleep(1000);
                    Console.WriteLine("\t>> Check is still ON <<");
                    Thread.Sleep(1000);
                    Console.WriteLine("\t>> Save your KING <<");
                    Thread.Sleep(2000);
                    reverseMove(player, move);                   
                    
                    //PrintTheChessBoard(chessBoard);
                }
            }
        }
        private void reverseMove(Player player,string move)
        {
            Stack<Piece> enemyDeadPieces = OponentsDeadPieces(player);
            int numDeadPiecesBeforeMove = enemyDeadPieces.Count;
            int numDeadPiecesAfterMove = 0;

            string[] squares = move.Split('>');
            Square originalSquare = chessBoard.allSquares.Find(sq => sq.Name == squares[0]);
            Square newSquare = chessBoard.allSquares.Find(sq => sq.Name == squares[1]);

            originalSquare.PiecePlaced = newSquare.PiecePlaced;
            originalSquare.PiecePlaced.CurrentPlacement = originalSquare;

            if (numDeadPiecesAfterMove > numDeadPiecesBeforeMove)
            {
                //if reversing a taken piece
                newSquare.PiecePlaced = enemyDeadPieces.Pop();
                newSquare.PiecePlaced.CurrentPlacement = newSquare;
            }
            else
            {
                //back to the check position
                newSquare.PiecePlaced = null;
            }
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
            player.IsCheck = retVal; 
            return retVal;
        } 

        public string moveInput(int playing)
        {
            PrintWhoIsPlaying(playing);
            string move = null;
            if (playing == 3)
            {
                //move = VerySuperSmartAIChessPlayer();  //Random moves Generator
                move = MiniMaxRootNode(2,true);        // MinMax algorithm  with alpha-beta pruning
                Console.WriteLine(move);
            }
            else
            {
                move = Console.ReadLine();
            }
            

            

            return move;
        }
        private List<string> PlayersMoves(Player player)
        {
            List<string> retVal = new();

            player.AlivePieces.ForEach(piece => {
                //possible moves are type Square
                piece.MovesCalc.PossibleMoves.ForEach(move => {
                    //formating the move like this ex: a1>a4
                    retVal.Add($"{piece.CurrentPlacement.Name}>{move.Name}");
                });
            });
            return retVal;
        }

        private string MiniMaxRootNode (int depth,bool isMaximisingPlayer)
        {
            //MiniMax algorithm with Alpha-Beta pruning

            List<string> moves = PlayersMoves(this.Player2);
            double bestMove = -9999;
            string bestMoveFound = null;

            for(int i =0; i< moves.Count; i++)
            {
                string move = moves[i];

                this.Player2.MakeAMove(move);
                double value = MiniMax(depth - 1, -10000, 10000, !isMaximisingPlayer);
                reverseMove(this.Player2, move);
                if(value >= bestMove)
                {
                    bestMove = value;
                    bestMoveFound = move;
                }

            }
            return bestMoveFound;



        }

        private double MiniMax(int depth,double alpha,double beta, bool isMaximisingPlayer)
        {
            if (depth == 0) { return -EvaluateBoard.GetBoardEvaluation(this.chessBoard); }

            if (isMaximisingPlayer)
            {
                List<string> moves = PlayersMoves(this.Player2);

                double bestMove = -9999;
                for (var i = 0; i < moves.Count; i++)
                {
                    //console.log(newGameMoves[i]);
                    string move = moves[1];
                    this.Player2.MakeAMove(move);
                    bestMove = Math.Max(bestMove, MiniMax(depth - 1, alpha, beta, !isMaximisingPlayer));
                    reverseMove(this.Player2, move);
                    alpha = Math.Max(alpha, bestMove);
                    if (beta <= alpha)
                    {
                        return bestMove;
                    }
                }
                return bestMove;
            }
            else
            {
                List<string> moves = PlayersMoves(this.Player1);
                double bestMove = 9999;
                for (var i = 0; i < moves.Count; i++)
                {
                    string move = moves[1];
                    this.Player1.MakeAMove(move);
                    //console.log(newGameMoves[i]);
                    bestMove = Math.Min(bestMove, MiniMax(depth - 1, alpha, beta, !isMaximisingPlayer));
                    reverseMove(this.Player1, move);
                    beta = Math.Min(beta, bestMove);
                    if (beta <= alpha)
                    {
                        return bestMove;
                    }
                }
                return bestMove;
            }
        }

        private string VerySuperSmartAIChessPlayer()
            //random moves generator
        {
            List<Piece> alivePieces = this.player2.AlivePieces;
            List<int> listIndexes = GetIndexes(alivePieces);
            listIndexes.Sort(new RandomComparator());   

            Square randomSquare = null;
            Piece randomPiece = null;
            while (randomSquare == null)
            {

                randomPiece = alivePieces[listIndexes[new Random().Next(0, listIndexes.Count)]]; 
                randomPiece.MovesCalc.calcSafePossibleMove();
                List<Square> randomPiecesSafeMoves = randomPiece.MovesCalc.SafePossibleMove;
                if (randomPiecesSafeMoves.Count == 0) { continue; }

                List<int> sqIndex = GetIndexes(randomPiecesSafeMoves);
                sqIndex.Sort(new RandomComparator());
                randomSquare = randomPiecesSafeMoves[sqIndex[new Random().Next(0, sqIndex.Count)]];


            }
            if(randomSquare == null)
            {
                //if there are no safe possible moves, play any possible move
                for (int i = 0; i <= listIndexes.Count(); i++)
                {
                    randomPiece = alivePieces[listIndexes[i]];
                    randomPiece.MovesCalc.CalcPossibleMoves();
                    List<Square> randomPossibleMoves = randomPiece.MovesCalc.PossibleMoves;
                    if (randomPossibleMoves.Count == 0) { continue; }

                    List<int> sqIndex = GetIndexes(randomPossibleMoves);
                    sqIndex.Sort(new RandomComparator());
                    randomSquare = randomPossibleMoves[sqIndex[new Random().Next(0, sqIndex.Count)]];

                    return $"{randomPiece.CurrentPlacement.Name}>{randomSquare.Name}";
                }
            }

            return $"{randomPiece.CurrentPlacement.Name}>{randomSquare.Name}";

        }


        private List<int> GetIndexes(List<Piece> lista)
        {
            List<int> retVal = new();
            for(int i = 0; i<lista.Count; i++)
            {
                retVal.Add(i);
            }
            return retVal;
        }
        private List<int> GetIndexes(List<Square> lista)
        {
            List<int> retVal = new();
            for (int i = 0; i < lista.Count; i++)
            {
                retVal.Add(i);
            }
            return retVal;
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
                Console.WriteLine("White is on the move! ");
            }
            else
            {
                Console.WriteLine("Black is on the move! ");
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
