using System.Collections.Generic;
using System.Linq;

namespace Shapers
{
    public class Shape : IShape
    {
        public short[][] _Shape { get; private set; }
        public List<Shape> AllDistinctPositions
        {
            get
            {
                return GetAllDistinctPositions();
            }
        }
        public short[,] _ShapeConverted
        {
            get
            {
                short[,] shapeConverted = null;
                if (_Shape != null)
                {
                    var rowsCount = _Shape.Length;
                    var colsCount = _Shape.FirstOrDefault().Length;
                    //Initialize _Shape
                    shapeConverted = new short[rowsCount, colsCount];
                    //Copy Elements
                    for (int i = 0; i < rowsCount; i++)
                    {
                        for (int j = 0; j < colsCount; j++)
                        {
                            shapeConverted[i, j] = _Shape[i][j];
                        }
                    }
                }
                return shapeConverted;
            }
        }
        public Shape(short[][] shape)
        {
            _Shape = shape.Select(e => e.ToArray()).ToArray();
        }
        public Shape(short[,] shape)
        {
            if (shape != null)
            {
                var rowsCount = shape.GetLength(0);
                var colsCount = shape.GetLength(1);
                //Initialize _Shape
                _Shape = new short[rowsCount][];
                for (int i = 0; i < rowsCount; i++)
                    _Shape[i] = new short[colsCount];
                //Copy Elements
                for (int i = 0; i < rowsCount; i++)
                {
                    for (int j = 0; j < colsCount; j++)
                    {
                        _Shape[i][j] = shape[i, j];
                    }
                }
            }
        }
        protected override short[][] Rotate90()
        {
            int numOfRow = _Shape.Length;
            int numOfCol = _Shape[0].Length;
            short[][] shape = new short[numOfCol][];
            for (int f = 0; f < numOfCol; f++)
            {
                shape[f] = new short[numOfRow];
            }
            for (int i = 0; i < numOfRow; i++)
            {
                for (int j = 0; j < numOfCol; j++)
                {
                    shape[(numOfCol - 1 - j)][(i)] = _Shape[i][j];
                }
            }
            return shape;
        }
        protected override short[][] FlipVertical()
        {
            int numOfRow = _Shape.Length;
            int numOfCol = _Shape[0].Length;
            short[][] shape = new short[numOfRow][];
            for (int f = 0; f < numOfRow; f++)
            {
                shape[f] = new short[numOfCol];
            }
            for (int i = 0; i < numOfRow; i++)
            {
                for (int j = 0; j < numOfCol; j++)
                {
                    shape[(numOfRow - 1 - i)][(j)] = _Shape[i][j];
                }
            }
            return shape;
        }
        protected override bool Equals(Shape shape)
        {
            int numOfRow = _Shape.Length;
            int numOfCol = _Shape[0].Length;
            if (_Shape.Length == shape._Shape.Length && _Shape[0].Length == shape._Shape[0].Length)
                for (int i = 0; i < numOfRow; i++)
                {
                    for (int j = 0; j < numOfCol; j++)
                    {
                        if (_Shape[i][j] != shape._Shape[i][j])
                        {
                            return false;
                        }
                    }
                }
            else return false;
            return true;
        }
        protected override List<Shape> GetAllDistinctPositions()
        {
            #region rotate shape
            Shape local_shape_rotate = new Shape(_Shape);
            var allDistinctPositions = new List<Shape>();
            allDistinctPositions.Add(local_shape_rotate); // add original shape  (1)
            local_shape_rotate = new Shape(local_shape_rotate.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_rotate)) == -1)
                allDistinctPositions.Add(local_shape_rotate); // add shape with rotational angle 90  (2)
            local_shape_rotate = new Shape(local_shape_rotate.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_rotate)) == -1)
                allDistinctPositions.Add(local_shape_rotate); // add shape with rotational angle 180  (3)
            local_shape_rotate = new Shape(local_shape_rotate.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_rotate)) == -1)
                allDistinctPositions.Add(local_shape_rotate); // add shape with rotational angle 270  (4)
            #endregion

            #region flip vertical shape
            Shape local_shape_flip_vertical = new Shape(_Shape);
            local_shape_flip_vertical = new Shape(local_shape_flip_vertical.FlipVertical());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_flip_vertical)) == -1)
                allDistinctPositions.Add(local_shape_flip_vertical); // add shape with flip horizontal  (5)
            local_shape_flip_vertical = new Shape(local_shape_flip_vertical.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_flip_vertical)) == -1)
                allDistinctPositions.Add(local_shape_flip_vertical); // add shape with flip horizontal and rotational angle 90  (6)
            local_shape_flip_vertical = new Shape(local_shape_flip_vertical.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_flip_vertical)) == -1)
                allDistinctPositions.Add(local_shape_flip_vertical); // add shape with flip horizontal and rotational angle 180  (7)
            local_shape_flip_vertical = new Shape(local_shape_flip_vertical.Rotate90());
            if (allDistinctPositions.FindIndex(s => s.Equals(local_shape_flip_vertical)) == -1)
                allDistinctPositions.Add(local_shape_flip_vertical); // add shape with flip horizontal and rotational angle 270  (8)
            #endregion

            return allDistinctPositions;
        }
    }
}
