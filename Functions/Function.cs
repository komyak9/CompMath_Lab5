using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplineInterpolationLibrary;

namespace Lab_5
{
    internal abstract class Function
    {
        protected Point[] points = new Point[2];
        protected uint chosenPoint;

        public abstract double CalculateValue(double x);

        public abstract double CalculateDerivative(double x, double y);

        public uint ChosenPoint
        {
            set { chosenPoint = value; }
        }

        public Point[] Points
        {
            get { return points; }
        }


        public double X0 { get; } = 0;

        public double Y0 { get; } = 1;
    }
}
