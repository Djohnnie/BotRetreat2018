using System;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Client.Interfaces
{
    public interface IScriptClient
    {
        Task<ScriptValidationDto> ValidateScript(ScriptDto script);
    }
}