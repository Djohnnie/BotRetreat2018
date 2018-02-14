using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Routes;
using BotRetreat2018.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2018.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class MessagesController : ApiController<IMessagesLogic>
    {
        public MessagesController(IMessagesLogic messagesLogic) : base(messagesLogic) { }

        [HttpGet, Route(RouteConstants.MESSAGES)]
        public Task<IActionResult> GetMessages(String arenaName)
        {
            return Ok(l => l.GetMessages(arenaName));
        }
    }
}