using Shapers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solvers
{
    public class Solver : ISolver
    {
        private const short MAX_PIECES = 5;
        private const short MAX_ROWS = 4;
        private const short MAX_COLS = 4;
        private int numpieces { set; get; }
        private short[] piecerows { set; get; }
        private short[] piececols { set; get; }
        private short[,,] pieces { set; get; }
        private short[,] board { set; get; }
        private long answer { get; set; }
        private dynamic UI { get; set; }
        public Solver(List<Shape> readyShapes, dynamic userInterface)
        {
            #region Vars Initialization
            piecerows = new short[MAX_PIECES];
            piececols = new short[MAX_PIECES];
            pieces = new short[MAX_PIECES, MAX_ROWS, MAX_COLS];
            board = new short[MAX_ROWS, MAX_COLS];
            #endregion
            numpieces = readyShapes.Count;
            for (int shapeIndex = 0; shapeIndex < readyShapes.Count; shapeIndex++)
            {
                piecerows[shapeIndex] = (short)readyShapes[shapeIndex]._ShapeConverted.GetLength(0);
                piececols[shapeIndex] = (short)readyShapes[shapeIndex]._ShapeConverted.GetLength(1);

                for (int rowIndex = 0; rowIndex < piecerows[shapeIndex]; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < piececols[shapeIndex]; colIndex++)
                    {
                        pieces[shapeIndex, rowIndex, colIndex] = 
                            readyShapes[shapeIndex]._ShapeConverted[rowIndex, colIndex];
                    }
                }
            }
            UI = userInterface;
        }
        public override Task TrySolve()
        {
            return new Task(() =>
            {
                if (solve())
                {
                    lock (UI.LOCK)
                    {
                        ShowThreadInformation("Task #" + Task.CurrentId.ToString());
                        UI.WriteLine(string.Format("CurTry {0} and solved", answer + 1));
                        var result = dump();
                        for (int i = 0; i < result.GetLength(0); i++)
                        {
                            var line = "";
                            for (int j = 0; j < result.GetLength(1); j++)
                            {
                                line += result[i, j].ToString();
                            }
                            UI.WriteLine(line);
                        }
                        UI.WriteLine("--------------------------------------------------------------------------------------------------------");
                        UI.TOTAL_SOLUTIONS++;
                    }
                }
            });
        }
        protected override bool solve()
        {
            bool solved = false;
            long numtries = 1;
            long curtry;
            for (long x = 1; x <= numpieces; x++)
                numtries *= 16;
            for (curtry = 0; (curtry < numtries) && (!solved); curtry++)
                if (fit(curtry))
                    solved = true;
            answer = curtry - 1;
            return solved;
        }
        protected override bool fit(long curtry)
        {
            long i, j, k;
            long boardpos, boardrow, boardcol;
            bool solved = false;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    board[i, j] = 0;
            for (i = 0; i < numpieces; i++)
            {
                boardpos = curtry % (long)Math.Pow(16, i + 1) / (long)Math.Pow(16, i);
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
            solved = true;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (board[i, j] == 1 && solved)
                    {
                        solved = true;
                    }
                    else
                    {
                        solved = false;
                    }
                }
            }

            return solved;
        }
        protected override short[,] dump()
        {
            long pos, col, row;
            if (answer != -1)
            {
                short[,] b = new short[4, 4];
                int r, c;
                for (int i = 0; i < numpieces; i++)
                {
                    pos = answer % (long)Math.Pow(16, i + 1) / (long)Math.Pow(16, i);
                    row = pos / 4;
                    col = pos % 4;
                    for (r = 0; r < piecerows[i]; r++)
                    {
                        for (c = 0; c < piececols[i]; c++)
                        {
                            try
                            {
                                if (pieces[i, r, c] > 0)
                                {
                                    b[row + r, col + c] = (short)i;
                                }
                            }
                            catch { }
                        }
                    }
                }
                return b;
            }
            return null;
        }
        private void ShowThreadInformation(string taskName)
        {
            string msg = null;
            Thread thread = Thread.CurrentThread;
            //Thread.BeginCriticalRegion();
            msg = string.Format("{0} thread information:", taskName) +
                    string.Format("   Background: {0}", thread.IsBackground) +
                    string.Format("   Thread Pool: {0}", thread.IsThreadPoolThread) +
                    string.Format("   Thread ID: {0}", thread.ManagedThreadId);
            //Thread.EndCriticalRegion();
            UI.WriteLine(msg);
        }
    }
}
