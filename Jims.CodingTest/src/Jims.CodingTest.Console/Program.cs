using Jims.CodingTest.Console.Models;
using Jims.CodingTest.Console.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Jims.CodingTest.Console
{
    public static class Program
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private static IConfiguration _config;

        /// <summary>
        /// Main program
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>Task</returns>
        private static async Task Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_config)
                .CreateLogger();

            try
            {
                // retrieve value from appsettings
                bool.TryParse(_config.GetSection("CalculateHardCoded").Value, out bool calculateHardCoded);

                // if calculate hard coded values
                if (calculateHardCoded)
                {
                    // calculate hard coded values
                    await CalculateHardCoded();
                }
                else
                {
                    // Calculate input
                    await CalculateInput();
                }
                
            }
            catch (Exception e)
            {
                // log error
                Log.Logger.Error(e.Message);
                throw;
            }
            
        }

        /// <summary>
        /// Calculate input
        /// </summary>
        /// <returns>Task</returns>
        private static async Task CalculateInput()
        {
            // get first number
            float firstNumber = GetFirstNumber();

            // get second number
            float secondNumber = GetSecondNumber();

            // get operator
            int oper = GetOperator(secondNumber);

            // get input model
            InputModel input = GetInputModel(firstNumber, secondNumber, oper);

            // calculate input
            await Calculate(input);

            // prompt user to calculate another?
            await CalculateAnother();
        }

        /// <summary>
        /// Get first number
        /// </summary>
        /// <returns>First number</returns>
        private static float GetFirstNumber()
        {
            // prompt and retrieve first number
            System.Console.Write("Enter First Number: ");
            string strFirstNumber = System.Console.ReadLine();
            float.TryParse(strFirstNumber, out float firstNumber);
            System.Console.WriteLine($"First Number: {firstNumber}");

            // return first number
            return firstNumber;
        }

        /// <summary>
        /// Get second number
        /// </summary>
        /// <returns>Second number</returns>
        private static float GetSecondNumber()
        {
            // prompt and retrieve second number
            System.Console.Write("Enter Second Number: ");
            string strSecondNumber = System.Console.ReadLine();
            float.TryParse(strSecondNumber, out float secondNumber);
            System.Console.WriteLine($"First Number: {secondNumber}");

            // return second number
            return secondNumber;
        }

        /// <summary>
        /// Get operator
        /// </summary>
        /// <returns>Operator</returns>
        private static int GetOperator(float secondNumber)
        {
            // prompt and retrieve operator
            System.Console.Write("Enter Operator [1]Add, [2]Subtract, [3]Multiply, [4]Divide: ");
            string strOper = System.Console.ReadLine();
            int.TryParse(strOper, out int oper);

            if (secondNumber == 0 && oper == 4)
            {
                System.Console.WriteLine("Division by 0 is not allowed.");
                oper = GetOperator(secondNumber);
                return oper;
            }

            if (oper == 0 || oper > 4)
            {
                oper = 1;
            }
            System.Console.WriteLine($"Operator: {oper}");

            // return operator
            return oper;
        }

        /// <summary>
        /// Get input model
        /// </summary>
        /// <param name="firstNumber">First number</param>
        /// <param name="secondNumber">Second number</param>
        /// <param name="oper">Operator</param>
        /// <returns>Input model</returns>
        private static InputModel GetInputModel(float firstNumber, float secondNumber, int oper)
        {
            // prepare input model
            InputModel input = new InputModel()
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operator = oper
            };

            // return input model
            return input;
        }

        /// <summary>
        /// Calculate provided input
        /// </summary>
        /// <param name="input">Input to calculate</param>
        /// <returns>Task</returns>
        private static async Task Calculate(InputModel input)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Calculating...");
            using ICalculatorService calc = new CalculatorService(_config, Log.Logger);
            var result = await calc.Calculate(input);

            System.Console.WriteLine($"Result: {result.Result}");
        }

        /// <summary>
        /// Prompt user to calculate another
        /// </summary>
        private static async Task CalculateAnother()
        {
            System.Console.WriteLine("Calculate another? [Y or any key to exit:] ");
            ConsoleKeyInfo calculateAnother  = System.Console.ReadKey();
            if (calculateAnother.Key == ConsoleKey.Y)
            {
                System.Console.Clear();
                await CalculateInput();
            }
        }

        private static async Task CalculateHardCoded()
        {
            float firstNumber = 3;
            float secondNumber = 2;
            int oper = 1;

            // display values to calculate
            System.Console.WriteLine($"First number: {firstNumber}");
            System.Console.WriteLine($"Second number: {secondNumber}");
            System.Console.WriteLine($"Operator [1]Add, [2]Subtract, [3]Multiply, [4]Divide: {oper}");

            // get input model
            InputModel input = GetInputModel(3, 2, 1);

            // calculate input
            await Calculate(input);

            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
