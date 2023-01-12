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
    public class FiboNunitTests
    {
        private Fibo fibo;
        [SetUp]
        public void SetUp()
        {
            fibo= new Fibo();
        }

        [Test]
        public void GetFiboSeries_Input1_NotEmpty() 
        {
            fibo.Range = 1;
            List<int> expectedOutput = new() { 0 };
            List<int> result = fibo.GetFiboSeries();
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Is.Ordered);
                Assert.That(result, Is.EquivalentTo(expectedOutput));

            });
        }   
        [Test]
        public void GetFiboSeries_Input6_NotEmpty() 
        {
            fibo.Range = 6;
            List<int> expectedOutput = new() { 0, 1, 1, 2, 3, 5};
            List<int> result = fibo.GetFiboSeries();
            Assert.Multiple(() =>
            {               
                Assert.That(result, Does.Contain(3));
                Assert.That(result, Does.Not.Contain(6));
                Assert.That(result.Count, Is.EqualTo(6));
                Assert.That(result, Is.EquivalentTo(expectedOutput));
            });
        }

    }
}
