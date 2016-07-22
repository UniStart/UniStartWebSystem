namespace UniStart.Unit.Tests
{
    using NUnit.Framework;
    using UniStart.Controllers;
    using Moq;
    using Unistart.Models;
    using UniStart.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Results;
    using UniStart.Common;

    public class LectureControllerTests
    {
        private LectureController controller;
        private Mock<IRepository<Lecture>> lectureRepositorMock;
        private IList<Lecture> mockLectures;

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
                new Lecture() {Id = 12, IsDeleted = false, Title = "FirstLecture.CoM", DatePublished = DateTime.UtcNow},
                new Lecture() {Id = 13, IsDeleted = true, Title = "SecondLecture", DatePublished = DateTime.UtcNow}
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

        [Test]
        public void GetLectureById_InNormalConditions_ShouldReturnTheLecture()
        {
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());
            var expectedLecture = new Lecture()
            {
                Id = 12,
                IsDeleted = false,
                Title = "FirstLecture.CoM",
                DatePublished = DateTime.UtcNow
            };

            IHttpActionResult actionResult = controller.GetLectureById(12);
            var content = actionResult as OkNegotiatedContentResult<Lecture>;
            var actualLecture = content.Content;

            Assert.IsNotNull(actualLecture);
            AssertLecturesEqual(expectedLecture, actualLecture);
        }

        [Test]
        public void GetLectureById_WhenSuchLectureDoesntExist_ShouldReturnBadRequestMessage()
        {
            IHttpActionResult actionResult = controller.GetLectureById(0);

            var result = actionResult as BadRequestErrorMessageResult;

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(actionResult);
            Assert.AreEqual(GlobalConstants.NonExistingLecture, result.Message);

            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void SearchLectureByName_InNormalConditions_ShouldReturnAllLecturesThatContainTheString()
        {
            string searchWord = "com";
            mockLectures.Add(new Lecture() { Id = 14, Title = "New Compass", IsDeleted = false });
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());

            IHttpActionResult actionResult = controller.SearchLectureByName(searchWord);
            var result = actionResult as OkNegotiatedContentResult<List<Lecture>>;
            var content = result.Content;

            Assert.IsNotNull(content);
            Assert.AreEqual(2, content.Count);
        }

        [Test]
        public void SearchLectureByName_WhenLecturesAreMissing_ShouldReturnErrorMessage()
        {
            string searchWord = "bg";
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());

            IHttpActionResult actionResult = controller.SearchLectureByName(searchWord);

            var result = actionResult as BadRequestErrorMessageResult;

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(actionResult);
            Assert.AreEqual(GlobalConstants.NonExistingLecture, result.Message);

            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void CreateLecture_InNormalcondition_ShouldSaveTheLecture()
        {
            var newLecture =
                new Lecture() { Id = 14, Title = "New Compass", IsDeleted = false };
            IHttpActionResult actionResult = controller.CreateLecture(newLecture);

            var result = actionResult as OkNegotiatedContentResult<Lecture>;
            var content = result.Content;

            Assert.IsNotNull(content);
            lectureRepositorMock.Verify(a => a.Add(newLecture));
            lectureRepositorMock.Verify(a => a.SaveChanges());
            AssertLecturesEqual(newLecture, content);
        }

        [Test]
        public void CreateLecture_WhenPassedLectureIsNull_ShouldReturnError()
        {
            IHttpActionResult actionResult = controller.CreateLecture(null);

            var result = actionResult as BadRequestErrorMessageResult;

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(actionResult);
            Assert.AreEqual("Lecture can't be null.", result.Message);

            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void UpdateLecture_InNormalConditions_ShouldUpdateTheLecture()
        {
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());

            var updatedLecture = new Lecture()
            {
                Id = 12,
                IsDeleted = false,
                Title = "FirstLecture.CoM and Some Update",
                DatePublished = DateTime.UtcNow
            };

            IHttpActionResult actionResult = controller.UpdateLecture(12, updatedLecture);
            var result = actionResult as OkNegotiatedContentResult<Lecture>;
            var content = result.Content;

            Assert.IsNotNull(result);

            lectureRepositorMock.Verify(r => r.SaveChanges());

            Assert.IsNotNull(content);
            AssertLecturesEqual(updatedLecture, content);
        }

        [Test]
        public void UpdateLecture_WhenLectureDoesntExist_ShouldCreateNewOne()
        {
            lectureRepositorMock.Setup(a => a.All()).Returns(mockLectures.AsQueryable());

            var updatedLecture = new Lecture()
            {
                Id = 14,
                IsDeleted = false,
                Title = "Non-existing Lecture",
                DatePublished = DateTime.UtcNow
            };

            IHttpActionResult actionResult = controller.UpdateLecture(14, updatedLecture);

            var result = actionResult as OkNegotiatedContentResult<Lecture>;
            var content = result.Content;

            Assert.IsNotNull(result);

            lectureRepositorMock.Verify(r => r.Add(updatedLecture));
            lectureRepositorMock.Verify(r => r.SaveChanges());

            AssertLecturesEqual(updatedLecture, content);
        }

        private void AssertLecturesEqual(Lecture expected, Lecture actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
        }
    }
}
