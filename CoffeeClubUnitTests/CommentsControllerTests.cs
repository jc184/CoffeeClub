using AutoMapper;
using CoffeeClub.Controllers;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeClubUnitTests
{
    public class CommentsControllerTests
    {
        //[Fact]
        //public async Task GetComments_Returns_AllComments()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IRepositoryManager>();
        //    var mockLogger = new Mock<ILoggerManager>();
        //    var mockMapper = new Mock<IMapper>();
        //    mockRepo.Setup(repo => repo.Comments.GetAllCommentsAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Comments>()));

        //    var controller = new CommentsController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

        //    // Act
        //    var result = await controller.GetComments();

        //    // Assert
        //    var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
        //    var model = Assert.IsAssignableFrom<OkObjectResult>(
        //        actionResult);

        //}

        [Fact]
        public async Task GetComment_Returns_Comment()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryManager>();
            var mockLogger = new Mock<ILoggerManager>();
            var mockMapper = new Mock<IMapper>();
            IEnumerable<Comments> rtnResult = new List<Comments>() { new Comments() { CommentId = 1, Comment = "Test", CoffeeId = 1, Rating = 5, DateCreated = System.DateTime.Now } };
            mockRepo.Setup(repo => repo.Comments.GetCommentsByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(rtnResult));

            var controller = new CommentsController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            int Id = 1;
            var result = await controller.GetComment(Id);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            Assert.Equal(200, model.StatusCode);
        }
    }
}
