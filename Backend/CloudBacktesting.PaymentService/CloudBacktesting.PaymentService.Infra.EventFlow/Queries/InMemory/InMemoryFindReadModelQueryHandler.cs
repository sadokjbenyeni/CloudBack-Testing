using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.EventFlow.Queries.InMemory
{
    public class InMemoryFindReadModelQueryHandler<TReadModel> : FindReadModelQueryHandler<TReadModel>
            where TReadModel : class, IReadModel
    {
        private readonly IInMemoryReadStore<TReadModel> readStore;

        public InMemoryFindReadModelQueryHandler(IInMemoryReadStore<TReadModel> readStore)
        {
            this.readStore = readStore;
        }
        protected override Task<IEnumerable<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> predicate, FindReadModelQuery<TReadModel> query, CancellationToken cancellationToken)
        {
            var whereClause = predicate.Compile();
            //var queryClause = query.Predicate.Compile();
            return readStore.FindAsync(model => whereClause(model), cancellationToken: cancellationToken)
                             .ContinueWith(task => task.Result as IEnumerable<TReadModel>);
        }
    }
}
