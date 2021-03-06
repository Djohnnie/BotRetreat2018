﻿using AutoMapper;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;

namespace BotRetreat2018.Mappers
{
    public class BotMapper : Mapper<Bot, BotDto>
    {
        private readonly IMapper _mapper;

        public BotMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bot, BotDto>();
                cfg.CreateMap<BotDto, Bot>();
            });
            _mapper = config.CreateMapper();
        }

        public override BotDto Map(Bot entity)
        {
            return _mapper.Map<BotDto>(entity);
        }

        public override Bot Map(BotDto dataTransferObject)
        {
            return _mapper.Map<Bot>(dataTransferObject);
        }
    }
}