using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingCraftHOMod1Ex6Dapper.Extension
{
    public static class MathExtension
    {
        public static double StandardDeviation(this IEnumerable<double> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}