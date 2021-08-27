using Amazon.Lambda.Core;
using Jims.CodingTest.Calculator.Exceptions;
using Jims.CodingTest.Calculator.Models;
using Jims.CodingTest.Calculator.Services;
using System;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Jims.CodingTest.Calculator
{
    public class Function
    {
        public string FunctionHandler(InputModel input, ILambdaContext context)
        {
            try
            {
                // validate input
                ValidateInput(input);

                // log received input
                context.Logger.Log($"FunctionHandler received First Number: {input.FirstNumber}, Second Number: {input.SecondNumber} and Operator: {input.Operator}");

                // calculate
                using ICalculatorService _cal = new Services.CalculatorService(context);
                ResultModel result = _cal.Calculate(input);

                // log result
                context.Logger.Log($"Calculator result: {result.Result}");

                // return result
                return JsonSerializer.Serialize(result);
            }
            catch (Exception e)
            {
                // log error
                context.Logger.Log(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get input
        /// </summary>
        /// <param name="input">Input</param>
        private void ValidateInput(InputModel input)
        {
            if (input == null)
            {
                throw new CalculatorException("Invalid request");
            }
        }
    }
}
