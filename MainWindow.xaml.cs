using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab_5
{
    public partial class MainWindow : Window
    {
        private readonly Function[] functions = { new FunctionOne() };
        private Function function = null;

        public MainWindow()
        {
            InitializeComponent();

            Graph.Plot.Title("Advanced Euler method");
            Graph.Plot.XLabel("x");
            Graph.Plot.YLabel("y");
            Graph.Plot.Legend();
            Graph.Plot.AxisScaleLock(true);

            for (int i = 0; i < functions.Length; i++)
            {
                var rb = new RadioButton { GroupName = "FunctionRadioButton", Name = 'f' + (i + 1).ToString(), Content = functions[i] };
                rb.Checked += RadioButton_Checked;
                Functions.Children.Add(rb);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Point.Children.Clear();
            function = GetFunction();

            for (int i = 0; i < function.Points.Length; i++)
            {
                Point.Children.Add(new RadioButton { GroupName = "PointsList", Name = 'p' + i.ToString(), Content = $"({function.Points[i].X}, {function.Points[i].Y})" });
            }
        }

        private void CalcButtonClick(object sender, RoutedEventArgs e)
        {
            Calculator calculator;
            Graph.Plot.Clear();

            Function function;
            double x0, y0, xn;
            uint pointsCount;
            try
            {
                x0 = 0;
                y0 = 1;
                xn = ReadXn();
                pointsCount = ReadPointsCount();
                function = GetFunction();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            calculator = new Calculator(x0, y0, xn, pointsCount, function);
            calculator.Calculate();

            DrawPoints(calculator.OrigninalX, calculator.OrigninalY, System.Drawing.Color.CornflowerBlue, "Original", 1, 1);
            DrawPoints(calculator.InterpolatedX, calculator.InterpolatedY, System.Drawing.Color.Red, "Interpolated Euler method points", 1, 1);
        }

        private void DrawPoints(double[] dataX, double[] dataY, System.Drawing.Color color, string lbl, int markSize, int lineSize)
        {
            Graph.Plot.AddScatter(dataX, dataY, lineWidth: lineSize, color: color, markerSize: markSize, label: lbl);
            Graph.Refresh();
        }

        //private double ReadX0()
        //{
        //    double x0Value;
        //    try
        //    {
        //        x0Value = double.Parse(x0.Text);
        //    }
        //    catch
        //    {
        //        throw new Exception("Check input for 'x0'.");
        //    }
        //    return x0Value;
        //}

        //private double ReadY0()
        //{
        //    double y0Value;
        //    try
        //    {
        //        y0Value = double.Parse(y0.Text);
        //    }
        //    catch
        //    {
        //        throw new Exception("Check input for 'y0'.");
        //    }
        //    return y0Value;
        //}

        private double ReadXn()
        {
            double xnValue;
            try
            {
                xnValue = double.Parse(xn.Text);
            }
            catch
            {
                throw new Exception("Check input for upper border.");
            }
            return xnValue;
        }

        private uint ReadPointsCount()
        {
            uint pointsCount;
            try
            {
                pointsCount = uint.Parse(count.Text);
            }
            catch
            {
                throw new Exception("Check input for count of points.");
            }

            if (pointsCount < 2)
                throw new Exception("Count of points must be 2 or greater.");

            return pointsCount;
        }

        private Function GetFunction()
        {
            List<RadioButton> radioButtons = Functions.Children.OfType<RadioButton>().Where(r => r.GroupName == "FunctionRadioButton").ToList();
            RadioButton radioButton = null;
            foreach (var r in radioButtons)
                if (r.IsChecked == true) radioButton = r;

            if (radioButton == null)
                throw new Exception("Function must be chosen!");
            else
            {
                foreach (Function f in functions)
                    if (f.ToString() == radioButton.Content.ToString()) return f;
            }
            return null;
        }
    }
}
