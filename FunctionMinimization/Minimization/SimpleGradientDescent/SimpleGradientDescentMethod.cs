using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.SimpleGradientDescent
{
    class SimpleGradientDescentMethod : MinimizationMethod
    {
        public SimpleGradientDescentMethod()
        {
            beta = 0.0001;
        }

        private readonly double beta;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 500; t++)
            {
                x -= beta * Gradient.CalculateGradient(function, x, 1);
            }

            var result = new MinimizationMethodResult
            {
                XStar = x,
                JofXStar = function(x),
            };

            return result;
        }
    }
}
