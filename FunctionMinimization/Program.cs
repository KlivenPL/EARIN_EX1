using FunctionMinimization.Helpers;
using FunctionMinimization.Minimization;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization.Minimization.SimpleGradientDescent;
using FunctionMinimization.UserInputs;
using System;

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
            PrintLogo();

            var userInput = GetUserInput(args);
            ValidateUserInput(userInput);

            PrintUserInput(userInput);

            var excerciseData = new ExcerciseData(userInput);
            var minimizationMethod = MapMinimizationMethod(userInput);

            var result = minimizationMethod.Minimize(excerciseData.JFunction, userInput.X0);

            PrintResult(result);
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
                MinimizationMethodType.SimpleGradientNum => new SimpleGradientDescentMethodNumeric(0.01),
                MinimizationMethodType.SimpleGradient => new SimpleGradientDescentMethodTask(userInput),
                MinimizationMethodType.NewtonsNum => new NewtonsMethodNumeric(),
                MinimizationMethodType.Newtons => new NewtonsMethodTask(userInput),
                _ => throw new System.NotImplementedException(),
            };
        }

        private void PrintLogo()
        {
            Console.ForegroundColor = EnumHelper.PickRandom<ConsoleColor>(ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.DarkGray);
            Console.WriteLine(Logo);
            Console.WriteLine();
            Console.ResetColor();
        }

        private void PrintUserInput(UserInput userInput)
        {
            Console.WriteLine();
            Console.WriteLine("Given data:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(userInput.ToString());
            Console.ResetColor();
        }

        private void PrintResult(MinimizationMethodResult result)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Results");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(result.ToString());
            Console.ResetColor();
        }

        const string Logo = "+-+-+-+-+-+-+-+-+-+-+\r\n|E|A|R|I|N| |E|X| |1|\r\n+-+-+-+-+-+-+-+-+-+-+\r\nOskar H\u0105cel\r\nMarcin Lisowski\r\nPW, 2021\r\n+-+-+-+-+-+-+-+-+-+-+";
    }
}
