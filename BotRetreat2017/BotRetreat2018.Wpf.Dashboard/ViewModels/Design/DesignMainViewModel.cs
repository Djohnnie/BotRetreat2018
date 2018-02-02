using Reactive.EventAggregator;

namespace BotRetreat2018.Wpf.Dashboard.ViewModels.Design
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base(new EventAggregator()) { }
    }
}