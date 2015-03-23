using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Operation
    {
        private string _Operator = "+";
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        private float _PreviousTotal;
        public float PreviousTotal
        {
            get { return _PreviousTotal; }
            set { _PreviousTotal = value; }
        }
        
        public virtual float Results
        {
            get
            {
                switch (Operator)
                {
                    case "sq":
                        return PreviousTotal * PreviousTotal;
                    case "sqrt":
                        return (float) Math.Sqrt( (double)PreviousTotal );
                    case "+/-":
                        return -PreviousTotal;
                    case "=":
                        return PreviousTotal;
                }
                return PreviousTotal;
            }
        }

        public override string ToString()
        {
            return Operator.ToString() + " " + PreviousTotal.ToString();
        }
    }

    public class BinaryOperation : Operation
    {
        public string StrOperand { get; set; }

        private float _Operand;
        public float Operand
        {
            get
            {
                if (!float.TryParse(StrOperand, out _Operand))
                {
                    return PreviousTotal;
                }
                return _Operand;
            }
        }
        public override float Results
        {
            get
            {
                switch (Operator)
                {
                    case "+":
                        return PreviousTotal + Operand;
                    case "-":
                        return PreviousTotal - Operand;
                    case "x":
                        return PreviousTotal * Operand;
                    case "/":
                        return PreviousTotal / Operand;
                }
                return PreviousTotal;
            }
        }
        public override string ToString()
        {
            return Operator.ToString() + " " + Operand.ToString();
        }
    }

    public class OperationsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
