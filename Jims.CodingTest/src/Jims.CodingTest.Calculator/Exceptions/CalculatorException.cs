using System;

namespace Jims.CodingTest.Calculator.Exceptions
{
    public class CalculatorException : Exception
    {
        public CalculatorException()
        {
        }

        public CalculatorException(string message) : base(message)
        {
        }
    }
}
