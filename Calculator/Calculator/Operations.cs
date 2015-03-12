using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BinaryOperation
    {
        private string _Operator = "+";
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }
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

        private float _PreviousTotal;
        public float PreviousTotal
        {
            get { return _PreviousTotal; }
            set { _PreviousTotal = value; }
        }



        public float Results
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

    public class Operations
    {
    }
}
