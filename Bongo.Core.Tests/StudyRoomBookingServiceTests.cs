using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableStudyRoom;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepositoryMock;
        private Mock<IStudyRoomRepository> _studyRoomRepositoryMock;
        private StudyRoomBookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmail.com",
                Date = new DateTime(2022, 1, 1)
            };
            _availableStudyRoom = new List<StudyRoom>{
                new StudyRoom
                {
                    Id = 10,
                    RoomName = "Michigan",
                    RoomNumber = "A202"
                }
            };

            _studyRoomBookingRepositoryMock = new Mock<IStudyRoomBookingRepository>();            
            _studyRoomRepositoryMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepositoryMock.Setup(r => r.GetAll()).Returns(_availableStudyRoom);

            _bookingService = new StudyRoomBookingService(_studyRoomBookingRepositoryMock.Object, _studyRoomRepositoryMock.Object);

        }

        [TestCase]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _bookingService.GetAllBooking();
            _studyRoomBookingRepositoryMock.Verify(x => x.GetAll(null), Times.Once);
        }
        [TestCase]
        public void Booking_exception_NullRequest_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>_bookingService.BookStudyRoom(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }
        [Test]

        public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithValues()
        {
            StudyRoomBooking savedStudyRoomBooking = null;
            _studyRoomBookingRepositoryMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking => { savedStudyRoomBooking = booking;
                });
            _bookingService.BookStudyRoom(_request);
            //assert
            _studyRoomBookingRepositoryMock.Verify(x=>x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
            Assert.NotNull(savedStudyRoomBooking);
            Assert.That(savedStudyRoomBooking.FirstName, Is.EqualTo(_request.FirstName));
            Assert.That(_availableStudyRoom.First().Id, Is.EqualTo(savedStudyRoomBooking.StudyRoomId));
        }
        [Test]
        public void StudyRoomResultCheck_InputRequest_ValuesMatchResult()
        {
            StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);
            Assert.NotNull(result);
            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
        }
        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailability_ReturnsSuccessResultCode(bool availability)
        {
            if (!availability)
            {
                _availableStudyRoom.Clear();
            }           
            var result = _bookingService.BookStudyRoom(_request).Code;
           // Assert.AreEqual(StudyRoomBookingCode.Success, result.Code);
             return result;           
        }

    }   
}
