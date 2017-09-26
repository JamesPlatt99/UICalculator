using System;
using System.Collections.Generic;

namespace Calculator_UI
{
    internal class NumericalCalc
    {
        public double StartNumCalc(String usrInput)
        {
            List<Char> operators = new List<Char>();
            List<double> numbers = new List<double>();
            List<String> inputTMP;
            String calcStr = "";

            Boolean valid = false;
            double curNum;
            Char curOp = '+';
            double result = 0;
            
                inputTMP = GetUserInputNumericalCalc(usrInput);
                operators = GetOpArray(inputTMP, ref valid);
                numbers = GetNumArray(inputTMP, ref valid);

            for (int i = 0; i < numbers.Count; i++)
            {
                try
                {
                    curNum = numbers[i];
                    curOp = operators[i];
                    result = PerformOneNumericalCalculation(curOp, result, curNum);
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Please ensure you start and end your input on a number.");
                    Console.WriteLine();
                }
            }
            return result;
        }

        private double PerformOneNumericalCalculation(Char op, double result, double number)
        {
            double temp;
            switch (op)
            {
                case '+':
                    result += Convert.ToDouble(number);
                    break;
                case '-':
                    result -= Convert.ToDouble(number);
                    break;
                case '*':
                    result = result * Convert.ToDouble(number);
                    break;
                case '/':
                    if(Convert.ToDouble(number) == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    result = result / Convert.ToDouble(number);
                    break;
            }
            return result;
        }

        private List<String> GetUserInputNumericalCalc(String usrInput)
        {
            string[] userInput = usrInput.Split(' ');
            List<String> userInputList = new List<String>();
            foreach (String number in userInput)
            {
                userInputList.Add(number);
            }
            return userInputList;
        }

        private List<double> GetNumArray(List<String> input, ref Boolean valid)
        {
            List<double> output = new List<double>();
            for (int i = 0; i < input.Count; i += 2)
            {
                if (!(valid = MainWindow.IsValid(input[i], 1)))
                {
                    throw new Exception();
                    return new List<double>();
                }
                output.Add(Convert.ToDouble(input[i]));
            }
            return output;
        }

        private List<Char> GetOpArray(List<String> input, ref Boolean valid)
        {
            List<Char> output = new List<Char>();
            //The first number should always be added to the result first
            output.Add('+');
            for (int i = 1; i < input.Count - 1; i += 2)
            {
                    if (!(valid = MainWindow.IsValid(input[i], 0)))
                    {
                        throw new UnsupportedOperatorException(input[i].ToCharArray()[0]);
                    }             
                output.Add(input[i].ToCharArray()[0]);
            }
            return output;
        }
        
    }
}
