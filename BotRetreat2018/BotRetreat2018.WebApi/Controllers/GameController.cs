using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Routes;
using BotRetreat2018.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2018.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class GameController : ApiController<IGameLogic>
    {
        public GameController(IGameLogic gameLogic) : base(gameLogic) { }

        [HttpGet, Route(RouteConstants.GET_GAME)]
        public Task<IActionResult> GetGameForArena(String arenaName)
        {
            return Ok(l => l.GetGameForArena(arenaName));
        }
    }
}