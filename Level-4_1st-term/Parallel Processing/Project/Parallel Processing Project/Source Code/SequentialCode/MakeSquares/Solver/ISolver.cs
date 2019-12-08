using System.Threading.Tasks;

namespace Solvers
{
    public abstract class ISolver
    {
        public abstract Task TrySolve();
        protected abstract bool solve();
        protected abstract bool fit(long curtry);
        protected abstract short[,] dump();
    }
}
