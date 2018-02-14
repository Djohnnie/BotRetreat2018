using System;
using System.Threading.Tasks;
using BotRetreat2018.Client.Base;
using BotRetreat2018.Client.Interfaces;
using BotRetreat2018.Contracts;
using BotRetreat2018.Routes;

namespace BotRetreat2018.Client
{
    public class ScriptClient : ClientBase, IScriptClient
    {
        public ScriptClient() : base("http://my.djohnnie.be:8991") { }

        public Task<ScriptValidationDto> ValidateScript(ScriptDto script)
        {
            return Post<ScriptValidationDto, ScriptDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_SCRIPT_VALIDATION}", script);
        }
    }
}