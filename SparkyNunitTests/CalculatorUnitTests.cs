using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNunitTests
{
    [TestFixture]
    public class CalculatorUnitTests
    {
        private Calculator calculator;
        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }
        [Test]
        public void AddNumbers_IntInput_IntSum()
        {
            //Arrange
            Calculator calc = new Calculator();
            //Act
            var result = calc.AddNumbers(3, 5);

            //Assert
            Assert.That(result, Is.EqualTo(8));
        }
        [Test]
        [TestCase(3.5, 7.3)]
        public void AddDoubleNumbers_IntInput_IntSum(double a, double b)
        {
            //Arrange
            Calculator calc = new Calculator();
            //Act
            double result = calc.AddDoubleNumbers(a, b);

            //Assert
            Assert.That(result, Is.EqualTo(10.8));
        }

        [Test]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = true)]
        public bool IsOddNumber(int a)
        {
            //Arrange
            Calculator calc = new Calculator();
            //Act
            bool result = calc.IsOddNumber(a);

            //Assert
            return result;
        }

        [Test]
        public void OddRanger_InputMinMaxRange_ReturnsOddNumberRange()
        {
            List<int> expectedOddRange= new() { 5, 7, 9};//4-10
            //act
            List<int> result = calculator.GetRange(4, 10);
            //Assert
            Assert.That(result, Is.EquivalentTo(expectedOddRange));
            //Assert.AreEqual(expectedOddRange, result);
            //Assert.Contains(7, result);
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));
            Assert.That(result, Is.Ordered);
          //  Assert.That(result, Is.Ordered.Descending);
           Assert.That(result, Is.Unique);



        }

    }
}
