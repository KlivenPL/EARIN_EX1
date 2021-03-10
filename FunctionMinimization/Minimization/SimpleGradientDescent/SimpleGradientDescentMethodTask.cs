using FunctionMinimization.Helpers;
using FunctionMinimization.UserInputs;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.SimpleGradientDescent
{
    public class SimpleGradientDescentMethodTask : MinimizationMethod
    {
        public SimpleGradientDescentMethodTask(UserInput userInput)
        {
            this.beta = userInput.Beta.Value;
            this.userInput = userInput;
        }

        private readonly UserInput userInput;

        private readonly double beta;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            bool success = Execute(() =>
            {
                x -= np.dot(Gradient.GradientTask(userInput, x), (NDarray)beta);
                return function(x);
            }, userInput.DesiredJOfX);

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
