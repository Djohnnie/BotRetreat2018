using BotRetreat2018.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2018.DataAccess
{
    public interface IBotRetreatHistoryDbContext : IDbContext
    {
        DbSet<History> History { get; set; }
    }
}