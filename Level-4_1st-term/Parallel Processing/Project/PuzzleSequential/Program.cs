using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PuzzleSequential
{
    class Program
    {
        //  VARIABLES
        static int numpieces;
        static short[, ,] pieces = new short[5, 4, 4];
        static long[] piecerows = new long[5];
        static long[] piececols = new long[5];
        static short[,] board = new short[4, 4];

        static void Main(string[] args)
        {
            // Input Phase
            Console.WriteLine("Input Phase Start");
            #region CollectionOne
            //  Pieces Number
            numpieces = 4;

            //  Piece One
            piecerows[0] = 2;       //  ROW
            piececols[0] = 3;       //  COL
            pieces[0, 0, 0] = 1;    //  POS 0   0
            pieces[0, 0, 1] = 1;    //  POS 0   1
            pieces[0, 0, 2] = 1;    //  POS 0   2
            pieces[0, 1, 0] = 1;    //  POS 1   0
            pieces[0, 1, 1] = 0;    //  POS 1   1
            pieces[0, 1, 2] = 1;    //  POS 1   2

            //  Piece Two
            piecerows[1] = 4;   //  ROW
            piececols[1] = 2;   //  COL
            pieces[1, 0, 0] = 0;    //  POS 0   0
            pieces[1, 0, 1] = 1;    //  POS 0   1
            pieces[1, 1, 0] = 0;    //  POS 1   0
            pieces[1, 1, 1] = 1;    //  POS 1   1
            pieces[1, 2, 0] = 1;    //  POS 2   0
            pieces[1, 2, 1] = 1;    //  POS 2   1
            pieces[1, 3, 0] = 0;    //  POS 3   0
            pieces[1, 3, 1] = 1;    //  POS 3   1

            //  Piece Three
            piecerows[2] = 2;   //  ROW
            piececols[2] = 1;   //  COL
            pieces[2, 0, 0] = 1;    //  POS 0   0
            pieces[2, 1, 0] = 1;    //  POS 1   0

            //  Piece Four
            piecerows[3] = 3;   //  ROW
            piececols[3] = 2;   //  COL
            pieces[3, 0, 0] = 1;    //  POS
            pieces[3, 0, 1] = 0;    //  POS
            pieces[3, 1, 0] = 1;    //  POS
            pieces[3, 1, 1] = 0;    //  POS
            pieces[3, 2, 0] = 1;    //  POS
            pieces[3, 2, 1] = 1;    //  POS
            #endregion

            #region CollectionTwo
            #endregion


            Console.WriteLine("Input Phase Done");
            //  Solving Phase

            Console.WriteLine("================");
            Stopwatch timer = Stopwatch.StartNew();
            long answer = 0;
            //long solveA;
            //solveA = solve(ref answer);
            //if (solveA != 0)
            //{
            //    Console.WriteLine(solveA);
            //}
            //Console.WriteLine("===");

            // Input Phase
            if (Convert.ToBoolean(solve(ref answer)))
                dump(answer);
            else
                dump(-1);

            Console.WriteLine("Ended In {0} Seconds", timer.Elapsed);
            
            Console.ReadLine();
        }


        static long solve(ref long answer)
        {
            long solved = 0;
            long numtries = 1;
            long curtry;
            for (long x = 1; x <= numpieces; x++)
            {
                numtries *= 16;
            }
            for (curtry = 0; (curtry < numtries) && (solved == 0); curtry++)
            {
                if (fit(curtry) == 1)
                {
                    solved = 1;
                    Console.WriteLine("CurTry {0} and solved", curtry);
                }
            }
            answer = curtry - 1;
            return solved;
        }

        static long fit(long curtry)
        {
            long i, j, k;
            long boardpos, boardrow, boardcol;
            long solved = 0;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    board[i, j] = 0;
            for (i = 0; i < numpieces; i++)
            {
                boardpos = (long)(curtry % (long)Math.Pow(16, i + 1)) / (long)Math.Pow(16, i);
                boardrow = (boardpos / 4);
                boardcol = (boardpos % 4);
                for (j = 0; j < piecerows[i]; j++)
                {
                    for (k = 0; k < piececols[i]; k++)
                    {
                        if ((boardrow + j < 4) && (boardcol + k < 4))
                        {
                            board[boardrow + j, boardcol + k] += pieces[i, j, k];
                        }
                    }
                }

            }
            solved = 1;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (board[i, j] == 1 && solved == 1)
                    {
                        solved = 1;
                    }
                    else
                    {
                        solved = 0;
                    }
                }
            }

            return solved;
        }

        static void dump(long answer)
        {
            long pos, col, row;
            if (answer == -1)
            {
                Console.WriteLine("No solution possible");
            }
            else
            {
                int[,] b = new int[4, 4];
                int r, c;
                for (int i = 0; i < numpieces; i++)
                {
                    pos = (long)(answer % (long)Math.Pow(16, i + 1)) / (long)Math.Pow(16, i);
                    row = pos / 4;
                    col = pos % 4;
                    for (r = 0; r < piecerows[i]; r++)
                    {
                        for (c = 0; c < piececols[i]; c++)
                        {
                            if (pieces[i, r, c] > 0)
                            {
                                b[row + r, col + c] = i;
                            }
                        }
                    }
                }
                for (r = 0; r < 4; r++)
                {
                    for (c = 0; c < 4; c++)
                    {
                        Console.Write(b[r, c]);
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
