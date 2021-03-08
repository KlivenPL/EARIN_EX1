using FunctionMinimization.Minimization;
using Numpy;

namespace FunctionMinimization.UserInputs
{
    public class UserInput
    {
        public MinimizationMethodType MinimizationMethodType { get; set; }
        public bool BatchMode { get; set; }
        public double C { get; set; }
        public NDarray B { get; set; }
        public NDarray A { get; set; }
        public NDarray X0 { get; set; }
    }
}
