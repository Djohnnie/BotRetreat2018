using BotRetreat2018.DataAccess;

namespace BotRetreat2018.Business.Base
{
    public abstract class Logic<TContext> where TContext : IDbContext
    {
        protected readonly TContext _dbContext;

        protected Logic(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.InitializeDatabase();
        }
    }
}