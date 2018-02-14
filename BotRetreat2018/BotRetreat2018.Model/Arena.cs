using System;
using System.Collections.Generic;
using BotRetreat2018.Model.Interfaces;

namespace BotRetreat2018.Model
{
    public class Arena : IEntity
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public String Name { get; set; }

        public Boolean Active { get; set; }

        public Int16 Width { get; set; }

        public Int16 Height { get; set; }

        public Boolean Private { get; set; }

        public TimeSpan DeploymentRestriction { get; set; }

        public DateTime LastRefreshDateTime { get; set; }

        public Int32 MaximumPoints { get; set; }

        public virtual ICollection<Bot> Bots { get; set; }
    }
}