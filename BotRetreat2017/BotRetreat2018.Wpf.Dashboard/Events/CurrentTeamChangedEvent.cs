using BotRetreat2018.Contracts;

namespace BotRetreat2018.Wpf.Dashboard.Events
{
    public class CurrentTeamChangedEvent
    {
        public TeamDto Team { get; }

        public CurrentTeamChangedEvent(TeamDto team)
        {
            Team = team;
        }
    }
}