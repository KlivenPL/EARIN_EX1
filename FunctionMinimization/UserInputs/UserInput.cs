using FunctionMinimization.Helpers;
using FunctionMinimization.Minimization;
using Numpy;
using System;
using System.Text;

namespace FunctionMinimization.UserInputs
{
    public class UserInput
    {
        private readonly Random random = new Random();
        private NDarray x0;

        public MinimizationMethodType MinimizationMethodType { get; set; }
        public int BatchModeN { get; set; }
        public double? Beta { get; set; }
        public double C { get; set; }
        public NDarray B { get; set; }
        public NDarray A { get; set; }
        public NDarray X0
        {
            get
            {
                if (x0 == null)
                {
                    x0 = GenerateX0();
                }

                return x0;
            }

            set
            {
                x0 = value;
            }
        }
        public double DesiredJOfX { get; set; }

        public double? L { get; set; }
        public double? U { get; set; }

        public NDarray GenerateX0()
        {
            if (L == null || U == null)
            {
                return x0 ?? null;
            }

            var size = B.size;
            var arr = np.zeros(size);

            for (int i = 0; i < size; i++)
            {
                arr[i] = (NDarray)random.NextDouble() * (U.Value - L.Value) + L.Value;
            }

            return arr;
        }

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

            if (L != null && U != null)
            {
                sb.AppendLine($"l: {L}");
                sb.AppendLine($"u: {U}");
            }

            sb.AppendLine($"X0: {X0}");
            sb.AppendLine($"Desired J(X): {DesiredJOfX}");

            return sb.ToString();
        }
    }
}
