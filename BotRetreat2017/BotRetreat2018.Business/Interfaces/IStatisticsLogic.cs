using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface IStatisticsLogic : ILogic
    {
        Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword);

        Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName);
    }
}