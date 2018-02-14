using BotRetreat2018.Wpf.Dashboard.Unity;
using Unity;

namespace BotRetreat2018.Wpf.Dashboard.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
           => UnityConfiguration.Container.Resolve<MainViewModel>();

        public TeamStatisticsViewModel TeamStatisticsViewModel
           => UnityConfiguration.Container.Resolve<TeamStatisticsViewModel>();

        public BotStatisticsViewModel BotStatisticsViewModel
           => UnityConfiguration.Container.Resolve<BotStatisticsViewModel>();

        public BotDeploymentViewModel BotDeploymentViewModel
           => UnityConfiguration.Container.Resolve<BotDeploymentViewModel>();
    }
}