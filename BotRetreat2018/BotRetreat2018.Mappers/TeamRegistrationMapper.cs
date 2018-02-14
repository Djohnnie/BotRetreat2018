using AutoMapper;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;

namespace BotRetreat2018.Mappers
{
    public class TeamRegistrationMapper : Mapper<Team, TeamRegistrationDto>
    {
        private readonly IMapper _mapper;

        public TeamRegistrationMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamRegistrationDto>();
                cfg.CreateMap<TeamRegistrationDto, Team>();
            });
            _mapper = config.CreateMapper();
        }

        public override TeamRegistrationDto Map(Team entity)
        {
            return _mapper.Map<TeamRegistrationDto>(entity);
        }

        public override Team Map(TeamRegistrationDto dataTransferObject)
        {
            return _mapper.Map<Team>(dataTransferObject);
        }
    }
}