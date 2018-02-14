using System.Collections.Generic;
using BotRetreat2018.Model;
using BotRetreat2018.Scripting.Interfaces;

namespace BotRetreat2018.Scripting
{
    public class FieldOfView : IFieldOfView
    {
        public List<IBot> Bots { get; }
        public List<IBot> FriendlyBots { get; }
        public List<IBot> EnemyBots { get; }
        public List<IBot> PredatorBots { get; }

        public FieldOfView(Arena arena, Bot bot, List<Bot> bots)
        {
            Bots = new List<IBot>();
            FriendlyBots = new List<IBot>();
            EnemyBots = new List<IBot>();
            PredatorBots = new List<IBot>();
            var currentTeamName = bot.Team.Name;
            var minimumX = 0;
            var minimumY = 0;
            var maximumX = arena.Width - 1;
            var maximumY = arena.Height - 1;
            switch (bot.Orientation)
            {
                case Orientation.North:
                    minimumX = 0;
                    maximumX = arena.Width - 1;
                    minimumY = 0;
                    maximumY = bot.LocationY - 1;
                    break;
                case Orientation.East:
                    minimumX = bot.LocationX + 1;
                    maximumX = arena.Width - 1;
                    minimumY = 0;
                    maximumY = arena.Height - 1;
                    break;
                case Orientation.South:
                    minimumX = 0;
                    maximumX = arena.Width - 1;
                    minimumY = bot.LocationY + 1;
                    maximumY = arena.Height - 1;
                    break;
                case Orientation.West:
                    minimumX = 0;
                    maximumX = bot.LocationX - 1;
                    minimumY = 0;
                    maximumY = arena.Height - 1;
                    break;
            }
            foreach (var otherBot in bots)
            {
                if (otherBot.Id != bot.Id)
                {
                    if (otherBot.LocationX >= minimumX && otherBot.LocationX <= maximumX &&
                        otherBot.LocationY >= minimumY && otherBot.LocationY <= maximumY)
                    {
                        Bots.Add(new VisibleBot(otherBot));
                        var botTeamName = otherBot.Team.Name;
                        if (botTeamName == currentTeamName)
                        {
                            FriendlyBots.Add(new VisibleBot(otherBot));
                        }
                        else
                        {
                            EnemyBots.Add(new VisibleBot(otherBot));
                        }
                    }
                }
            }
        }
    }
}