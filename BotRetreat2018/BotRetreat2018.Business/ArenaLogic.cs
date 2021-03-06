﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2018.Business.Base;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Mappers.Interfaces;
using BotRetreat2018.Model;
using BotRetreat2018.Contracts;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Utilities;
using Microsoft.EntityFrameworkCore;
using Crypt = BCrypt.Net.BCrypt;
using BotRetreat2018.Business.Exceptions;

namespace BotRetreat2018.Business
{
    public class ArenaLogic : Logic<IBotRetreatDbContext>, IArenaLogic
    {
        private readonly IMapper<Arena, ArenaDto> _arenaMapper;
        private readonly IMapper<Arena, ArenaListDto> _arenaListMapper;

        public ArenaLogic(IBotRetreatDbContext dbContext, IMapper<Arena, ArenaDto> arenaMapper, IMapper<Arena, ArenaListDto> arenaListMapper) : base(dbContext)
        {
            _arenaMapper = arenaMapper;
            _arenaListMapper = arenaListMapper;
        }

        public async Task<List<TopTeamDto>> GetTopTeams(String arenaName)
        {
            using (var sw = new SimpleStopwatch())
            {
                var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
                if (arena == null) throw new BusinessException("Specified arena does not exist!");
                var teams = await _dbContext.Teams.Where(x => x.Bots.Any(b => b.Arena.Id == arena.Id))
                    .Include(x => x.Bots).ThenInclude(x => x.Arena).ToListAsync();

                var topTeams = teams.Select(x => new TopTeamDto
                {
                    TeamName = x.Name,
                    NumberOfKills = x.Bots.Where(d => d.Arena.Id == arena.Id).Sum(b => b.Kills),
                    AverageBotLife = TimeSpan.FromMilliseconds(x.Bots.Where(d => d.Arena.Id == arena.Id)
                            .Select(s => ((s.TimeOfDeath ?? DateTime.UtcNow) - s.TimeOfBirth).TotalMilliseconds)
                            .AverageOrDefault()).ToString()
                }).ToList();

                Debug.WriteLine($"GetTopTeams - {sw.ElapsedMilliseconds}ms");
                Console.WriteLine($"GetTopTeams - {sw.ElapsedMilliseconds}ms");
                return topTeams.OrderByDescending(x => x.NumberOfKills).ThenByDescending(x => x.AverageBotLife).Take(3).ToList();
            }
        }

        public async Task<List<ArenaDto>> GetAllArenas()
        {
            return _arenaMapper.Map(await _dbContext.Arenas.ToListAsync());
        }

        public async Task<List<ArenaListDto>> GetArenasList()
        {
            return _arenaListMapper.Map(
                await _dbContext.Arenas.ToListAsync());
        }

        public async Task<List<ArenaDto>> GetAvailableArenas()
        {
            return _arenaMapper.Map(
                await _dbContext.Arenas.Where(x => x.Active && !x.Private).ToListAsync());
        }

        public async Task<ArenaDto> GetTeamArena(String teamName, String teamPassword)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName);
            if (team != null && Crypt.EnhancedVerify(teamPassword, team.Password))
            {
                return _arenaMapper.Map(
                    await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == teamName));
            }
            return null;
        }

        public async Task<List<ArenaDto>> GetTeamArenas(String teamName, String teamPassword)
        {
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName);
            if (team != null && Crypt.EnhancedVerify(teamPassword, team.Password))
            {
                var arenas = _arenaMapper.Map(
                    await _dbContext.Arenas.Where(x => x.Active && (!x.Private || x.Name == teamName)).ToListAsync());
                foreach (var arena in arenas)
                {
                    var lastDeployment = await _dbContext.Bots.Where(x => x.Arena.Id == arena.Id && x.Team.Id == team.Id)
                            .OrderByDescending(x => x.DeploymentDateTime)
                            .FirstOrDefaultAsync();
                    arena.LastDeploymentDateTime = lastDeployment?.DeploymentDateTime;
                }
                return arenas;
            }
            return null;
        }

        public async Task<ArenaDto> CreateArena(ArenaDto arena)
        {
            arena.Active = true;
            arena.LastRefreshDateTime = DateTime.UtcNow;
            var arenaEntity = _arenaMapper.Map(arena);
            _dbContext.Arenas.Add(arenaEntity);
            await _dbContext.SaveChangesAsync();
            return _arenaMapper.Map(
                await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arenaEntity.Id));
        }

        public async Task<ArenaDto> EditArena(ArenaDto arena)
        {
            _dbContext.Arenas.Attach(_arenaMapper.Map(arena));
            await _dbContext.SaveChangesAsync();
            return _arenaMapper.Map(
                await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arena.Id));
        }

        public async Task RemoveArena(Guid arenaId)
        {
            var existingArena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Id == arenaId);
            if (existingArena != null)
            {
                var bots = await _dbContext.Bots.Where(x => x.Arena.Id == existingArena.Id).ToListAsync();
                bots.ForEach(bot => _dbContext.Bots.Remove(bot));
                _dbContext.Arenas.Remove(existingArena);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}