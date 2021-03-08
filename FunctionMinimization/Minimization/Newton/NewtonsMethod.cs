using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.Newton
{
    public class NewtonsMethod : MinimizationMethod
    {
        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 500; t++)
            {
                x -= 1.0 / Gradient.Calculate2ndOrderGradient(function, x, 1.0E-10) * Gradient.CalculateGradient(function, x, 1.0E-10);
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
