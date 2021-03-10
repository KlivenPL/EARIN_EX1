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

            bool success = Execute(() =>
            {
                x -= np.dot(Gradient.GradientTask(userInput, x), Gradient.Gradient2ndOrderTaskUserInput(userInput));
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
