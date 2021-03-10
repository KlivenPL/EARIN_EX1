using FunctionMinimization.Helpers;
using FunctionMinimization.Minimization;
using FunctionMinimization.Minimization.Newton;
using FunctionMinimization.Minimization.SimpleGradientDescent;
using FunctionMinimization.UserInputs;
using Numpy;
using System;
using System.Collections.Generic;

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
            PrintProgramSettings();

            var userInput = GetUserInput(args);
            ValidateUserInput(userInput);

            PrintUserInput(userInput);

            var excerciseData = new ExcerciseData(userInput);
            var minimizationMethod = MapMinimizationMethod(userInput);

            var jOfExes = new List<double>();

            for (int i = 0; i < userInput.BatchModeN; i++)
            {
                var result = minimizationMethod.Minimize(excerciseData.JFunction, userInput.X0);
                jOfExes.Add(result.JofXStar);

                PrintResult(result, userInput.X0);

                userInput.X0 = userInput.GenerateX0();
            }

            if (userInput.BatchModeN > 1)
            {
                PrintBatchModeResults(jOfExes);
            }
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
                MinimizationMethodType.SimpleGradientNum => new SimpleGradientDescentMethodNumeric(userInput.Beta.Value, userInput.DesiredJOfX),
                MinimizationMethodType.SimpleGradient => new SimpleGradientDescentMethodTask(userInput),
                MinimizationMethodType.NewtonsNum => new NewtonsMethodNumeric(userInput.DesiredJOfX),
                MinimizationMethodType.Newtons => new NewtonsMethodTask(userInput),
                _ => throw new NotImplementedException(),
            };
        }

        private void PrintLogo()
        {
            Console.ForegroundColor = EnumHelper.PickRandom<ConsoleColor>(ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.DarkGray);
            Console.WriteLine(Logo);
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

        private void PrintProgramSettings()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Program settings:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Timeout after: {AppConfig.MaxExecutionTimeInMs}ms");
            Console.WriteLine($"Desired precision: {AppConfig.DesiredPrecision}");
            Console.WriteLine();
            Console.ResetColor();
        }

        private void PrintResult(MinimizationMethodResult result, NDarray x0)
        {
            Console.WriteLine();

            if (!result.Timeout)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Results for x0 = {x0}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Results for x0 = {x0} (Timed out)");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(result.ToString());
            Console.ResetColor();
        }

        private void PrintBatchModeResults(IEnumerable<double> jOfExes)
        {
            var standardDeviation = MathHelper.StandardDeviation(jOfExes);
            var meanValue = MathHelper.MeanValue(jOfExes);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Batch mode results");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Standard deviation: {standardDeviation}");
            Console.WriteLine($"Mean value: {meanValue}");
        }

        const string Logo = "+-+-+-+-+-+-+-+-+-+-+\r\n|E|A|R|I|N| |E|X| |1|\r\n+-+-+-+-+-+-+-+-+-+-+\r\nOskar H\u0105cel\r\nMarcin Lisowski\r\nPW, 2021\r\n+-+-+-+-+-+-+-+-+-+-+";
    }
}
