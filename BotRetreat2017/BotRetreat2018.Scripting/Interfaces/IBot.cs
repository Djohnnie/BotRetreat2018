using System;
using BotRetreat2018.Model;

namespace BotRetreat2018.Scripting.Interfaces
{
    public interface IBot
    {
        Orientation Orientation { get; }

        Position Location { get; }

        String Name { get; }
    }
}