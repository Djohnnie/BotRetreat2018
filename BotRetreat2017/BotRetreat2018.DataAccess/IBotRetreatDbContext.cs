using BotRetreat2018.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2018.DataAccess
{
    public interface IBotRetreatDbContext : IDbContext
    {
        DbSet<Team> Teams { get; set; }

        DbSet<Arena> Arenas { get; set; }

        DbSet<Bot> Bots { get; set; }

        DbSet<Deployment> Deployments { get; set; }
    }
}