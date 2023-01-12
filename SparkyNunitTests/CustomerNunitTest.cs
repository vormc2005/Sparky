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
    public class CustomerNunitTest
    {
        private Customer cust;
        [SetUp]
        public void SetUp()
        {
             cust = new Customer();
        }

        [Test]

        public void GreetAndCombineString_StringInput_GreetingStringOutput()
        {
            //Arrange
           // Customer cust = new Customer();
            //Act
            string greeting = cust.GreetAndCombineString("Dmitry", "Voronov");
            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(greeting, "Hello, Dmitry Voronov");
                //  Assert.That(greeting, Is.EqualTo("Hello, Dmitry Voronov"));
                Assert.That(greeting, Does.Contain(","));
                Assert.That(greeting, Does.StartWith("Hello"));
                Assert.That(greeting, Does.EndWith("voronov").IgnoreCase);
                Assert.That(greeting, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]"));
            });
        }
        [Test]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            //Arrange
           
            //Act
            //Assert
            Assert.IsNull(cust.GreetMessage);
        }
        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = cust.Discount;
            Assert.That(result, Is.InRange(10, 25));
        }
        [Test]
        public void GreetMessage_GreetedWithoutFirstName_ReturnsNotNull()
        {
            cust.GreetAndCombineString("ben", "");

                Assert.That(cust.GreetMessage, Is.Not.Null);
                Assert.IsFalse(string.IsNullOrEmpty(cust.GreetMessage));
            
        }
        [Test]
        public void GreetChecker_EmpyFirstName_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(()=>cust.GreetAndCombineString("", "Spark"));
            Assert.AreEqual("Empty First Name", exception.Message);
            Assert.That(exception.Message, Is.EqualTo("Empty First Name"));
            Assert.That(()=>cust.GreetAndCombineString("", "spark"), 
                Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));

            Assert.Throws<ArgumentException>(() => cust.GreetAndCombineString("", "Spark"));
           
            Assert.That(() => cust.GreetAndCombineString("", "spark"),
                Throws.ArgumentException);
        }
        [Test]
        public void CustomerType_CreateCustomerWithLess100_ReturnBasicCustomer()
        {
            cust.OrderTotal = 10;
            var result = cust.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerWithMore100_ReturnBasicCustomer()
        {
            cust.OrderTotal = 105;
            var result = cust.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }
    }
}
