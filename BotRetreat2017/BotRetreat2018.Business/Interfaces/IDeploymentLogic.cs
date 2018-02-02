using System;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;

namespace BotRetreat2018.Business.Interfaces
{
    public interface IDeploymentLogic : ILogic
    {
        Task<DeploymentDto> Deploy(DeploymentDto deployment);

        Task<Boolean> Available(String teamName, String arenaName);
    }
}