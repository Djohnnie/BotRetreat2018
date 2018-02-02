using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface IHistoryLogic : ILogic
    {
        Task<List<HistoryDto>> GetHistoryByArenaId(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);

        Task<List<HistoryDto>> GetHistoryByBotId(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);
    }
}