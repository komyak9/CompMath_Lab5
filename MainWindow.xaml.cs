using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        }
    }
}
