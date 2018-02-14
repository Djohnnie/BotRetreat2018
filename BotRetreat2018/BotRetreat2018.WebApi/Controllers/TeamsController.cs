using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Contracts;
using BotRetreat2018.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2018.WebApi.Controllers
{
    [Route("api")]
    public class TeamsController : ApiController<ITeamsLogic>
    {
        public TeamsController(ITeamsLogic teamsLogic) : base(teamsLogic) { }

        [HttpGet, Route("teams/all")]
        public Task<IActionResult> GetAllTeams()
        {
            return Ok(l => l.GetAllTeams());
        }

        [HttpGet, Route("teams/{name}/{password}")]
        public Task<IActionResult> GetTeam(String name, String password)
        {
            return Ok(l => l.GetTeam(name, password));
        }

        [HttpPost, Route("teams")]
        public Task<IActionResult> CreateTeam([FromBody]TeamRegistrationDto team)
        {
            return Ok(l => l.CreateTeam(team));
        }
    }
}