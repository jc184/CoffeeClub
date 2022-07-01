using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICommentsRepository : IRepositoryBase<Comments>
    {
        Task<List<Comments>> GetAllCommentsAsync(bool trackChanges);
        Task<IEnumerable<Comments>> GetCommentsByIdAsync(int commentId, bool trackChanges);
        Task<IEnumerable<Comments>> GetCommentsByCoffeeIdAsync(int coffeeId, bool trackChanges);
        void CreateComment(int coffeeId, Comments comment);
        void UpdateComment(Comments comment);
        void DeleteComment(Comments comment);
    }
}
