using Amazon.Lambda.TestUtilities;
using Jims.CodingTest.Calculator.Exceptions;
using Jims.CodingTest.Calculator.Models;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static Jims.CodingTest.Calculator.Enums;

namespace Jims.CodingTest.Calculator.Tests
{
    [TestFixture]
    public class Function_Test
    {
        private static TestLambdaContext _testContext;
        private static Function _function;

        [SetUp]
        public void SetUp()
        {
            _testContext = new TestLambdaContext();
            _function = new Function();
        }

        [TearDown]
        public void TearDown()
        {
            _testContext = null;
            _function = null;
        }

        [Test]
        public void WhenFunctionHandler_WithValidInputModel_ShouldNotThrowError()
        {
            // assert
            Should.NotThrow(() => _function.FunctionHandler(_validInputModel, _testContext));
        }

        [Test]
        public void WhenFunctionHandler_WithInvalidInputModel_ShouldNotThrowError()
        {
            // assert
            Should.Throw<CalculatorException>(() => _function.FunctionHandler(_invalidInputModel, _testContext));
        }

        [Test]
        public void WhenFunctionHandler_WithAddition_ShouldReturnSum()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Add;

            // act
            string strResult = _function.FunctionHandler(_validInputModel, _testContext);
            ResultModel result = JsonSerializer.Deserialize<ResultModel>(strResult);

            // assert
            result.Result.ShouldBe(_sum);
        }

        [Test]
        public void WhenFunctionHandler_WithSubtraction_ShouldReturnDifference()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Subtract;

            // act
            string strResult = _function.FunctionHandler(_validInputModel, _testContext);
            ResultModel result = JsonSerializer.Deserialize<ResultModel>(strResult);

            // assert
            result.Result.ShouldBe(_difference);
        }

        [Test]
        public void WhenFunctionHandler_WithMultiplication_ShouldReturnProduct()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Multiply;

            // act
            string strResult = _function.FunctionHandler(_validInputModel, _testContext);
            ResultModel result = JsonSerializer.Deserialize<ResultModel>(strResult);

            // assert
            result.Result.ShouldBe(_product);
        }

        [Test]
        public void WhenFunctionHandler_WithDivision_ShouldReturnQuotient()
        {
            // arrange
            _validInputModel.Operator = (int)Operator.Divide;

            // act
            string strResult = _function.FunctionHandler(_validInputModel, _testContext);
            ResultModel result = JsonSerializer.Deserialize<ResultModel>(strResult);

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
