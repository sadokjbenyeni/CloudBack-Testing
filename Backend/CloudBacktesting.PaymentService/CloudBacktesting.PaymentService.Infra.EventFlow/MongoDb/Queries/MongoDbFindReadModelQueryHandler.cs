using CloudBacktesting.Infra.EventFlow.Queries;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.MongoDb.Driver.Extensions;

namespace CloudBacktesting.Infra.EventFlow.MongoDb.Queries
{
    public class MongoDbFindReadModelQueryHandler<TReadModel> : FindReadModelQueryHandler<TReadModel>
        where TReadModel : class, IReadModel
    {
        private readonly IMongoDbReadModelStore<TReadModel> store;

        public MongoDbFindReadModelQueryHandler(IMongoDbReadModelStore<TReadModel> store)
        {
            this.store = store;
        }

        protected override async Task<IEnumerable<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> predicate, FindReadModelQuery<TReadModel> query, CancellationToken cancellationToken)
        {
            var cursor = await store.FindAsync(predicate, cancellationToken: cancellationToken);
            return await cursor.ToEnumerableAsync(cancellationToken);
        }
    }
}
