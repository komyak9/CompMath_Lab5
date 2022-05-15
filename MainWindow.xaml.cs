using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab_5
{
    public partial class MainWindow : Window
    {
        private readonly Function[] functions = { new FunctionOne() };
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
                Functions.Children.Add(new RadioButton { GroupName = "FunctionRadioButton", Name = 'f' + (i + 1).ToString(), Content = functions[i] });
            }
        }

        private void CalcButtonClick(object sender, RoutedEventArgs e)
        {
            Graph.Plot.Clear();

            double x0, y0, xn;
            uint pointsCount;

            try
            {
                x0 = ReadX0();
                y0 = ReadY0();
                xn = ReadXn();
                pointsCount = ReadPointsCount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"x0: {x0}\ty0: {y0}\txn: {xn}\tcount: {pointsCount}");
        }

        private double ReadX0()
        {
            double x0Value;
            try
            {
                x0Value = double.Parse(x0.Text);
            }
            catch
            {
                throw new Exception("Check input for 'x0'.");
            }
            return x0Value;
        }

        private double ReadY0()
        {
            double y0Value;
            try
            {
                y0Value = double.Parse(y0.Text);
            }
            catch
            {
                throw new Exception("Check input for 'y0'.");
            }
            return y0Value;
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
    }
}
