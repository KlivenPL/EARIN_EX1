using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.SimpleGradientDescent
{
    public class SimpleGradientDescentMethodNumeric : MinimizationMethod
    {
        public SimpleGradientDescentMethodNumeric(double beta)
        {
            this.beta = beta;
        }

        private readonly double beta;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 500; t++)
            {
                x -= beta * Gradient.CalculateGradientNum(function, x, 0.00001);
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
