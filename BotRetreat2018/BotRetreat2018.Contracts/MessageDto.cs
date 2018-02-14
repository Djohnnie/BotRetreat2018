using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class MessageDto : IDataTransferObject
    {
        public String Message { get; set; }

        public String BotName { get; set; }
    }
}