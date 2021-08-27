using Jims.CodingTest.Console.Models;
using System;
using System.Threading.Tasks;

namespace Jims.CodingTest.Console.Services
{
    interface ICalculatorService : IDisposable
    {
        public Task<ResultModel> Calculate(InputModel input);
    }
}
