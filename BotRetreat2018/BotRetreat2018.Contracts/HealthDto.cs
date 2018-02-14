using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class HealthDto : IDataTransferObject
    {
        public Int16 Maximum { get; set; }

        public Int16 Current { get; set; }

        public Int16 Drain { get; set; }
    }
}