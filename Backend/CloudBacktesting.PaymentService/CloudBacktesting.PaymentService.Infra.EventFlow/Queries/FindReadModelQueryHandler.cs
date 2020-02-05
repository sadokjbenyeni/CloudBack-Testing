using CloudBacktesting.Infra.EventFlow.Queries.InMemory;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.EventFlow.Queries
{
    public abstract class FindReadModelQueryHandler<TReadModel> : IQueryHandler<FindReadModelQuery<TReadModel>, ICollectionReadModel<TReadModel>>, IQueryHandler
         where TReadModel : class, IReadModel
    {
        public async Task<ICollectionReadModel<TReadModel>> ExecuteQueryAsync(FindReadModelQuery<TReadModel> query, CancellationToken cancellationToken)
        {
            //return FindAsync(query.Predicate, query, cancellationToken)
            //    .ContinueWith(task => (ICollectionReadModel<TReadModel>)new EnumerableReadModel(task.Result));
            var result = await FindAsync(query.Predicate, query, cancellationToken);
            return new EnumerableReadModel(result);
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
