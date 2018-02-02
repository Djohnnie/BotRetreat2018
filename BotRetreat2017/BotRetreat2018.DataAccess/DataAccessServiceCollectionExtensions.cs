using Microsoft.Extensions.DependencyInjection;

namespace BotRetreat2018.DataAccess
{
    public static class DataAccessServiceCollectionExtensions
    {
        public static void AddBotRetreatDataAccess(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBotRetreatDbContext, BotRetreatDbContext>();
            serviceCollection.AddScoped<IBotRetreatHistoryDbContext, BotRetreatHistoryDbContext>();
        }
    }
}