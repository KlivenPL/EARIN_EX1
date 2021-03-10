using FunctionMinimization.Minimization;
using Numpy;
using System;
using System.Linq;

namespace FunctionMinimization.UserInputs
{
    public class UserInputGetter
    {
        private string MethodTypeDescription => $"Enter method type: {string.Join(", ", Enum.GetNames(typeof(MinimizationMethodType)))}";
        private const string BetaDescription = "Enter beta, like 0.01 or 1";
        private const string CDescription = "Enter C, like 1.23 or 911";
        private const string BDescription = "Enter B, like 1.1, 2, 3.5, 4, 5";
        private const string ADescription = "Enter A, like 1, 0; 0, 1";
        private const string LDescription = "Enter l, like 1.23 or 911";
        private const string UDescription = "Enter u, like 1.23 or 911. Should be greater than l";
        private const string X0Description = "Enter X0, like 1.1, 2, 3.5, 4, 5";
        private const string X0HasToBeGeneratedDescription = "Specify if X0 has to be generated from [l, u], by typing: true or false";
        private const string DesiredJofXDescription = "Enter desired J(X) value to reach";
        private const string BatchModeDescription = "Enter n for batch mode (press enter for non-batch mode)";

        public UserInput ParseUserInput(params string[] args)
        {
            if (args?.Any() != true)
            {
                return GetUserInput();
            }

            if (args.Length >= 7)
            {
                return ParseUserInputFromArgs(args);
            }
            else
            {
                throw Error(
                    $"Invalid parameters. Should be either{Environment.NewLine}" +
                    $"<MethodType> [beta (if grad. descent)] <C> <B> <A> <X0> <desired J(X)> <n>" +
                    $"{Environment.NewLine}or{Environment.NewLine}" +
                    $"<MethodType> [beta (if grad. descent)] <C> <B> <A> <l> <u> <desired J(X)> <n>" +
                    $"{Environment.NewLine}Examples:{Environment.NewLine}" +
                    $"FunctionMinimization NewtonsNum 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -50,50 0.75 1{Environment.NewLine}" +
                    $"FunctionMinimization SimpleGradient 0.01 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -100 100 0.75 10"
                );
            }
        }

        private UserInput GetUserInput()
        {
            var methodType = TryGetInput(MethodTypeDescription, ParseMethodType);

            double? beta = null;
            if (methodType == MinimizationMethodType.SimpleGradient || methodType == MinimizationMethodType.SimpleGradientNum)
            {
                beta = TryGetInput(BetaDescription, ParseBeta);
            }

            var C = TryGetInput(CDescription, ParseC);
            var B = TryGetInput(BDescription, ParseB);
            var A = TryGetInput(ADescription, ParseA);

            var generateX0 = TryGetInput(X0HasToBeGeneratedDescription, ParseIfX0HasToBeGenerated);

            NDarray X0 = null;
            double? l = null, u = null;

            if (generateX0)
            {
                l = TryGetInput(LDescription, ParseL);
                u = TryGetInput(UDescription, (str) => ParseU(str, l.Value));
            }
            else
            {
                X0 = TryGetInput(X0Description, ParseX0);
            }

            var desiredJofX = TryGetInput(DesiredJofXDescription, ParseDesiredJOfX);
            var batchModeN = TryGetInput(BatchModeDescription, ParseBatchModeN);

            return new UserInput
            {
                MinimizationMethodType = methodType,
                BatchModeN = batchModeN,
                Beta = beta,
                C = C,
                B = B,
                A = A,
                X0 = X0,
                DesiredJOfX = desiredJofX,
                L = l,
                U = u
            };
        }

        private T TryGetInput<T>(string description, Func<string, T> inputGetter)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(description);
                Console.ForegroundColor = ConsoleColor.White;
                var input = Console.ReadLine();
                try
                {
                    Console.ResetColor();
                    return inputGetter(input);
                }
                catch (UserInputException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
        }

        private UserInput ParseUserInputFromArgs(string[] args)
        {
            int n = 0;

            var methodType = ParseMethodType(args[n++]);

            double? beta = null;
            if (methodType == MinimizationMethodType.SimpleGradient || methodType == MinimizationMethodType.SimpleGradientNum)
            {
                beta = ParseBeta(args[n++]);
            }

            var C = ParseC(args[n++]);
            var B = ParseB(args[n++]);
            var A = ParseA(args[n++]);

            NDarray X0 = null;

            int argsCountForLU = 0, argsCountForX0 = 0;
            double? l = null, u = null;

            if (methodType == MinimizationMethodType.SimpleGradient || methodType == MinimizationMethodType.SimpleGradientNum)
            {
                argsCountForLU = 9;
                argsCountForX0 = 8;
            }
            else
            {
                argsCountForLU = 8;
                argsCountForX0 = 7;
            }

            if (args.Length == argsCountForX0)
            {
                X0 = ParseX0(args[n++]);
            }
            else if (args.Length == argsCountForLU)
            {
                l = ParseL(args[n++]);
                u = ParseU(args[n++], l.Value);
            }

            var desiredJofX = ParseDesiredJOfX(args[n++]);

            var batchMode = ParseBatchModeN(args[n++]);

            return new UserInput
            {
                MinimizationMethodType = methodType,
                BatchModeN = batchMode,
                Beta = beta,
                C = C,
                B = B,
                A = A,
                X0 = X0,
                DesiredJOfX = desiredJofX,
                L = l,
                U = u,
            };
        }

        private bool ParseIfX0HasToBeGenerated(string str)
        {
            if (bool.TryParse(str, out bool generateX0))
            {
                return generateX0;
            }

            throw Error($"Invalid input: {str}. Specify if X0 has to be generated from [l, u], by typing: \"true\" or \"false\".");
        }

        private MinimizationMethodType ParseMethodType(string str)
        {
            if (Enum.TryParse(typeof(MinimizationMethodType), str, true, out object methodType))
            {
                return (MinimizationMethodType)methodType;
            }

            throw Error($"Invalid minimization method type was given: {str}. Method type should be one of following: \"Newtons\" or \"SimpleGradient\".");
        }

        private int ParseBatchModeN(string str)
        {
            if (int.TryParse(str, out int batchMode) && batchMode > 1)
            {
                return batchMode;
            }

            return 1;
        }

        private double ParseBeta(string str)
        {
            if (double.TryParse(str, out double beta) && beta > 0)
            {
                return beta;
            }

            throw Error($"Invalid beta was given: {str}. Beta should be a positive double-precision floating point number.");
        }

        private double ParseDesiredJOfX(string str)
        {
            if (double.TryParse(str, out double desiredJOfX))
            {
                return desiredJOfX;
            }

            throw Error($"Invalid desired J(X) values: {str}. Desired J(X) should be a double-precision floating point number.");
        }

        private double ParseC(string str)
        {
            if (double.TryParse(str, out double C))
            {
                return C;
            }

            throw Error($"Invalid C was given: {str}. C should be a double-precision floating point number.");
        }

        private NDarray ParseB(string str)
        {
            try
            {
                return Parse1DArray(str);
            }
            catch
            {
                throw Error($"Invalid B was given: {str}. B should be a vector, like 1.1, 2, 3.5, 4, 5");
            }
        }

        private NDarray ParseA(string str)
        {
            try
            {
                var split = str.Split(';', System.StringSplitOptions.RemoveEmptyEntries);

                if (split?.Any() != true)
                {
                    throw Error("");
                }

                var arrays = split.Select(spl => Parse1DArray(spl)).ToArray();

                return np.stack(arrays);
            }
            catch
            {
                throw Error($"Invalid A was given: {str}. A should be a matrix, like 1, 0; 0, 1");
            }
        }

        private double ParseL(string str)
        {
            if (double.TryParse(str, out double l))
            {
                return l;
            }

            throw Error($"Invalid l was given: {str}. L should be double-precision floating point number, like -4 or 7.53");
        }

        private double ParseU(string str, double l)
        {
            if (double.TryParse(str, out double u) && u >= l)
            {
                return u;
            }

            throw Error($"Invalid u was given: {str}. U should be double-precision floating point number, like -4 or 7.53, and should be greater or equal to l={l}");
        }

        private NDarray ParseX0(string str)
        {
            try
            {
                return Parse1DArray(str);
            }
            catch
            {
                throw Error($"Invalid X0 was given: {str}. X0 should be a vector, like 1.1, 2, 3.5, 4, 5");
            }
        }

        private NDarray Parse1DArray(string str)
        {
            var ndArray = (NDarray)str.Split(',').Select(n => double.Parse(n)).ToArray();
            return ndArray;
        }

        private UserInputException Error(string message) => new UserInputException(message);
    }
}
