using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class ArenaListDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }
}