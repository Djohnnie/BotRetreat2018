using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface ITeamsLogic : ILogic
    {
        Task<List<TeamDto>> GetAllTeams();

        Task<TeamDto> GetTeam(String name, String password);

        Task<TeamDto> CreateTeam(TeamRegistrationDto team);
    }
}