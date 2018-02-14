using AutoMapper;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;

namespace BotRetreat2018.Mappers
{
    public class TeamMapper : Mapper<Team, TeamDto>
    {
        private readonly IMapper _mapper;

        public TeamMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamDto>();
                cfg.CreateMap<TeamDto, Team>();
            });
            _mapper = config.CreateMapper();
        }

        public override TeamDto Map(Team entity)
        {
            return _mapper.Map<TeamDto>(entity);
        }

        public override Team Map(TeamDto dataTransferObject)
        {
            return _mapper.Map<Team>(dataTransferObject);
        }
    }
}