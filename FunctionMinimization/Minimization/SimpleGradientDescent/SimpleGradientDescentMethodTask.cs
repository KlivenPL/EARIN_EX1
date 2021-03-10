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
            //this.beta = beta;
            this.beta = 0.01;
            this.userInput = userInput;
        }

        private readonly UserInput userInput;

        private readonly double beta;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 500; t++)
            {
                x -= np.dot(Gradient.GradientTask(userInput), (NDarray)beta);
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
