﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2018.Business.Base;
using BotRetreat2018.Business.Exceptions;
using BotRetreat2018.Business.Interfaces;
using BotRetreat2018.Contracts;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Mappers.Interfaces;
using BotRetreat2018.Model;
using BotRetreat2018.Utilities;
using Microsoft.EntityFrameworkCore;
using Crypt = BCrypt.Net.BCrypt;

namespace BotRetreat2018.Business
{
    public class BotsLogic : Logic<IBotRetreatDbContext>, IBotsLogic
    {
        private readonly IMapper<Bot, BotDto> _botMapper;

        public BotsLogic(IBotRetreatDbContext dbContext, IMapper<Bot, BotDto> botMapper) : base(dbContext)
        {
            _botMapper = botMapper;
        }

        public async Task<List<BotDto>> GetAllBots()
        {
            return _botMapper.Map(await _dbContext.Bots.ToListAsync());
        }

        public async Task<BotDto> CreateBot(BotDto bot)
        {
            var existingBot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Name == bot.Name);
            if (existingBot != null) { throw new BusinessException($"Bot with name {bot.Name} already exists!"); }

            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == bot.TeamName);
            if (team == null) { throw new BusinessException($"Team '{bot.TeamName}' is unknown!"); }

            if (!Crypt.EnhancedVerify(bot.Password, team.Password))
            {
                throw new BusinessException($"Password for '{bot.TeamName}' is incorrect!");
            }

            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == bot.ArenaName);
            if (arena == null) { throw new BusinessException($"Arena '{bot.ArenaName}' does not exist!"); }

            if (bot.MaximumPhysicalHealth < 0 || bot.MaximumStamina < 0)
            {
                throw new BusinessException($"Number of assigned bot points is not valid!");
            }

            var assignedPoints = bot.MaximumStamina + bot.MaximumPhysicalHealth;
            if (assignedPoints > arena.MaximumPoints)
            {
                throw new BusinessException($"Number of assigned bot points ({assignedPoints}) is larger than maximum allowed ({arena.MaximumPoints})!");
            }

            var decodedScript = bot.Script.Base64Decode();
            var blockedTokens = new[] { "CSharpCompilation", "dynamic" };
            if (blockedTokens.Any(x => decodedScript.Contains(x)))
            {
                throw new BusinessException("Script blocked!");
            }

            var lastDeployment = await _dbContext.Bots
                .Where(x => x.Arena.Id == arena.Id)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync();

            if (lastDeployment != null && !team.Predator)
            {
                var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
                if (timeSinceLastDeployment < arena.DeploymentRestriction && !bot.Predator)
                {
                    throw new BusinessException($"Deployment restriction of {arena.DeploymentRestriction} applies!");
                }
            }

            var botEntity = _botMapper.Map(bot);
            botEntity.TimeOfBirth = DateTime.UtcNow;
            botEntity.DeploymentDateTime = DateTime.UtcNow;
            botEntity.Arena = arena;
            botEntity.Team = team;

            var existingBots = await _dbContext.Bots.Where(x => x.Arena.Id == arena.Id)
                    .Where(x => x.CurrentPhysicalHealth > 0)
                    .Select(b => new { b.LocationX, b.LocationY }).ToListAsync();
            var randomGenerator = new Random();
            var locationFound = false;
            while (!locationFound)
            {
                botEntity.LocationX = (Int16)randomGenerator.Next(0, arena.Width);
                botEntity.LocationY = (Int16)randomGenerator.Next(0, arena.Height);
                botEntity.Orientation = (Orientation)randomGenerator.Next(0, 4);
                if (!existingBots.Any(l => l.LocationX == bot.LocationX && l.LocationY == bot.LocationY))
                {
                    locationFound = true;
                }
            }

            _dbContext.Bots.Add(botEntity);
            await _dbContext.SaveChangesAsync();
            return _botMapper.Map(
                await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == botEntity.Id));
        }
    }
}