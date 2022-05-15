using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Calculator
    {
        AdvancedEulerMethod advancedEulerMethod;
        readonly Function function;
        readonly double x0, y0, xn;

        double[] originalXPoints, originalYPoints;
        double[] xPoints, yPoints;

        public Calculator(double x0, double y0, double xn, uint count, Function function)
        {
            this.function = function;
            this.x0 = x0;
            this.y0 = y0;
            this.xn = xn;
            xPoints = new double[count];

            FillXValues();
        }

        public void Calculate()
        {
            advancedEulerMethod = new AdvancedEulerMethod(xPoints, y0, function);
            yPoints = advancedEulerMethod.CalculateYValues();
        }

        private void FillXValues()
        {
            double h = (double)(xn - x0) / (xPoints.Length - 1);
            for (int i = 0; i < xPoints.Length; i++)
            {
                xPoints[i] = x0 + i * h;
            }
        }

        public double[] OrigninalX
        {
            get
            {
                List<double> origX = new List<double>();
                for (double i = x0; i < xn; i += 0.01)
                    origX.Add(i);

                originalXPoints = origX.ToArray();
                return originalXPoints;
            }
        }

        public double[] OrigninalY
        {
            get
            {
                originalYPoints = new double[originalXPoints.Length];
                for (int i = 0; i < originalYPoints.Length; i++)
                    originalYPoints[i] = function.CalculateValue(originalXPoints[i]);

                return originalYPoints;
            }
        }
    }
}
