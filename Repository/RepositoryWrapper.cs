using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private CoffeeClubContext _repoContext;
        private ICoffeeRepository _coffee;
        private ICommentsRepository _comment;
        
        public ICoffeeRepository Coffee
        {
            get
            {
                if (_coffee == null)
                {
                    _coffee = new CoffeeRepository(_repoContext);
                }

                return _coffee;
            }
        }

        public ICommentsRepository Comments
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentsRepository(_repoContext);
                }

                return _comment;
            }
        }

        public RepositoryWrapper(CoffeeClubContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
