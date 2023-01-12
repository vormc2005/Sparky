using Moq;
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
    public class BankAccountNUnitTest
    {
        private BankAccount account;
        [SetUp]
        public void SetUp()
        {
           
        }
        //[Test]
        //public void BankDeposit_Add100_ReturnTrue()
        //{
        //   BankAccount bankAccount = new(new LogFakker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.That(result, Is.True);
        //    Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        //}

        [Test]
        public void BankDepositMoq_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            
            BankAccount bankAccount = new(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.That(result, Is.True);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }
        [Test]
        [TestCase(200, 100)]
        public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdrawal)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWithrawal(It.IsAny<int>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x=>x > 0))).Returns(true);

            BankAccount bankAccount = new (logMock.Object);
            bankAccount.Deposit(balance);

            var result = bankAccount.Withdraw(withdrawal);
            Assert.That(result, Is.True);

        }
        [Test]
        public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse()
        {
            var logMock = new Mock<ILogBook>();
           
            //logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x => x < 0))).Returns(false);
            //logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x => x < 0))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(300);

            var result = bankAccount.Withdraw(200);
            Assert.That(result, Is.False);
        }
        [Test]
        public void BankLogDummy_LogMoqString_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();

            //logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) =>str.ToLower());

            //logMock.Setup(u => u.LogBalanceAfterWithrawal(It.Is<int>(x => x < 0))).Returns(false);
            string desiredOutput = "hello";
            Assert.That(logMock.Object.MessageWithReturnStr("HELLO"), Is.EqualTo(desiredOutput));
        }
        [Test]
        public void BankLogDummy_LogMoqStringOutputStr_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput="hello";

            
            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.That(logMock.Object.LogWithOutputResult("Ben", out result));         
            Assert.That(result, Is.EqualTo(desiredOutput));
        }
        [Test]
        public void BankLogDummy_LogRefChecker_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();


            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);
          
            Assert.That(logMock.Object.LogWithRefObj(ref customerNotUsed), Is.EqualTo(false));
            Assert.That(logMock.Object.LogWithRefObj(ref customer), Is.EqualTo(true));
            
        }
        [Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(u => u.LogSeverity).Returns(10);   
            logMock.Setup(u => u.LogType).Returns("warning");
            logMock.Object.LogSeverity = 100;

           Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
           Assert.That(logMock.Object.LogType, Is.EqualTo("warning"));

            //callbacks
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback((string str)=>logTemp += str);
            logMock.Object.LogToDb("Ben");
            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

            //callbacks
            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback(()=> counter++);
            logMock.Object.LogToDb("Ben");
            Assert.That(counter, Is.EqualTo(6));
        }
        [Test]
        public void BankLogDummy_VeryfyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(100);
            //verification
            logMock.Verify(u=>u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.VerifySet(u=>u.LogSeverity = 101, Times.Once);
            logMock.VerifyGet(u=>u.LogSeverity, Times.Once);
        }
    }
}
