using System;
using System.Collections.Generic;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class TeamDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public List<TeamStatisticDto> Statistics { get; set; }
    }
}