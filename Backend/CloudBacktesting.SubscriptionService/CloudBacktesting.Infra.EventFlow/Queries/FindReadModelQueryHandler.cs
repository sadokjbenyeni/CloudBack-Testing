using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace CloudBacktesting.Infra.EventFlow.Queries
{
    public abstract class FindReadModelQueryHandler<TReadModel> : IQueryHandler<FindReadModelQuery<TReadModel>, ICollectionReadModel<TReadModel>>, IQueryHandler
        where TReadModel : class, IReadModel
    {
        public Task<ICollectionReadModel<TReadModel>> ExecuteQueryAsync(FindReadModelQuery<TReadModel> query, CancellationToken cancellationToken)
        {
            return FindAsync(query.Predicate, query, cancellationToken)
                .ContinueWith(task => (ICollectionReadModel<TReadModel>)new EnumerableReadModel(task.Result));
        }

        protected abstract Task<IEnumerable<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> predicate, FindReadModelQuery<TReadModel> query, CancellationToken cancellationToken);

        private sealed class EnumerableReadModel : ICollectionReadModel<TReadModel>
        {
            private readonly IEnumerable<TReadModel> enumerable;

            public EnumerableReadModel(IEnumerable<TReadModel> enumerable)
            {
                this.enumerable = enumerable?.ToList() ?? Enumerable.Empty<TReadModel>();
            }

            public IEnumerator<TReadModel> GetEnumerator()
            {
                return enumerable.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return enumerable.GetEnumerator();
            }
        }
    }
}
