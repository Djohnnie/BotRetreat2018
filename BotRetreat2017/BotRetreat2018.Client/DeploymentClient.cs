using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Client.Base;
using BotRetreat2018.Client.Interfaces;
using BotRetreat2018.Contracts;
using BotRetreat2018.Routes;

namespace BotRetreat2018.Client
{
    public class DeploymentClient : ClientBase, IDeploymentClient
    {
        public Task<DeploymentDto> Deploy(DeploymentDto deployment)
        {
            return Post<DeploymentDto, DeploymentDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT}", deployment);
        }

        public Task<Boolean> Available(String teamName, String arenaName)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(arenaName)] = arenaName
            };
            return Get<Boolean>(
              $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT_AVAILABLE}", parameters);
        }
    }
}