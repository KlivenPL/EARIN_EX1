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
            bTranspose = userInput.B.transpose(0);
        }

        public double JFunction(NDarray x)
        {
            var func = userInput.C + bTranspose * x + x.transpose() * userInput.A * x;
            return (double)func;
        }
    }
}
