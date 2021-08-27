using Amazon.Lambda.TestUtilities;
using Jims.CodingTest.Calculator.Exceptions;
using Jims.CodingTest.Calculator.Models;
using Jims.CodingTest.Calculator.Services;
using NUnit.Framework;
using Shouldly;
using static Jims.CodingTest.Calculator.Enums;

namespace Jims.CodingTest.Calculator.Tests.Services
{
    [TestFixture]
    public class Calculator_Tests
    {
        private static TestLambdaContext _testContext;
        private static ICalculatorService _calculatorService;

        [SetUp]
        public void SetUp()
        {
            _testContext = new TestLambdaContext();
            _calculatorService = new CalculatorService(_testContext);
        }

        [TearDown]
        public void TearDown()
        {
            _testContext = null;
            _calculatorService = null;
        }

        [Test]
        public void WhenCalculate_WithValidInputModel_ShouldNotThrowError()
        {
            // assert
            Should.NotThrow(() => _calculatorService.Calculate(_validInputModel));
        }

        [Test]
        public void WhenCalculate_WithInvalidInputModel_ShouldThrowError()
        {
            // assert
            Should.Throw<CalculatorException>(() => _calculatorService.Calculate(_invalidInputModel));
        }

        [Test]
        public void WhenCalculate_WithAddition_ShouldReturnSum()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Add;

            // act
            ResultModel result = _calculatorService.Calculate(_validInputModel);

            // assert
            result.Result.ShouldBe(_sum);
        }

        [Test]
        public void WhenCalculate_WithSubtraction_ShouldReturnDifference()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Subtract;

            // act
            ResultModel result = _calculatorService.Calculate(_validInputModel);

            // assert
            result.Result.ShouldBe(_difference);
        }

        [Test]
        public void WhenCalculate_WithMultiplication_ShouldReturnProduct()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Multiply;

            // act
            ResultModel result = _calculatorService.Calculate(_validInputModel);

            // assert
            result.Result.ShouldBe(_product);
        }

        [Test]
        public void WhenCalculate_WithDivision_ShouldReturnQuotient()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Divide;

            // act
            ResultModel result = _calculatorService.Calculate(_validInputModel);

            // assert
            result.Result.ShouldBe(_quotient);
        }

        // test data
        private static float _firstNumber = 6f;
        private static float _secondNumber = 3f;
        private static float _sum = 9f;
        private static float _difference = 3f;
        private static float _product = 18f;
        private static float _quotient = 2f;

        private static InputModel _validInputModel = new InputModel()
        {
            FirstNumber = _firstNumber,
            SecondNumber = _secondNumber
        };

        private static InputModel _invalidInputModel = null;

        
    }
}
