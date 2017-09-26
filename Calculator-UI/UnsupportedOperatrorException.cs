using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_UI
{
    class UnsupportedOperatorException : Exception
    {
        public UnsupportedOperatorException(Char op) : base(String.Format("Unsupported operator {0} used, please enter (+,-,/,*) only.", op))
        {

        }
    }
}
