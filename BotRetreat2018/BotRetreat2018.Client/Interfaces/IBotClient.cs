using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Client.Interfaces
{
    public interface IBotClient
    {
        Task<List<BotDto>> GetAllBots();

        Task<BotDto> CreateBot(BotDto bot);

        Task<BotDto> EditBot(BotDto bot);

        Task RemoveBot(Guid botId);
    }
}