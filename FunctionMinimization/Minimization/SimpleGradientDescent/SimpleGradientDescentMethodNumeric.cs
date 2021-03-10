using FunctionMinimization.Helpers;
using Numpy;
using System;
using System.Diagnostics;

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

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int t = 0; t < 500; t++)
            {
                if (sw.ElapsedMilliseconds > 10000)
                {
                    break;
                }

                x -= beta * Gradient.CalculateGradientNum(function, x, 0.00001);
            }

            sw.Stop();

            var result = new MinimizationMethodResult
            {
                XStar = x,
                JofXStar = function(x),
            };

            return result;
        }
    }
}
