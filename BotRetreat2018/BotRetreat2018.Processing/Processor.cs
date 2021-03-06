﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BotRetreat2018.DataAccess;
using BotRetreat2018.Model;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using BotRetreat2018.Scripting;
using BotRetreat2018.Scripting.Extensions;
using BotRetreat2018.Utilities;

namespace BotRetreat2018.Processing
{
    public class Processor
    {
        private readonly BotScriptCache _cache = new BotScriptCache();

        public Task GoPublic()
        {
            return Go(arena => !arena.Private);
        }

        public Task GoPrivate()
        {
            return Go(arena => arena.Private);
        }

        public async Task Go(Expression<Func<Arena, Boolean>> arenaPredicate)
        {
            using (var dbContext = new BotRetreatDbContext())
            {
                List<Arena> arenas = await dbContext.Arenas.Where(arenaPredicate).ToListAsync();
                foreach (Arena arena in arenas)
                {
                    List<Bot> bots = await dbContext.Bots.Where(x =>
                            !x.TimeOfDeath.HasValue && x.Arena.Id == arena.Id)
                            .Include(x => x.Arena).Include(x => x.Team)
                            .OrderByDescending(x => x.CurrentStamina)
                            .ThenByDescending(x => x.DeploymentDateTime).ToListAsync();

                    List<Message> messages = new List<Message>();

                    // Get all health statistics from all bots before the iteration.
                    var botStats = bots.GetBotStats();

                    foreach (Bot bot in bots)
                    {
                        await GoBot(bot, arena, bots, messages);
                    }

                    // Update last attack location
                    bots.UpdateLastAttackLocation();

                    // Update all health statistics from all bots after the iteration.
                    bots.UpdateStatDrains(botStats);

                    // Add all messages to the database.
                    dbContext.Messages.AddRange(messages);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task GoBot(Bot bot, Arena arena, List<Bot> bots, List<Message> messages)
        {
            Script botScript = GetCompiledBotScript(bot);
            ScriptGlobals globals = new ScriptGlobals(arena, bot, bots, messages);

            try
            {
                await botScript.RunAsync(globals);
                bot.UpdateBot(globals);
            }
            catch
            {
                // Even if there are errors in the botscript, persist memory.
                bot.Memory = globals.Memory.Serialize();
                // Last action was a script error.
                bot.LastAction = LastAction.ScriptError;
            }
        }

        private Script GetCompiledBotScript(Bot bot)
        {
            if (!_cache.ScriptStored(bot))
            {
                var botScript = BotScript.PrepareScript(bot.Script);
                botScript.Compile();
                _cache.StoreScript(bot, botScript);
            }

            return _cache.LoadScript(bot);
        }
    }
}