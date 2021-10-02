using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private CoffeeClubContext _coffeeClubContext;
        private ICoffeeRepository _coffeeRepository;
        private ICommentsRepository _commentsRepository;
        public RepositoryManager(CoffeeClubContext coffeeClubContext)
        {
            _coffeeClubContext = coffeeClubContext;
        }
        public ICoffeeRepository Coffee
        {
            get
            {
                if (_coffeeRepository == null)
                    _coffeeRepository = new CoffeeRepository(_coffeeClubContext);
                return _coffeeRepository;
            }
        }
        public ICommentsRepository Comments
        {
            get
            {
                if (_commentsRepository == null)
                    _commentsRepository = new CommentsRepository(_coffeeClubContext);
                return _commentsRepository;
            }
        }
        public Task SaveAsync() => _coffeeClubContext.SaveChangesAsync();
    }
}
