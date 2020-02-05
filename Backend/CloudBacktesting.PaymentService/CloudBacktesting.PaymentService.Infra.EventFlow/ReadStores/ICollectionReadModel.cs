using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.Infra.EventFlow.ReadStores
{
    public interface ICollectionReadModel<TReadModel> : IReadModel, IEnumerable<TReadModel>
            where TReadModel : IReadModel
    {
    }
}
