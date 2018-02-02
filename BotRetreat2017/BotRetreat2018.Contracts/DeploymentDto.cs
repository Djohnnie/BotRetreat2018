using System;

namespace BotRetreat2018.Contracts
{
    public class DeploymentDto
    {
        public Guid Id { get; set; }
        public String BotName { get; set; }
        public String TeamName { get; set; }
        public String ArenaName { get; set; }
    }
}