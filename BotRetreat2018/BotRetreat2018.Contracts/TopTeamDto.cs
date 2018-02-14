using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class TopTeamDto : IDataTransferObject
    {
        public String TeamName { get; set; }
        public Int32 NumberOfKills { get; set; }
        public String AverageBotLife { get; set; }
    }
}