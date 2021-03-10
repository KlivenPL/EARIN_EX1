using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.Newton
{
    public class NewtonsMethodNumeric : MinimizationMethod
    {
        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 100; t++)
            {
                x -= Gradient.CalculateGradientNum(function, x, 0.00001) / Gradient.Calculate2ndOrderGradientNum(function, x, 0.00001);
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
