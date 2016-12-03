using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace TicTacToeWeb.EntityFramework.Repositories
{
    public abstract class TicTacToeWebRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TicTacToeWebDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TicTacToeWebRepositoryBase(IDbContextProvider<TicTacToeWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class TicTacToeWebRepositoryBase<TEntity> : TicTacToeWebRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TicTacToeWebRepositoryBase(IDbContextProvider<TicTacToeWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
