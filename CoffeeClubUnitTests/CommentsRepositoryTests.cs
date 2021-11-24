using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeClubUnitTests
{
    public class CommentsRepositoryTests
    {
        private readonly Mock<ICommentsRepository> _mockRepo;
        public CommentsRepositoryTests()
        {
            _mockRepo = new Mock<ICommentsRepository>();
        }

        [Fact]
        public void GetCommentByIdAsync_Returns_Comment()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Comments>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Comments()));
            dbContextMock.Setup(s => s.Set<Comments>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetCommentByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Comments()));
            var commentsRepoMock = _mockRepo.Object;

            //Execute method of SUT (CoffeeRepository)
            int commentId = 1;
            bool trackChanges = false;
            var comment = commentsRepoMock.GetCommentByIdAsync(commentId, trackChanges).Result;

            //Assert
            Assert.NotNull(comment);
            Assert.IsAssignableFrom<Comments>(comment);
        }

        [Fact]
        public void GetAllCommentAsync_Returns_AllComments()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Comments>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Comments()));
            dbContextMock.Setup(s => s.Set<Comments>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAllCommentsAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Comments>()));
            var commentRepoMock = _mockRepo.Object;

            //Execute method of SUT (CoffeeRepository)
            var comments = commentRepoMock.GetAllCommentsAsync(false).Result;

            //Assert
            Assert.NotNull(comments);
            Assert.IsAssignableFrom<List<Comments>>(comments);
        }

        [Fact]
        public void CreateComment_Returns_Comment()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Comments>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Comments()));
            dbContextMock.Setup(s => s.Set<Comments>()).Returns(dbSetMock.Object);

            int coffeeId = 1;
            _mockRepo.Setup(p => p.CreateComment(coffeeId, new Comments()));
            var commentRepoMock = _mockRepo.Object;
            var comment = new Comments() { Comment = "some comment", DateCreated = DateTime.Now, Rating = 5};
            commentRepoMock.CreateComment(coffeeId, comment);

            //Assert
            Assert.NotNull(comment);
            Assert.IsAssignableFrom<Comments>(comment);
        }
    }
}
