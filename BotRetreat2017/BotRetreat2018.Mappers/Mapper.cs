using System.Collections.Generic;
using System.Linq;
using BotRetreat2018.Contracts.Interfaces;
using BotRetreat2018.Mappers.Interfaces;
using BotRetreat2018.Model.Interfaces;

namespace BotRetreat2018.Mappers
{
    public abstract class Mapper<TEntity, TDataTransferObject> : IMapper<TEntity, TDataTransferObject>
        where TEntity : IEntity
        where TDataTransferObject : IDataTransferObject
    {
        public abstract TDataTransferObject Map(TEntity entity);

        public abstract TEntity Map(TDataTransferObject dataTransferObject);

        public List<TDataTransferObject> Map(List<TEntity> entities)
        {
            return entities.Select(Map).ToList();
        }

        public List<TEntity> Map(List<TDataTransferObject> dataTransferObjects)
        {
            return dataTransferObjects.Select(Map).ToList();
        }
    }
}