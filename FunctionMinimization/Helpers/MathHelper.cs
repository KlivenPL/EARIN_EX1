using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionMinimization.Helpers
{
    public static class MathHelper
    {
        public static double StandardDeviation(IEnumerable<double> values)
        {
            double sd = 0;

            if (values.Any())
            {
                double avg = values.Average();
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                sd = Math.Sqrt(sum / (values.Count() - 1));
            }

            return sd;
        }

        public static double MeanValue(IEnumerable<double> values)
        {
            return values.Sum() / values.Count();
        }
    }
}
