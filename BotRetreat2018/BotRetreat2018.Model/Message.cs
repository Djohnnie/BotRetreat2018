using System;
using BotRetreat2018.Model.Interfaces;

namespace BotRetreat2018.Model
{
    public class Message : IEntity
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public String Content { get; set; }

        public DateTime DateTime { get; set; }

        public Bot Bot { get; set; }
    }
}