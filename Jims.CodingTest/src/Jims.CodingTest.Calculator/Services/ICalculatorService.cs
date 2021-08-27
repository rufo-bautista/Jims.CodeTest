using Jims.CodingTest.Calculator.Models;
using System;

namespace Jims.CodingTest.Calculator.Services
{
    public interface ICalculatorService : IDisposable
    {
        public ResultModel Calculate(InputModel input);
    }
}
