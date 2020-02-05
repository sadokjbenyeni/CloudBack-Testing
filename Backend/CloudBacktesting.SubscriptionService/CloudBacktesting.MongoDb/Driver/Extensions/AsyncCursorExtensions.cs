using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.MongoDb.Driver.Extensions
{
    public static class AsyncCursorExtensions
    {
        public static async Task<IEnumerable<TDocument>> ToEnumerableAsync<TDocument>(this IAsyncCursor<TDocument> cursor, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new List<TDocument>();
            while (await cursor.MoveNextAsync())
            {
                result.AddRange(cursor.Current);
                cancellationToken.ThrowIfCancellationRequested();
            }
            return result;
        }

    }
}
