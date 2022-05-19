using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class AdvancedEulerMethod
    {
        readonly Function function;
        readonly double[] x, y;
        readonly double h;

        public AdvancedEulerMethod(double[] x, double y0, Function function)
        {
            h = x[x.Length - 1] - x[x.Length - 2];
            this.function = function;
            this.x = x;

            y = new double[x.Length];
            y[0] = y0;
        }

        public double[] CalculateYValues()
        {
            double deltaY;
            for (int i = 0; i < y.Length - 1; i++)
            {
                deltaY = h * function.CalculateDerivative(x[i] + h / 2, y[i] + h * function.CalculateDerivative(x[i], y[i]) / 2);
                y[i + 1] = y[i] + deltaY;
            }
            return y;
        }
    }
}
