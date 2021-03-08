using Numpy;
using System;

namespace FunctionMinimization.UserInputs
{
    public class UserInputValidator
    {
        private readonly UserInput userInput;

        public UserInputValidator(UserInput userInput)
        {
            this.userInput = userInput;
        }

        public void Validate()
        {
            try
            {
                ValidateIsBVector();
                ValidateAMatrixSize();
                ValidateIfAIsPositiveDefinite();
                ValidateX0Size();
            }
            catch (UserInputException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private void ValidateIsBVector()
        {
            if (userInput.B.ndim != 1)
            {
                Error($"Invalid vector B dimension");
            }
        }

        private void ValidateAMatrixSize()
        {
            var A = userInput.A;
            var B = userInput.B;

            if (A.shape.Dimensions.Length != 2 || A.shape[0] != B.size || A.shape[1] != B.size)
            {
                Error($"Invalid matrix A dimensions. For d={B.size}, A should be a {B.size}x{B.size} matrix");
            }
        }

        private void ValidateIfAIsPositiveDefinite()
        {
            if (!np.all(np.linalg.eigvals(userInput.A) > 0))
            {
                Error("Given matrix A is not positive-definite");
            }
        }

        private void ValidateX0Size()
        {
            if (userInput.X0.size != userInput.B.size)
            {
                Error($"Invalid vector X0 size. X0 should be of length d={userInput.B.size}");
            }
        }

        private void Error(string message)
        {
            throw new UserInputException(message);
        }
    }
}
