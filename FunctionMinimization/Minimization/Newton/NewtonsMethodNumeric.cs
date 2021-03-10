using FunctionMinimization.Helpers;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.Newton
{
    public class NewtonsMethodNumeric : MinimizationMethod
    {
        public NewtonsMethodNumeric(double desiredValue)
        {
            this.desiredValue = desiredValue;
        }

        private readonly double desiredValue;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            bool success = Execute(() =>
            {
                x -= Gradient.CalculateGradientNum(function, x, 0.00001) / Gradient.Calculate2ndOrderGradientNum(function, x, 0.00001);
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
