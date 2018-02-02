using System;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface IGameLogic : ILogic
    {
        Task<GameDto> GetGameForArena(String arenaName);
    }
}