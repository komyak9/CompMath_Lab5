using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class FunctionOne : Function
    {
        public override double CalculateFirstDerivative(double x, double y) => x * y;

        public override double CalculateValue(double x) => Math.Pow(Math.E, Math.Pow(x, 2) / 2);

        public override string ToString() => "y' = x * y\ny(0) = 1";
    }
}
