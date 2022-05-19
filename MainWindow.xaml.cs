using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab_5
{
    public partial class MainWindow : Window
    {
        private readonly Function[] functions = { new FunctionOne(), new FunctionTwo(), new FunctionThree() };
        private Function function = null;
        private Calculator calculator = null;

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
            Graph.Plot.Clear();

            double xn;
            uint pointsCount;
            try
            {
                if (function == null)
                    throw new Exception("Any function must be chosen.");

                function.ChosenPoint = ReadPointNumber();
                xn = ReadXn();
                pointsCount = ReadPointsCount();

                if (xn <= function.Points[function.ChosenPoint].X)
                    throw new Exception("Upper border can't be lower or same as lower one.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            calculator = new Calculator(function.Points[function.ChosenPoint].X, function.Points[function.ChosenPoint].Y, xn, pointsCount, function);
            calculator.Calculate();

            DrawPoints(calculator.OrigninalX, calculator.OrigninalY, System.Drawing.Color.CornflowerBlue, "Original", 1, 1);
            DrawPoints(calculator.InterpolatedX, calculator.InterpolatedY, System.Drawing.Color.Red, "Interpolated Euler method points", 1, 1);

            pointCalculator.Visibility = Visibility.Visible;
        }

        private void DrawPoints(double[] dataX, double[] dataY, System.Drawing.Color color, string lbl, int markSize, int lineSize)
        {
            Graph.Plot.AddScatter(dataX, dataY, lineWidth: lineSize, color: color, markerSize: markSize, label: lbl);
            Graph.Refresh();
        }

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

        private uint ReadPointNumber()
        {
            RadioButton radioButton = Point.Children.OfType<RadioButton>().Where(r => r.IsChecked == true).FirstOrDefault();
            if (radioButton != null)
            {
                string name = radioButton.Name;
                char n = name[name.Length - 1];
                return Convert.ToUInt32(n) - 48;
            }
            else
                throw new Exception("Point must be chosen!");
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
                throw new Exception("Check input for count of points. No negative values accepted.");
            }

            if (pointsCount < 2)
                throw new Exception("Count of points must be 2 or greater.");

            return pointsCount;
        }

        private Function GetFunction()
        {
            RadioButton radioButton = Functions.Children.OfType<RadioButton>().Where(r => r.IsChecked == true).First();

            foreach (Function f in functions)
                if (f.ToString() == radioButton.Content.ToString())
                    return f;

            return null;
        }

        private void CalculateY_TextChanged(object sender, RoutedEventArgs e)
        {

            if (x.Text == "-" || x.Text == "\u2408" || x.Text == string.Empty)
                return;

            double xValue, originalY, interpolatedY;
            try
            {
                xValue = double.Parse(x.Text);
                if (xValue < calculator.X0 || xValue > calculator.Xn)
                    throw new Exception("X must be in range [x0, xn].");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            originalY = calculator.CalculateOriginalY(xValue);
            interpolatedY = calculator.CalculateInterpolatedY(xValue);
            yOriginal.Text = originalY.ToString();
            yInterpolated.Text = interpolatedY.ToString();
            error.Text = Math.Abs(originalY - interpolatedY).ToString();

            Graph.Plot.AddPoint(xValue, originalY, color: System.Drawing.Color.CornflowerBlue, size: 7);
            Graph.Plot.AddPoint(xValue, interpolatedY, color: System.Drawing.Color.Red, size: 7);
            Graph.Refresh();
        }
    }
}
