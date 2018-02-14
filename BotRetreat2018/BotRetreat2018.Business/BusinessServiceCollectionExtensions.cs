using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace BotRetreat2018.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static void AddBotRetreatBusiness(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArenaLogic, ArenaLogic>();
            serviceCollection.AddTransient<ITeamsLogic, TeamsLogic>();
            serviceCollection.AddTransient<IBotsLogic, BotsLogic>();
            serviceCollection.AddTransient<IDeploymentLogic, DeploymentLogic>();
            serviceCollection.AddTransient<IHistoryLogic, HistoryLogic>();
            serviceCollection.AddTransient<ILogLogic, LogLogic>();
            serviceCollection.AddTransient<IStatisticsLogic, StatisticsLogic>();
            serviceCollection.AddTransient<IGameLogic, GameLogic>();
            serviceCollection.AddTransient<IMessagesLogic, MessagesLogic>();
            serviceCollection.AddBotRetreatMappers();
            serviceCollection.AddBotRetreatDataAccess();
        }
    }
}