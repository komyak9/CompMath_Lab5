using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplineInterpolationLibrary;

namespace Lab_5
{
    internal class FunctionOne : Function
    {
        public FunctionOne()
        {
            points[0] = new Point
            {
                X = 0,
                Y = 1
            };
            points[1] = new Point
            {
                X = 1,
                Y = 1
            };
        }

        public override double CalculateDerivative(double x, double y) => x * y;

        public override double CalculateValue(double x)
        {
            if (chosenPoint == 0)
                return Math.Pow(Math.E, Math.Pow(x, 2) / 2);
            else if (chosenPoint == 1)
                return Math.Pow(Math.E, (Math.Pow(x, 2) - 1) / 2);
            else
                throw new Exception("Incorrect point choice;");
        }

        public override string ToString() => "y' = x * y";
    }
}
