using Numpy;
using System;

namespace FunctionMinimization.Minimization
{
    public abstract class MinimizationMethod
    {
        public abstract MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0);
        protected double TestFunction(NDarray x)
        {
            return (double)(x[0] * x[0] * x[0] * x[0] + x[1] * x[1] * x[1] * x[1] - x[0] * x[1] + 2 * x[2] * x[2] + 2);
        }
    }
}
