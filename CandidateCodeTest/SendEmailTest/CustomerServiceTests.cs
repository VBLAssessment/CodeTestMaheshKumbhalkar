using CandidateCodeTest;
using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using Moq;
using System;
using Xunit;

namespace SendEmailTest
{
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private Mock<IMessageService> _messageService;
        private readonly Mock<ILogEntry> _AddLogsr;

        public CustomerServiceTests()
        {
            //creating mock
            _messageService = new Mock<IMessageService>();
            _AddLogsr = new Mock<ILogEntry>();
        }

        /// <summary>
        /// Test cases used to check the test in case of positive scenario
        /// </summary>
        [Fact]
        public void Within_Time_Window_Email_Has_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _AddLogsr.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Test cases used to check the test in case of negative scenario
        /// 
        /// </summary>
        [Fact]
        public void Outside_Time_Window_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan();
            var endTime = new TimeSpan();
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _AddLogsr.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Test cases used to check the test in case of negative scenario
        /// </summary>
        [Fact]
        public void Case1_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = (TimeSpan?)null;
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _AddLogsr.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Test cases used to check the test in case of negative scenario
        /// </summary>
        [Fact]
        public void Case2_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _AddLogsr.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Test cases used to check the test in case of negative scenario
        /// </summary>
        [Fact]
        public void Case3_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = (TimeSpan?)null;
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _AddLogsr.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }
    }
}
