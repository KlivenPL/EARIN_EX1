using FunctionMinimization.UserInputs;
using Numpy;

namespace FunctionMinimization
{
    public class ExcerciseData
    {
        private readonly UserInput userInput;
        private readonly NDarray bTranspose;

        public ExcerciseData(UserInput userInput)
        {
            this.userInput = userInput;
            bTranspose = np.transpose(new[] { userInput.B });
        }

        public double JFunction(NDarray x)
        {
            var xTranspose = np.transpose(new[] { x });
            var bTx = np.dot(x, bTranspose);
            var xTA = np.dot(userInput.A, xTranspose);
            var xTAx = np.dot(x, xTA);

            var func = userInput.C + bTx + xTAx;
            return (double)func;
        }
    }
}
