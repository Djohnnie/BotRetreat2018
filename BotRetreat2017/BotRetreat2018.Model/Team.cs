using System;
using System.Collections.Generic;
using BotRetreat2018.Model.Interfaces;

namespace BotRetreat2018.Model
{
    public class Team : IEntity
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public String Name { get; set; }

        public String Password { get; set; }

        public Boolean Predator { get; set; }

        public virtual Arena PersonalArena { get; set; }

        public virtual ICollection<Bot> Bots { get; set; }
    }
}