using System;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2018.Business.Base;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2018.Business
{
    public class DeploymentLogic : Logic<IBotRetreatDbContext>, IDeploymentLogic
    {
        private readonly ILogLogic _logLogic;

        public DeploymentLogic(IBotRetreatDbContext dbContext, ILogLogic logLogic) : base(dbContext)
        {
            _logLogic = logLogic;
        }

        public async Task<Boolean> Available(String teamName, String arenaName)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName);

            var lastDeployment = await _dbContext.Bots
                .Where(x => x.Team.Name == teamName)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync(x => x.Arena.Name == arenaName);

            if (team.Predator || lastDeployment == null)
            {
                return true;
            }

            var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
            if (timeSinceLastDeployment > arena.DeploymentRestriction)
            {
                return true;
            }

            return false;
        }
    }
}