using AutoMapper;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;

namespace BotRetreat2018.Mappers
{
    public class MessageMapper : Mapper<Message, MessageDto>
    {
        private readonly IMapper _mapper;

        public MessageMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Message, MessageDto>()
                    .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content))
                    .ForMember(dest => dest.BotName, opt => opt.MapFrom(src => src.Bot.Name));
                cfg.CreateMap<MessageDto, Message>();
            });
            _mapper = config.CreateMapper();
        }

        public override MessageDto Map(Message entity)
        {
            return _mapper.Map<MessageDto>(entity);
        }

        public override Message Map(MessageDto dataTransferObject)
        {
            return _mapper.Map<Message>(dataTransferObject);
        }
    }
}