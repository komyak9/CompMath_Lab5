using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplineInterpolationLibrary;

namespace Lab_5
{
    internal class Calculator
    {
        SplineInterpolation interpolation;
        AdvancedEulerMethod advancedEulerMethod;
        readonly Function function;
        readonly double x0, y0, xn;

        double[] originalX, originalY;
        double[] calculatedX, calculatedY;

        public Calculator(double x0, double y0, double xn, uint count, Function function)
        {
            this.function = function;
            this.x0 = x0;
            this.y0 = y0;
            this.xn = xn;
            calculatedX = new double[count];

            FillXValues();
        }

        public void Calculate()
        {
            advancedEulerMethod = new AdvancedEulerMethod(calculatedX, y0, function);
            calculatedY = advancedEulerMethod.CalculateYValues();

            interpolation = new SplineInterpolation(CreatePoints());
            interpolation.Calculate();
        }

        private void FillXValues()
        {
            double h = (double)(xn - x0) / (calculatedX.Length - 1);
            for (int i = 0; i < calculatedX.Length; i++)
            {
                calculatedX[i] = x0 + i * h;
            }
        }

        public double[] OrigninalX
        {
            get
            {
                List<double> origX = new List<double>();
                for (double i = x0; i < xn; i += 0.01)
                    origX.Add(i);

                originalX = origX.ToArray();
                return originalX;
            }
        }

        public double[] OrigninalY
        {
            get
            {
                originalY = new double[originalX.Length];
                for (int i = 0; i < originalY.Length; i++)
                    originalY[i] = function.CalculateValue(originalX[i]);

                return originalY;
            }
        }

        public double[] InterpolatedX
        {
            get { return interpolation.interpolatedX; }
        }

        public double[] InterpolatedY
        {
            get { return interpolation.interpolatedY; }
        }

        private Point[] CreatePoints()
        {
            Point[] points = new Point[calculatedX.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point
                {
                    X = calculatedX[i],
                    Y = calculatedY[i]
                };
            }
            return points;
        }
    }
}
