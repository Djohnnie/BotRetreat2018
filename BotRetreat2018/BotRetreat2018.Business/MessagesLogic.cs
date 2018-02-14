using System;
using System.Collections.Generic;
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
    public class MessagesLogic : Logic<IBotRetreatDbContext>, IMessagesLogic
    {
        private readonly IMapper<Message, MessageDto> _messageMapper;

        public MessagesLogic(IBotRetreatDbContext dbContext, IMapper<Message, MessageDto> messageMapper) : base(dbContext)
        {
            _messageMapper = messageMapper;
        }

        public async Task<List<MessageDto>> GetMessages(String arenaName)
        {
            using (var sw = new SimpleStopwatch())
            {
                var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
                if (arena == null) return new List<MessageDto>();

                var lastMessages = await _dbContext.Messages.Where(x => x.Bot.Arena.Id == arena.Id)
                    .Include(x => x.Bot.Name).OrderByDescending(x => x.DateTime).Take(10).ToListAsync();
                List<MessageDto> messages = _messageMapper.Map(lastMessages);

                Debug.WriteLine($"GetMessages - {sw.ElapsedMilliseconds}ms");
                Console.WriteLine($"GetMessages - {sw.ElapsedMilliseconds}ms");
                return messages;
            }
        }
    }
}