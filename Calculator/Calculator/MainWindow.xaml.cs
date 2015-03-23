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
            History.ItemsSource = _Operations;
        }

        BinaryOperation _Op = new BinaryOperation();        
        ObservableCollection< Operation > _Operations = new ObservableCollection< Operation>();

        private void Number_Clicked(object sender, RoutedEventArgs e)
        {
            
            _Op.StrOperand += ((Button)sender).Content.ToString();
            Results.Text = _Op.StrOperand;
        }

        private void Operator_Clicked(object sender, RoutedEventArgs e)
        {
            _Operations.Add(_Op);
            _Op = new BinaryOperation
            {
                PreviousTotal = _Op.Results,
                Operator = ((Button)sender).Content.ToString()
            };

            Results.Text = _Op.PreviousTotal.ToString();
        }
        private void UnaryOperator_Clicked(object sender, RoutedEventArgs e)
        {
            _Operations.Add(_Op);
            var op = new Operation
            {
                PreviousTotal = _Op.Results,
                Operator = ((Button)sender).Content.ToString()
            };
            _Operations.Add(op);
            _Op = new BinaryOperation
            {
                PreviousTotal = op.Results,
                Operator = "+"
            };

            Results.Text = _Op.PreviousTotal.ToString();
        }
    }
}
