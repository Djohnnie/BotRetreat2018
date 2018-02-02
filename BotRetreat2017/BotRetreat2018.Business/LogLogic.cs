using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Base;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Model;
using BotRetreat2018.Utilities;

namespace BotRetreat2018.Business
{
    public class LogLogic : Logic<IBotRetreatHistoryDbContext>, ILogLogic
    {
        private readonly Object _syncRoot = new Object();

        public LogLogic(IBotRetreatHistoryDbContext dbContext) : base(dbContext)
        {
        }

        private async Task Log(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description, HistoryType type)
        {
            var history = new History
            {
                ArenaId = arena?.Id,
                DeploymentId = deployment?.Id,
                BotId = bot?.Id,
                Name = name.GetName(),
                Description = name.GetDescription(),
                DateTime = DateTime.UtcNow
            };
            await _dbContext.History.AddAsync(history);
        }

        private async Task Log(Arena arena, Deployment deployment, Bot bot, String description, HistoryType type)
        {
            var history = new History
            {
                ArenaId = arena?.Id,
                DeploymentId = deployment?.Id,
                BotId = bot?.Id,
                Name = "Timing",
                Description = description,
                DateTime = DateTime.UtcNow
            };
            await _dbContext.History.AddAsync(history);
        }

        public Task LogMessage(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            return Log(arena, deployment, bot, name, description, HistoryType.Message);
        }

        public Task LogWarning(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            return Log(arena, deployment, bot, name, description, HistoryType.Warning);
        }

        public Task LogError(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            return Log(arena, deployment, bot, name, description, HistoryType.Error);
        }

        public Task LogTiming(Arena arena, Deployment deployment, Bot bot, String description)
        {
            return Log(arena, deployment, bot, description, HistoryType.Timing);
        }

        public Task SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}