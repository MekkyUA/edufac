using System.Collections.Generic;
using System.Linq;

namespace Shapers
{
    public class ShapeMan : IShapeMan
    {
        private Shape[] AllShapes;
        private volatile List<Shape>[] AllDistinctShapesPositions;
        private List<List<Shape>> ReadyToMakeSquareElements { get; set; }
        public List<List<Shape>> ReadyShapesLists
        {
            get
            {
                if (ReadyToMakeSquareElements.Count() == 0)
                {
                    GetEveryDistinctShapePositions();
                    GetEveryReadyPossibleCombination();
                }
                return ReadyToMakeSquareElements;
            }
        }

        public ShapeMan(List<Shape> allShapes)
        {
            AllShapes = allShapes.Select(e => new Shape(e._Shape)).ToArray();
            AllDistinctShapesPositions = new List<Shape>[AllShapes.Length];
            ReadyToMakeSquareElements = new List<List<Shape>>();
        }

        protected override void GetEveryDistinctShapePositions()
        {
            for (int i = 0; i < AllShapes.Length; i++)
                AllDistinctShapesPositions[i] = AllShapes[i].AllDistinctPositions;
        }

        protected override void GetEveryReadyPossibleCombination()
        {
            if (AllDistinctShapesPositions != null)
            {
                //COPY ARRAY OF LISTS OF OBJECTS BY VALUE
                var AllDistinctShapesPositionsCopy = AllDistinctShapesPositions.
                    Select(x => x.Select(obj => new Shape(obj._Shape)).ToList()).ToList();

                if (AllDistinctShapesPositionsCopy.Count > 0)
                {
                    foreach (var ShapePos in AllDistinctShapesPositionsCopy.ElementAt(0))
                        ReadyToMakeSquareElements.Add(new List<Shape> { new Shape (ShapePos._Shape) });
                    AllDistinctShapesPositionsCopy.RemoveAt(0);

                    foreach (var ShapePositions in AllDistinctShapesPositionsCopy)
                    {
                        var ReadyToMakeSquareElementsCopy = ReadyToMakeSquareElements.ToList();
                        ReadyToMakeSquareElements.Clear();

                        foreach (var ShapePos in ShapePositions)
                        {
                            //COPY LIST OF LISTS OF OBJECTS BY VALUE
                            var ReadyToMakeSquareElementsTmp = ReadyToMakeSquareElementsCopy.
                                Select(x => x.Select(obj => new Shape(obj._Shape)).ToList()).ToList();

                            foreach (var combinationTmp in ReadyToMakeSquareElementsTmp)
                                combinationTmp.Add(new Shape(ShapePos._Shape));
                            ReadyToMakeSquareElements.AddRange(ReadyToMakeSquareElementsTmp.ToList());
                        }
                    }
                }
            }
        }
    }
}
