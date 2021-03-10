using FunctionMinimization.Helpers;
using FunctionMinimization.Minimization;
using Numpy;
using System.Text;

namespace FunctionMinimization.UserInputs
{
    public class UserInput
    {
        public MinimizationMethodType MinimizationMethodType { get; set; }
        public int BatchModeN { get; set; }
        public double? Beta { get; set; }
        public double C { get; set; }
        public NDarray B { get; set; }
        public NDarray A { get; set; }
        public NDarray X0 { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Minimization method: {MinimizationMethodType.GetDescription()}");
            sb.AppendLine($"Batch mode: " + (BatchModeN == 1 ? "false" : $"n = {BatchModeN}"));

            if (Beta != null)
            {
                sb.AppendLine($"Beta: {Beta}");
            }

            sb.AppendLine($"C: {C}");
            sb.AppendLine($"B: {B}");
            sb.AppendLine($"A");
            sb.AppendLine($"{A}");
            sb.AppendLine($"X0: {X0}");

            return sb.ToString();
        }
    }
}
