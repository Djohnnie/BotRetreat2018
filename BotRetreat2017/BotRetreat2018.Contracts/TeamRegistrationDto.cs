using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class TeamRegistrationDto : IDataTransferObject
    {
        public String Name { get; set; }

        public String Password { get; set; }
    }
}