using Api.Controllers;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;

namespace Api.Tests
{
    public class CommentControllerTests
    {
        #region Properties / Attributes

        private readonly Mock<Logger<CommentController>> _loggerMock;
        private readonly Mock<ICommentDataService> _commentDataServiceMock;

        private readonly CommentController _commentController;

        #endregion Properties / Attributes

        public CommentControllerTests()
        {
            _loggerMock = new Mock<Logger<CommentController>>();
            _commentDataServiceMock = new Mock<ICommentDataService>();

            _commentController = new CommentController(null, _commentDataServiceMock.Object);
        }

        [Fact]
        public void GetAll_Returns_Existing_Comments()
        {
            // Arrange
            var expected = new List<Comment>();
            _commentDataServiceMock.Setup(_ => _.GetAll()).Returns(expected);

            // Act
            var actual = _commentController.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void GetAll_Returns_BadRequest()
        {
            // Arrange
            var expected = new List<Comment>();
            _commentDataServiceMock.Setup(_ => _.GetAll()).Throws(new Exception());

            // Act
            var actual = _commentController.GetAll();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Get_Returns_Existing_Comment()
        {
            // Arrange
            var expected = new Comment();
            _commentDataServiceMock.Setup(_ => _.Get(It.IsAny<Guid>())).Returns(expected);

            // Act
            var actual = _commentController.Get(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void Get_Returns_BadRequest()
        {
            // Arrange
            _commentDataServiceMock.Setup(_ => _.Get(It.IsAny<Guid>())).Throws(new Exception());

            // Act
            var actual = _commentController.Get(It.IsAny<Guid>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Post_Returns_Existing_Comment()
        {
            // Arrange
            var expected = new Comment();
            _commentDataServiceMock.Setup(_ => _.Create(It.IsAny<Comment>())).Returns(expected);

            // Act
            var actual = _commentController.Post(It.IsAny<Comment>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void Post_Returns_BadRequest()
        {
            // Arrange
            _commentDataServiceMock.Setup(_ => _.Create(It.IsAny<Comment>())).Throws(new Exception());

            // Act
            var actual = _commentController.Post(It.IsAny<Comment>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Put_Returns_Update_True()
        {
            // Arrange
            var comment = new Comment
            {
                Author = "Author",
                Content = "Content",
                CreationDate = DateTime.Now,
                PostId = Guid.NewGuid()
            };

            var guid = Guid.NewGuid();

            _commentDataServiceMock.Setup(_ => _.Update(It.IsAny<Comment>())).Returns(true);

            // Act
            var actual = _commentController.Put(Guid.NewGuid(), comment);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(true, okObjectResult.Value);
        }

        [Fact]
        public void Put_Returns_Update_False()
        {
            // Arrange
            var comment = new Comment
            {
                Author = "Author",
                Content = "Content",
                CreationDate = DateTime.Now,
                PostId = Guid.NewGuid()
            };

            var guid = Guid.NewGuid();

            _commentDataServiceMock.Setup(_ => _.Update(It.IsAny<Comment>())).Returns(false);

            // Act
            var actual = _commentController.Put(Guid.NewGuid(), comment);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(false, okObjectResult.Value);
        }

        [Fact]
        public void Put_Returns_BadRequest()
        {
            // Arrange
            var comment = new Comment
            {
                Author = "Author",
                Content = "Content",
                CreationDate = DateTime.Now,
                PostId = Guid.NewGuid()
            };

            var guid = Guid.NewGuid();

            _commentDataServiceMock.Setup(_ => _.Update(It.IsAny<Comment>())).Throws(new Exception());

            // Act
            var actual = _commentController.Put(Guid.NewGuid(), comment);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Delete_Returns_DeleteTrue()
        {
            // Arrange
            _commentDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Returns(true);

            // Act
            var actual = _commentController.Delete(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(true, okObjectResult.Value);
        }

        [Fact]
        public void Delete_Returns_DeleteFalse()
        {
            // Arrange
            _commentDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Returns(false);

            // Act
            var actual = _commentController.Delete(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(false, okObjectResult.Value);
        }

        [Fact]
        public void Delete_Returns_BadRequest()
        {
            // Arrange
            _commentDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Throws(new Exception());

            // Act
            var actual = _commentController.Delete(It.IsAny<Guid>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        //TODO Teardown
    }
}