﻿using System;
using BotRetreat2018.Contracts.Interfaces;

namespace BotRetreat2018.Contracts
{
    public class BotDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String TeamName { get; set; }

        public String Password { get; set; }

        public String ArenaName { get; set; }

        public Boolean Predator { get; set; }

        public String Name { get; set; }

        public Int16 LocationX { get; set; }

        public Int16 LocationY { get; set; }

        public OrientationDto Orientation { get; set; }

        public Int16 MaximumPhysicalHealth { get; set; }

        public Int16 CurrentPhysicalHealth { get; set; }

        public Int16 PhysicalHealthDrain { get; set; }

        public Int16 MaximumStamina { get; set; }

        public Int16 CurrentStamina { get; set; }

        public Int16 StaminaDrain { get; set; }

        public LastActionDto LastAction { get; set; }

        public Int16 LastAttackLocationX { get; set; }

        public Int16 LastAttackLocationY { get; set; }

        public String Script { get; set; }
    }
}