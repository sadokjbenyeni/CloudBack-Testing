using EventFlow.ReadStores;
using System.Collections.Generic;

namespace CloudBacktesting.Infra.EventFlow.ReadStores
{
    public interface ICollectionReadModel<TReadModel> : IReadModel, IEnumerable<TReadModel>
            where TReadModel : IReadModel
    {
    }
}
