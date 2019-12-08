using System.Collections.Generic;

namespace Shapers
{
    public abstract class IShape
    {
        protected abstract short[][] Rotate90();
        protected abstract short[][] FlipVertical();
        protected abstract bool Equals(Shape shape);
        /// <summary>
        /// Get all distinct possible shape positions
        /// </summary>
        /// <returns>List of all distinct possible shape positions</returns>
        protected abstract List<Shape> GetAllDistinctPositions();
    }
}
