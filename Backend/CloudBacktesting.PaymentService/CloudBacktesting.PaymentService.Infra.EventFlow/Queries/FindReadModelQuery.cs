

using CloudBacktesting.Infra.EventFlow.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores;
using System;
using System.Linq.Expressions;

namespace CloudBacktesting.Infra.EventFlow.Queries
{
    public class FindReadModelQuery<TReadModel> : IQuery<ICollectionReadModel<TReadModel>>, IQuery
        where TReadModel : class, IReadModel
    {
        public Expression<Func<TReadModel, bool>> Predicate { get; }

        public FindReadModelQuery(Expression<Func<TReadModel, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
