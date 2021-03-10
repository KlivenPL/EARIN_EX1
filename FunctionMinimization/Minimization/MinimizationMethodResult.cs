using Numpy;
using System.Text;

namespace FunctionMinimization.Minimization
{
    public class MinimizationMethodResult
    {
        public NDarray XStar { get; set; }
        public double JofXStar { get; set; }
        public bool Timeout { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"X*: {XStar}");
            sb.AppendLine($"J(X*): {JofXStar}");

            return sb.ToString();
        }
    }
}
