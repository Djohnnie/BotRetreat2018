using System;

namespace BotRetreat2018.Wpf.Dashboard.Events
{
    public class IsBusyChangedEvent
    {
        public Boolean IsBusy { get; set; }

        public IsBusyChangedEvent(Boolean isBusy)
        {
            IsBusy = isBusy;
        }
    }
}