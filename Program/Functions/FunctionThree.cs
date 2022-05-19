using SplineInterpolationLibrary;
using System;

namespace Lab_5
{
    internal class FunctionThree : Function
    {
        public FunctionThree()
        {
            points[0] = new Point
            {
                X = 3,
                Y = -1
            };
            points[1] = new Point
            {
                X = -4,
                Y = 17
            };
        }

        public override double CalculateDerivative(double x, double y) => 1 - Math.Sin(x);

        public override double CalculateValue(double x)
        {
            if (chosenPoint == 0)
                return x + Math.Cos(x) - 4 - Math.Cos(3);
            else if (chosenPoint == 1)
                return x + Math.Cos(x) + 21 - Math.Cos(4);
            else
                throw new Exception("Incorrect point choice;");
        }

        public override string ToString() => "y' = 1 - sin(x)";
    }
}
