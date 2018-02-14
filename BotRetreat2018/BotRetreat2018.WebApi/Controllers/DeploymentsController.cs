using System;
using System.Threading.Tasks;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Routes;
using BotRetreat2018.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2018.WebApi.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class DeploymentsController : ApiController<IDeploymentLogic>
    {
        public DeploymentsController(IDeploymentLogic deploymentsLogic) : base(deploymentsLogic) { }

        [HttpGet, Route(RouteConstants.POST_DEPLOYMENT_AVAILABLE)]
        public Task<IActionResult> Get(String teamName, String arenaName)
        {
            return Ok(l => l.Available(teamName, arenaName));
        }
    }
}