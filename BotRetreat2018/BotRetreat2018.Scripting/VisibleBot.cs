using System;
using BotRetreat2018.Model;
using BotRetreat2018.Scripting.Interfaces;

namespace BotRetreat2018.Scripting
{
    public class VisibleBot : IBot
    {
        public Orientation Orientation { get; }

        public Position Location { get; }

        public String Name { get; }

        public VisibleBot(Bot bot)
        {
            Orientation = bot.Orientation;
            Location = new Position { X = bot.LocationX, Y = bot.LocationY };
            Name = bot.Name;
        }

    }
}