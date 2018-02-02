using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Routes;
using BotRetreat2018.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2018.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class HistoryController : ApiController<IHistoryLogic>
    {
        public HistoryController(IHistoryLogic historyLogic) : base(historyLogic) { }

        [HttpGet, Route(RouteConstants.GET_HISTORY_BY_ARENA)]
        public Task<IActionResult> GetByArena(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return Ok(l => l.GetHistoryByArenaId(arenaId, fromDateTime, untilDateTime));
        }

       [HttpGet, Route(RouteConstants.GET_HISTORY_BY_BOT)]
        public Task<IActionResult> GetByBot(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return Ok(l => l.GetHistoryByBotId(botId, fromDateTime, untilDateTime));
        }
    }
}