namespace Shapers
{
    public abstract class IShapeMan
    {
        /// <summary>
        /// Foreach shape from AllShapes
        /// get all distinct possible shape  positions
        /// then add it to AllDistinctShapesPositions
        /// </summary>
        protected abstract void GetEveryDistinctShapePositions();

        /// <summary>
        /// Foreach ShapesPositions from AllDistictShapesPositions
        /// get all possible combination form these lists then
        /// add it to ReadyToMakeSquareElements
        /// </summary>
        protected abstract void GetEveryReadyPossibleCombination();
    }
}
