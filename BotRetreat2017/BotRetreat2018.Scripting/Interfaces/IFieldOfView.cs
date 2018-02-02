using System.Collections.Generic;

namespace BotRetreat2018.Scripting.Interfaces
{
    public interface IFieldOfView
    {
        List<IBot> Bots { get; }
        List<IBot> FriendlyBots { get; }
        List<IBot> EnemyBots { get; }
        List<IBot> PredatorBots { get; }
    }
}