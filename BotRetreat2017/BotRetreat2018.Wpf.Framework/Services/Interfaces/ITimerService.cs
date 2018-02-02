using System;

namespace BotRetreat2018.Wpf.Framework.Services.Interfaces
{
    public interface ITimerService : IDisposable
    {
        ITimerToken Start(TimeSpan interval, Action action);
    }
}