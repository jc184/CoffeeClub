using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CommentsRepository : RepositoryBase<Comments>, ICommentsRepository
    {
        public CommentsRepository(CoffeeClubContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateComment(int coffeeId, Comments comment)
        {
            comment.CoffeeId = coffeeId;
            Create(comment);
        }

        public void DeleteComment(Comments comment)
        {
            Delete(comment);
        }

        public async Task<IEnumerable<Comments>> GetAllCommentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(co => co.Comment)
                .ToListAsync();


        public async Task<Comments> GetCommentByIdAsync(int commentId, bool
            trackChanges) =>
            await FindByCondition(comment => comment.CommentId.Equals(commentId), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateComment(Comments comment)
        {
            Update(comment);
        }
    }
}
