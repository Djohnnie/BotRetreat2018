using System;
using System.Threading.Tasks;
using BotRetreat2018.Model;

namespace BotRetreat2018.Business.Interfaces
{
    public interface ILogLogic
    {
        Task LogMessage(Arena arena, Bot bot, HistoryName name, String description = null);

        Task LogWarning(Arena arena, Bot bot, HistoryName name, String description = null);

        Task LogError(Arena arena, Bot bot, HistoryName name, String description = null);

        Task LogTiming(Arena arena, Bot bot, String description);

        Task SaveChanges();
    }
}