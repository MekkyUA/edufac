using Shapers;
using Solvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeSquares
{
    public partial class MainForm : Form
    {
        public volatile object LOCK = new object();
        public int TOTAL_SOLUTIONS = 0;
        private List<Shape> shapes;
        public MainForm()
        {
            InitializeComponent();
        }
        public void WriteLine(string str)
        {
            ResultsTextBox.AppendLine(str);
        }
        private async void solveBtn_Click(object sender, EventArgs e)
        {
            solveBtn.Enabled = false;
            solveBtn.Text = "Solving...";
            TOTAL_SOLUTIONS = 0;
            ResultsTextBox.Clear();

            #region TEST COLLECTION:
            //short[,] A = new short[,]
            //{
            //    { 1, 1, 1 },
            //    { 1, 0, 1 }
            //};//4 Distinct
            //short[,] B = new short[,]
            //{
            //    { 0, 1 },
            //    { 0, 1 },
            //    { 1, 1 },
            //    { 0, 1 }
            //};//8 Distinct
            //short[,] C = new short[,]
            //{
            //    { 1 },
            //    { 1 },
            //};//2 Distinct
            //short[,] D = new short[,]
            //{
            //    { 1, 0 },
            //    { 1, 0 },
            //    { 1, 1 }
            //};//8 Distinct

            //var sm = new ShapeMan(new List<Shape> { new Shape(A), new Shape(B), new Shape(C), new Shape(D) });

            //foreach (var item in sm.ReadyShapesLists)
            //    await new Solver(item, this).TrySolve();

            //if(TOTAL_SOLUTIONS == 0)
            //    WriteLine("No Possible Solution!");
            #endregion

            if (await ReadShapes())
            {
                ShapeMan sm = new ShapeMan(shapes);
                List<Task> tasks = new List<Task>(sm.ReadyShapesLists.Count);
                foreach (var item in sm.ReadyShapesLists)
                    tasks.Add(new Solver(item, this).TrySolve()); //auto writes
                var sw = new Stopwatch();

                #region Solving Sequential
                sw.Start();
                await Task.Run(() => 
                {
                    foreach (var task in tasks)
                        task.RunSynchronously();
                    sw.Stop();
                });
                #endregion

                if (TOTAL_SOLUTIONS == 0)
                    WriteLine(string.Format("No Possible Solution!, Time Elapsed: {0}s.",
                        (sw.ElapsedMilliseconds / 1000.0).ToString()));
                else
                    WriteLine(string.Format("Total Possible Solutions : {0}, Time Elapsed: {1}s.",
                        TOTAL_SOLUTIONS.ToString(), (sw.ElapsedMilliseconds / 1000.0).ToString()));
            }
            else
            {
                WriteLine("Cannot Read Shapes!,");
                WriteLine("Please Check & Format Input Correctly.");
            }

            solveBtn.Text = "Solve";
            solveBtn.Enabled = true;
        }
        private Task<bool> ReadShapes()
        {
            return Task.Run(() => 
            {
                var input_lines = ShapesTextBox.Lines;
                shapes = new List<Shape>();
                short[,] shape = null;
                for (int i = 0; i < input_lines.Length; i++)
                {
                    try
                    {
                        var rows = (int)char.GetNumericValue(input_lines[i][0]);
                        var cols = (int)char.GetNumericValue(input_lines[i][2]);
                        shape = new short[rows, cols];
                        i++; // i -> Line Index
                        for (int rowIdx = 0; rowIdx < rows; rowIdx++)
                        {
                            for (int colIdx = 0; colIdx < cols; colIdx++)
                            {
                                shape[rowIdx, colIdx] = (short)char.GetNumericValue(input_lines[i][colIdx]);
                            }
                            i++;
                        }
                        shapes.Add(new Shape(shape));
                    }
                    catch
                    {
                        return false;
                    }
                }

                if (shapes.Count >= 4 && shapes.Count <= 5)
                    return true;
                return false;
            });
        }
    }
    public static class WinFormsExtensions
    {
        public static void AppendLine(this TextBox source, string value)
        {
            source.Invoke(new Action(() => 
            {
                if (source.Text.Length == 0)
                    source.Text = value;
                else
                    source.AppendText(Environment.NewLine + value);
            }));
        }
    }
}
