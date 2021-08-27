using Amazon.Lambda.Core;
using Jims.CodingTest.Calculator.Exceptions;
using Jims.CodingTest.Calculator.Models;
using System;
using static Jims.CodingTest.Calculator.Enums;

namespace Jims.CodingTest.Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private bool _disposed;
        private ILambdaContext _context;
        
        public CalculatorService(ILambdaContext context)
        {
            _context = context;
        }

        public ResultModel Calculate(InputModel input)
        {
            try
            {
                ValidateInput(input);

                _context.Logger.Log($"CalculatorService.Calculate() received: First Number: {input.FirstNumber}, Second Number: {input.SecondNumber} and Operator: {input.Operator}");

                var oper = (Operator)input.Operator;
                var result = new ResultModel()
                {
                    Input = input
                };

                switch (oper)
                {
                    case Operator.Subtract:
                        {
                            result.Result = input.FirstNumber - input.SecondNumber;
                            break;
                        }
                    case Operator.Multiply:
                        {
                            result.Result = input.FirstNumber * input.SecondNumber;
                            break;
                        }
                    case Operator.Divide:
                        {
                            result.Result = input.FirstNumber / input.SecondNumber;
                            break;
                        }
                    default:
                        {
                            result.Result = input.FirstNumber + input.SecondNumber;
                            break;
                        }
                }

                _context.Logger.Log("Calculation succeeded.");
                return result;

            }
            catch (Exception e)
            {
                _context.Logger.Log(e.Message);
                throw;
            }
        }

        private void ValidateInput(InputModel input)
        {
            if (input == null)
            {
                throw new CalculatorException("Invalid input");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _context = null;
                _disposed = true;
            }
        }
    }
}
