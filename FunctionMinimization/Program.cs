using FunctionMinimization.Minimization;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization.Minimization.SimpleGradientDescent;
using FunctionMinimization.UserInputs;

namespace FunctionMinimization
{
    class Program
    {
        private readonly UserInputGetter userInputGetter;

        public Program()
        {
            userInputGetter = new UserInputGetter();
        }

        static void Main(string[] args)
        {
            new Program().Start(args);
        }

        private void Start(string[] args)
        {
            var userInput = GetUserInput(args);
            ValidateUserInput(userInput);

            var excerciseData = new ExcerciseData(userInput);
            var minimizationMethod = MapMinimizationMethod(userInput);

            var result = minimizationMethod.Minimize(excerciseData.JFunction, userInput.X0);
        }

        private UserInput GetUserInput(string[] args)
        {
            return userInputGetter.ParseUserInput(args);
        }

        private void ValidateUserInput(UserInput userInput)
        {
            var userInputValidator = new UserInputValidator(userInput);
            userInputValidator.Validate();
        }

        private MinimizationMethod MapMinimizationMethod(UserInput userInput)
        {
            return userInput.MinimizationMethodType switch
            {
                MinimizationMethodType.SimpleGradient => new SimpleGradientDescentMethod(),
                MinimizationMethodType.Newtons => new NewtonsMethod(),
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}
