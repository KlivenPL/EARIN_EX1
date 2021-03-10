using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.SimpleGradientDescent
{
    public class SimpleGradientDescentMethodNumeric : MinimizationMethod
    {
        public SimpleGradientDescentMethodNumeric(double beta, double desiredValue)
        {
            this.beta = beta;
            this.desiredValue = desiredValue;
        }

        private readonly double beta, desiredValue;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            bool success = Execute(() =>
            {
                x -= beta * Gradient.CalculateGradientNum(function, x, 0.00001);
                return function(x);
            }, desiredValue);

            var result = new MinimizationMethodResult
            {
                XStar = x,
                JofXStar = function(x),
                Timeout = !success,
            };

            return result;
        }
    }
}
