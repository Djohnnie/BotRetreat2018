using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface IMessagesLogic : ILogic
    {
        Task<List<MessageDto>> GetMessages(String arenaName);
    }
}