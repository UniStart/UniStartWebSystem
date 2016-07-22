using System;
using System.Collections.Generic;
using System.Linq;

namespace UniStart.Unit.Tests
{
    using NUnit.Framework;
    using UniStart.Controllers;
    using Moq;
    using Unistart.Models;
    using UniStart.Data.Repositories;

    public class LectureControllerTests
    {
        private LectureController controller;
        private Mock<IRepository<Lecture>> lectureRepositorMock;
        private IEnumerable<Lecture> mockLectures;

        [SetUp]
        public void TestInitialize()
        {
            SetupMockLectures();
            lectureRepositorMock = new Mock<IRepository<Lecture>>();
            controller = new LectureController(lectureRepositorMock.Object);
        }

        private void SetupMockLectures()
        {
            mockLectures = new List<Lecture>()
            {
                new Lecture() {Id = 12, isDeleted = false, Title = "FirstLecture", DatePublished = DateTime.UtcNow},
                new Lecture() {Id = 13, isDeleted = true, Title = "SecondLecture", DatePublished = DateTime.UtcNow}
            };
        }

        [Test]
        public void GetAll_InNormalConditions_ShouldReturnOnlyTheAvailableLectures()
        {
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());
            var allLectures = controller.GetAll().ToList();

            Assert.IsNotNull(allLectures);
            Assert.AreEqual(1, allLectures.Count);
        }

        [Test]
        public void GetAll_WhenTheresNoExistingLectures_ShouldNotThrowException()
        {
            var allLectures = controller.GetAll();

            Assert.IsNotNull(allLectures);
        }
    }
}
