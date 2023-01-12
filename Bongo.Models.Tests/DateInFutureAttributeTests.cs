using Bongo.Models.ModelValidations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Models.Tests
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [Test]
        [TestCase(100, ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        public bool DateValidator_InputExpectedDateRange_DateValidity(int addTime) 
        {
            DateInFutureAttribute dateInFutureAttribute= new DateInFutureAttribute(()=>DateTime.Now);
            var result = dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));
           // Assert.That(result, Is.True);
           return result;
        }
        [Test]
        public void DateValidator_NotValidDate_ReturnErrorMessage()
        {
            var result =new DateInFutureAttribute();
            Assert.That(result.ErrorMessage, Is.EqualTo("Date must be in the future"));
        }
    }
}
