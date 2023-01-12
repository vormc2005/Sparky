using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyMSTest
{
     [TestClass]
    public class CalculatorMsTests
    {

        [TestMethod]
        public void AddNumbers_IntInput_OutputInSum()
        {
            //Arrange
            Calculator calc = new Calculator();
            //Act
            int result = calc.AddNumbers(8, 6);
            //Assert
            Assert.AreEqual(14, result);
        }
    }
}
