﻿using BotRetreat2018.Contracts;
using BotRetreat2018.Mappers.Interfaces;
using BotRetreat2018.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BotRetreat2018.Mappers
{
    public static class MappersServiceCollectionExtensions
    {
        public static void AddBotRetreatMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMapper<Arena, ArenaDto>, ArenaMapper>();
            serviceCollection.AddTransient<IMapper<Arena, ArenaListDto>, ArenaListMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamDto>, TeamMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamRegistrationDto>, TeamRegistrationMapper>();
            serviceCollection.AddTransient<IMapper<Bot, BotDto>, BotMapper>();
            serviceCollection.AddTransient<IMapper<Team, TeamStatisticDto>, TeamStatisticMapper>();
            serviceCollection.AddTransient<IMapper<Bot, BotStatisticDto>, BotStatisticMapper>();
            serviceCollection.AddTransient<IMapper<History, HistoryDto>, HistoryMapper>();
            serviceCollection.AddTransient<IMapper<Message, MessageDto>, MessageMapper>();
        }
    }
}