using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BinaryOperation
    {
        public float PreviousTotal;
        public string Operator = "+";
        public float Operand;
        public string StrOperand;

        public float GetResults()
        {
            if (!float.TryParse(StrOperand, out Operand))
            {
                return PreviousTotal;
            }
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

    public class Operations
    {
    }
}
