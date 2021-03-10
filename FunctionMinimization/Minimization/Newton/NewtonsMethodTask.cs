using FunctionMinimization.Helpers;
using FunctionMinimization.UserInputs;
using Numpy;
using System;

namespace FunctionMinimization.Minimization.Newton
{
    public class NewtonsMethodTask : MinimizationMethod
    {
        public NewtonsMethodTask(UserInput userInput)
        {
            this.userInput = userInput;
        }

        private readonly UserInput userInput;

        public override MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0)
        {
            NDarray x = np.copy(x0);

            for (int t = 0; t < 100; t++)
            {
                x -= np.dot(Gradient.GradientTask(userInput), Gradient.Gradient2ndOrderTaskUserInput(userInput));
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
