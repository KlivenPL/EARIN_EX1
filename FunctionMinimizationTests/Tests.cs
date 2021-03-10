using FunctionMinimization;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization.Minimization.SimpleGradientDescent;
using FunctionMinimization.UserInputs;
using System;
using Xunit;

namespace FunctionMinimizationTests
{
    public class Tests
    {

        private readonly UserInputGetter userInputGetter;

        public Tests()
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
        [InlineData(-500, 500)]
        public void SampleNewtonsMethodNumTest(double startX, double startY)
        {
            var data = $"NewtonsNum 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} 0.75 -1";
            var userInput = ParseAndValidateInput(data);

            var result = new NewtonsMethodNumeric(userInput.DesiredJOfX).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.JofXStar, userInput.DesiredJOfX, AppConfig.DesiredPrecision));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(50, 50)]
        [InlineData(-50, 50)]
        [InlineData(50, -50)]
        [InlineData(-50, -50)]
        [InlineData(-500, 500)]
        public void SampleNewtonsMethodTaskTest(double startX, double startY)
        {
            var data = $"Newtons 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} 0.75 -1";
            var userInput = ParseAndValidateInput(data);

            var result = new NewtonsMethodTask(userInput).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.JofXStar, userInput.DesiredJOfX, AppConfig.DesiredPrecision));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(50, 50)]
        [InlineData(-50, 50)]
        [InlineData(50, -50)]
        [InlineData(-50, -50)]
        [InlineData(-500, 500)]
        public void SampleSimpleGradientDescentMethodNumTest(double startX, double startY)
        {
            var data = $"SimpleGradientNum 0.01 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} 0.75 -1";
            var userInput = ParseAndValidateInput(data);

            var result = new SimpleGradientDescentMethodNumeric(userInput.Beta.Value, userInput.DesiredJOfX).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.JofXStar, userInput.DesiredJOfX, AppConfig.DesiredPrecision));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(50, 50)]
        [InlineData(-50, 50)]
        [InlineData(50, -50)]
        [InlineData(-50, -50)]
        [InlineData(-500, 500)]
        public void SampleSimpleGradientDescentMethodTaskTest(double startX, double startY)
        {
            var data = $"SimpleGradient 0.01 1.0 1.0,0.0 1.0,0.0;0.0,1.0 {startX},{startY} 0.75 -1";
            var userInput = ParseAndValidateInput(data);

            var result = new SimpleGradientDescentMethodTask(userInput).Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            Assert.True(CheckIfResultInDeltaRange(result.JofXStar, userInput.DesiredJOfX, AppConfig.DesiredPrecision));
        }

        private UserInput ParseAndValidateInput(string str)
        {
            var userInput = userInputGetter.ParseUserInput(str.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            new UserInputValidator(userInput).Validate();

            return userInput;
        }

        private bool CheckIfResultInDeltaRange(double result, double expectedResult, double deltaRange)
        {
            return Math.Abs(result - expectedResult) <= deltaRange;
        }
    }
}
