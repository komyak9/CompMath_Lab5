using SplineInterpolationLibrary;
using System;

namespace Lab_5
{
    internal class FunctionTwo : Function
    {
        public FunctionTwo()
        {
            points[0] = new Point
            {
                X = -5,
                Y = -1
            };
            points[1] = new Point
            {
                X = 5,
                Y = 3
            };
        }

        public override double CalculateDerivative(double x, double y) => Math.Sin(x) * 2 * y;

        public override double CalculateValue(double x)
        {
            if (chosenPoint == 0)
                return -(Math.Pow(Math.E, 2 * (Math.Cos(5) - Math.Cos(x))));
            else if (chosenPoint == 1)
                return 3 * Math.Pow(Math.E, 2 * (Math.Cos(5) - Math.Cos(x)));
            else
                throw new Exception("Incorrect point choice;");
        }

        public override string ToString() => "y' = sin(x) * 2y";
    }
}
