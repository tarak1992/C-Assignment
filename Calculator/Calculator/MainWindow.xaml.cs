using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = VM;
        }

        CalculatorVM VM = new CalculatorVM();

        private void Number_Clicked(object sender, RoutedEventArgs e)
        {
            
            VM.Op.StrOperand += ((Button)sender).Content.ToString();
        }

        private void Operator_Clicked(object sender, RoutedEventArgs e)
        {
            VM.Operations.Add(VM.Op);
            VM.Op = new BinaryOperation
            {
                PreviousTotal = VM.Op.Results,
                Operator = ((Button)sender).Content.ToString()
            };
        }
        private void UnaryOperator_Clicked(object sender, RoutedEventArgs e)
        {
            VM.Operations.Add(VM.Op);
            var op = new Operation
            {
                PreviousTotal = VM.Op.Results,
                Operator = ((Button)sender).Content.ToString()
            };
            VM.Operations.Add(op);
            VM.Op = new BinaryOperation
            {
                PreviousTotal = op.Results,
                Operator = "+"
            };
        }
    }
}
