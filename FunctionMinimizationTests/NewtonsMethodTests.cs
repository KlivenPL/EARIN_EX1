using FunctionMinimization;
using FunctionMinimization.Minimization;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization.Minimization.SimpleGradientDescent;
using FunctionMinimization.UserInputs;
using Numpy;
using System;
using Xunit;

namespace FunctionMinimizationTests
{
    public class NewtonsMethodTests
    {

        private readonly UserInputGetter userInputGetter;

        public NewtonsMethodTests()
        {
            userInputGetter = new UserInputGetter();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(50, 50)]
        [InlineData(-50, 50)]
        [InlineData(50, -50)]
        [InlineData(-50, -50)]
        public void SampleNewtonsMethodNumTest(double startX, double startY)
        {
            var data = $"NewtonsNum 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} -1";
            var userInput = ParseAndValidateInput(data);

            var result = new NewtonsMethodNumeric().Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.XStar, np.array(new[] { -0.5, 0 }), 0.001));
        }

        [Theory]
        [InlineData(50, -50)]
        public void SampleNewtonsMethodTaskTest(double startX, double startY)
        {
            var data = $"Newtons 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} -1";
            var userInput = ParseAndValidateInput(data);

            var result = new NewtonsMethodTask(userInput).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.XStar, np.array(new[] { -0.5, 0 }), 0.001));
        }

        [Theory]
        [InlineData(50, -50)]
        public void SampleSimpleGradientDescentMethodTaskTest(double startX, double startY)
        {
            //var data = $"SimpleGradient 1.0 0.5 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} -1";
            // var userInput = ParseAndValidateInput(data);

            UserInput userInput = new UserInput
            {
                A = new NDarray(new double[][] { new double[] { 1, 0 }, new double[] { 0, 1 } }),
                B = new NDarray(new double[] { 1, 0 }),
                Beta = 0.5,
                C = 1.0,
                MinimizationMethodType = MinimizationMethodType.SimpleGradient,
                X0 = new NDarray(new double[] { startX, startY })
            };

            var result = new SimpleGradientDescentMethodTask(userInput).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.XStar, np.array(new[] { -0.5, 0 }), 0.001));
        }

        private UserInput ParseAndValidateInput(string str)
        {
            var userInput = userInputGetter.ParseUserInput(str.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            new UserInputValidator(userInput).Validate();

            return userInput;
        }

        private bool CheckIfResultInDeltaRange(NDarray result, NDarray expectedResult, double deltaRange)
        {
            return np.all(np.abs(result - expectedResult) <= deltaRange);
        }
    }
}
