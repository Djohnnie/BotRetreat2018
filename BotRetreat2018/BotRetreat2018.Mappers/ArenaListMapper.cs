using AutoMapper;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;

namespace BotRetreat2018.Mappers
{
    public class ArenaListMapper : Mapper<Arena, ArenaListDto>
    {
        private readonly IMapper _mapper;

        public ArenaListMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Arena, ArenaListDto>();
                cfg.CreateMap<ArenaListDto, Arena>();
            });
            _mapper = config.CreateMapper();
        }

        public override ArenaListDto Map(Arena entity)
        {
            return _mapper.Map<ArenaListDto>(entity);
        }

        public override Arena Map(ArenaListDto dataTransferObject)
        {
            return _mapper.Map<Arena>(dataTransferObject);
        }
    }
}