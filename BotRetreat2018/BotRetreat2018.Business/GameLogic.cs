using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2018.Business.Base;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Contracts;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Mappers.Interfaces;
using BotRetreat2018.Model;
using BotRetreat2018.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2018.Business
{
    public class GameLogic : Logic<IBotRetreatDbContext>, IGameLogic
    {
        private readonly IMapper<Arena, ArenaDto> _arenaMapper;
        private readonly IMapper<Bot, BotDto> _botMapper;
        private readonly IMapper<History, HistoryDto> _historyMapper;

        public GameLogic(IBotRetreatDbContext dbContext, IMapper<Arena, ArenaDto> arenaMapper, IMapper<Bot, BotDto> botMapper, IMapper<History, HistoryDto> historyMapper) : base(dbContext)
        {
            _arenaMapper = arenaMapper;
            _botMapper = botMapper;
            _historyMapper = historyMapper;
        }

        public async Task<GameDto> GetGameForArena(String arenaName)
        {
            using (var sw = new SimpleStopwatch())
            {
                var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
                if (arena == null) return new GameDto();
                var bots = await _dbContext.Bots.Where(x => x.Arena.Id == arena.Id)
                    .Where(x => !x.TimeOfDeath.HasValue || (DateTime.UtcNow - x.TimeOfDeath.Value).TotalMinutes < 2)
                    .Include(x => x.Team).ToListAsync();

                bots.ForEach(x =>
                {
                    x.Script = String.Empty;
                    x.Name = $"{x.Name} ({x.Team.Name})";
                });

                Debug.WriteLine($"GetGameForArena - {sw.ElapsedMilliseconds}ms");
                Console.WriteLine($"GetGameForArena - {sw.ElapsedMilliseconds}ms");

                return new GameDto
                {
                    Arena = _arenaMapper.Map(arena),
                    Bots = _botMapper.Map(bots),
                    //History = _historyMapper.Map(history)
                };
            }
        }
    }
}