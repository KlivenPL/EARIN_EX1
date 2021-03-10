using FunctionMinimization.UserInputs;
using Numpy;
using System;

namespace FunctionMinimization.Helpers
{
    static class Gradient
    {
        public static NDarray CalculateGradientNum(Func<NDarray, double> function, NDarray x, double step)
        {
            NDarray gradient = np.zeros(x.size);

            for (int i = 0; i < x.size; i++)
            {
                var xMovedInIthDirection = x.copy();
                var origXt = (double)x[i];

                xMovedInIthDirection[i] = (NDarray)(origXt + step);

                var gradientNum = (function(xMovedInIthDirection) - function(x)) / step;
                gradient[i] = (NDarray)gradientNum;
            }

            return gradient;
        }

        public static NDarray Calculate2ndOrderGradientNum(Func<NDarray, double> function, NDarray x, double step)
        {
            NDarray gradient2ndOrder = np.zeros(x.size);

            for (int i = 0; i < x.size; i++)
            {
                var xMovedInIthDirection = x.copy();
                var origXt = (double)x[i];

                xMovedInIthDirection[i] = (NDarray)(origXt + 2 * step);
                var fx2h = function(xMovedInIthDirection);

                xMovedInIthDirection[i] = (NDarray)(origXt + step);
                var fxh = function(xMovedInIthDirection);

                var fx = function(x);

                var gradientNum = fx2h - 2 * fxh + fx;
                gradientNum /= step * step;

                gradient2ndOrder[i] = (NDarray)gradientNum;
            }

            return gradient2ndOrder;
        }

        public static NDarray GradientTask(UserInput userInput)
        {
            return np.add(np.dot(userInput.A.T + userInput.A, userInput.X0), userInput.B);
        }

        public static NDarray Gradient2ndOrderTaskUserInput(UserInput userInput)
        {
            return np.linalg.inv(np.add(userInput.A, userInput.A.T));
        }
    }
}
