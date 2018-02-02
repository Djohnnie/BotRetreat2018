using System.Collections.Generic;
using BotRetreat2018.Contracts.Interfaces;
using BotRetreat2018.Model.Interfaces;

namespace BotRetreat2018.Mappers.Interfaces
{
    public interface IMapper<TEntity, TDataTransferObject>
        where TEntity : IEntity
        where TDataTransferObject : IDataTransferObject
    {
        TDataTransferObject Map(TEntity entity);

        TEntity Map(TDataTransferObject dataTransferObject);

        List<TDataTransferObject> Map(List<TEntity> entities);

        List<TEntity> Map(List<TDataTransferObject> dataTransferObjects);
    }
}