using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal abstract class Function
    {
        public abstract double CalculateValue(double x);

        public abstract double CalculateFirstDerivative(double x, double y);
    }
}
