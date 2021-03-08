using System;
using Xunit;
using FunctionMinimization.UserInputs;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization;

namespace FunctionMinimizationTests
{
    public class NewtonsMethodTests
    {

        private readonly UserInputGetter userInputGetter;

        public NewtonsMethodTests()
        {
            userInputGetter = new UserInputGetter();
        }

        [Fact]
        public void SampleNewtonsMethodTest1()
        {
            var data = "Newtons false 10.0 1.0,2.0 1.0,0.0;0.0,1.0 -5.0 5.0";
            var userInput = userInputGetter.ParseUserInput(data.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            new UserInputValidator(userInput).Validate();

            var result = new NewtonsMethod().Minimize(new ExcerciseData(userInput).JFunction, userInput.X0);

            var x = 1;
        }
    }
}
