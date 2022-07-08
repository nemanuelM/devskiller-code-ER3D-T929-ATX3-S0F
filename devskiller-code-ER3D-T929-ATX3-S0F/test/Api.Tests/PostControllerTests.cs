using Api.Controllers;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class PostControllerTests
    {
        #region Properties / Attributes

        
        private readonly Mock<IPostDataService> _postDataServiceMock;

        private readonly PostController _postController;


        #endregion


        public PostControllerTests()
        {            
            _postDataServiceMock = new Mock<IPostDataService>();

            _postController = new PostController(null, _postDataServiceMock.Object);
        }


        [Fact]
        public void GetAll_Returns_Existing_Posts()
        {
            // Arrange
            var expected = new List<Post>();
            _postDataServiceMock.Setup(_ => _.GetAll()).Returns(expected);

            // Act
            var actual = _postController.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void GetAll_Returns_BadRequest()
        {
            // Arrange
            var expected = new List<Post>();
            _postDataServiceMock.Setup(_ => _.GetAll()).Throws(new Exception());

            // Act
            var actual = _postController.GetAll();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Get_Returns_Existing_Posts()
        {
            // Arrange
            var expected = new Post();
            _postDataServiceMock.Setup(_ => _.Get(It.IsAny<Guid>())).Returns(expected);

            // Act
            var actual = _postController.Get(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void Get_Returns_BadRequest()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.Get(It.IsAny<Guid>())).Throws(new Exception());

            // Act
            var actual = _postController.Get(It.IsAny<Guid>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Post_Returns_Existing_Post()
        {
            // Arrange
            var expected = new Post();
            _postDataServiceMock.Setup(_ => _.Create(It.IsAny<Post>())).Returns(expected);

            // Act
            var actual = _postController.Post(It.IsAny<Post>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void Post_Returns_BadRequest()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.Create(It.IsAny<Post>())).Throws(new Exception());

            // Act
            var actual = _postController.Post(It.IsAny<Post>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void Put_Returns_Update_True()
        {
            // Arrange
            var post = new Post
            {
                Id =Guid.NewGuid(), 
                Title = "Title",
                Content = "Content",
                CreationDate = DateTime.Now                
            };

            var guid = Guid.NewGuid();

            _postDataServiceMock.Setup(_ => _.Update(It.IsAny<Post>())).Returns(true);

            // Act
            var actual = _postController.Put(Guid.NewGuid(), post);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(true, okObjectResult.Value);
        }

        [Fact]
        public void Put_Returns_Update_False()
        {
            // Arrange
            var post = new Post
            {                
                Title = "Title",
                Content = "Content",
                CreationDate = DateTime.Now
            };

            var guid = Guid.NewGuid();

            _postDataServiceMock.Setup(_ => _.Update(It.IsAny<Post>())).Returns(false);


            // Act
            var actual = _postController.Put(guid, post);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(false, okObjectResult.Value);
        }

        [Fact]
        public void Put_Returns_BadRequest()
        {
            // Arrange
            var post = new Post
            {                
                Title = "Title",
                Content = "Content",
                CreationDate = DateTime.Now
            };
            var guid = Guid.NewGuid();

            _postDataServiceMock.Setup(_ => _.Update(It.IsAny<Post>())).Throws(new Exception());

            // Act
            var actual = _postController.Put(Guid.NewGuid(), post);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }


        [Fact]
        public void Delete_Returns_DeleteTrue()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Returns(true);

            // Act
            var actual = _postController.Delete(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(true, okObjectResult.Value);
        }

        [Fact]
        public void Delete_Returns_DeleteFalse()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Returns(false);

            // Act
            var actual = _postController.Delete(It.IsAny<Guid>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(false, okObjectResult.Value);
        }

        [Fact]
        public void Delete_Returns_BadRequest()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Throws(new Exception());

            // Act
            var actual = _postController.Delete(It.IsAny<Guid>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public void GetComments_Returns_BadRequest()
        {
            // Arrange
            _postDataServiceMock.Setup(_ => _.GetByPostId(It.IsAny<Guid>())).Throws(new Exception());

            // Act
            var actual = _postController.GetComments(It.IsAny<Guid>());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(actual.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        //TODO Teardown
    }
}