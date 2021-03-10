using Numpy;
using System;
using System.Diagnostics;

namespace FunctionMinimization.Minimization
{
    public abstract class MinimizationMethod
    {
        public abstract MinimizationMethodResult Minimize(Func<NDarray, double> function, NDarray x0);
        protected double TestFunction(NDarray x)
        {
            return (double)(x[0] * x[0] * x[0] * x[0] + x[1] * x[1] * x[1] * x[1] - x[0] * x[1] + 2 * x[2] * x[2] + 2);
        }

        protected bool Execute(Func<double> func, double desiredValue)
        {
            var maxTime = AppConfig.MaxExecutionTimeInMs;
            var precision = AppConfig.DesiredPrecision;

            var sw = new Stopwatch();
            sw.Start();

            double value;

            do
            {
                value = func();
            } while (Math.Abs(value - desiredValue) > precision && sw.ElapsedMilliseconds <= maxTime);

            sw.Stop();

            return sw.ElapsedMilliseconds <= maxTime;
        }
    }
}
