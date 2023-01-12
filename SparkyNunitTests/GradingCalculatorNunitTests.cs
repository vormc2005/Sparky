using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace SparkyNunitTests
{
    [TestFixture]
    public class GradingCalculatorNunitTests
    {
        public GradingCalculator calculator;
        [SetUp]
        public void SetUp()
        {
            calculator = new GradingCalculator();
        }
        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GradingCalculator_Input_Grades(int score, int attendancePercentage)
        {
            calculator.Score = score;
            calculator.AttendancePercentage = attendancePercentage;
            var result = calculator.GetGrade();
            return result;
        }

        
    }
}
