using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.Infra.EventFlow.Extensions
{
    // public static class ReadModelEventFlowExtensions
    // {
    //     public static EventFlowOptions UseMongoDbFindReadModel<TQueryHandler, TQuery, TCollectionResult, TResult>(this EventFlowOptions options)
    //         where TQueryHandler : MongoDbFindReadModelQueryHandler<TResult>
    //         where TQuery : FindReadModelQuery<TResult>
    //         // where TCollectionResult : ICollectionReadModel<TResult>
    //         where TResult : class, IReadModel
    //     {
    //         options.AddQueryHandler<TQueryHandler, TQuery, TCollectionResult>();
    //         return options;
    //     }
    // }
}
