using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.EventFlow.ReadStores;
using CloudBacktesting.Infra.EventFlow.MongoDb.Queries;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using MongoDB.Driver;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.EventFlow.Tests.Queries
{
    [TestFixture]
    public class ReadModelForAllQueryHandlerTest
    {
        [Test]
        public async Task Should_have_list_when_query_hanlder_have_list()
        {
            var store = Substitute.For<IMongoDbReadModelStore<ReadModelTest>>();
            var expected = Enumerable.Range(1, 10).Select(index => new ReadModelTest() { Name = $"ReadModel number {index}" }).ToList();
            var cursor = new AsyncCursorTest(new[] { expected });
            store.FindAsync(Arg.Any<Expression<Func<ReadModelTest, bool>>>(), Arg.Any<FindOptions<ReadModelTest, ReadModelTest>>(), Arg.Any<CancellationToken>())
                .Returns(info => cursor);

            var handler = new MongoDbFindReadModelQueryHandler<ReadModelTest>(store);

            var actual = await handler.ExecuteQueryAsync(new FindReadModelQuery<ReadModelTest>(model => true), CancellationToken.None);

            Assert.That(actual, Is.EquivalentTo(expected));
            cursor.Dispose();
        }

        [Test]
        public async Task Should_collectionreadmodel_type_when_query_handler_is_executed()
        {
            var store = Substitute.For<IMongoDbReadModelStore<ReadModelTest>>();
            var expected = Enumerable.Range(1, 10).Select(index => new ReadModelTest() { Name = $"ReadModel number {index}" }).ToList();
            var cursor = new AsyncCursorTest(new[] { expected });
            store.FindAsync(Arg.Any<Expression<Func<ReadModelTest, bool>>>(), Arg.Any<FindOptions<ReadModelTest, ReadModelTest>>(), Arg.Any<CancellationToken>())
                .Returns(info => cursor);

            var handler = new MongoDbFindReadModelQueryHandler<ReadModelTest>(store);

            var actual = await handler.ExecuteQueryAsync(new FindReadModelQuery<ReadModelTest>(model => true), CancellationToken.None);

            Assert.That(typeof(ICollectionReadModel<ReadModelTest>).IsAssignableFrom(actual.GetType()), Is.True);
            cursor.Dispose();
        }

        [TestCase("null")]
        [TestCase("notEmptyAndEmpty")]
        [TestCase("FullEmpty")]
        public async Task Should_has_empty_list_when_source_is_empty_or_null(string nullOrEmpty)
        {
            var expected = Enumerable.Empty<IEnumerable<ReadModelTest>>();
            switch (nullOrEmpty)
            {
                case "null":
                    expected = null;
                    break;
                case "notEmptyAndEmpty":
                    expected = new[] { Enumerable.Empty<ReadModelTest>() };
                    break;
                default:
                    break;
            }
            var store = Substitute.For<IMongoDbReadModelStore<ReadModelTest>>();
            var cursor = new AsyncCursorTest(nullOrEmpty == null ? null : new[] { Enumerable.Empty<ReadModelTest>() });
            store.FindAsync(Arg.Any<Expression<Func<ReadModelTest, bool>>>(), Arg.Any<FindOptions<ReadModelTest, ReadModelTest>>(), Arg.Any<CancellationToken>())
                .Returns(info => cursor);
            var handler = new MongoDbFindReadModelQueryHandler<ReadModelTest>(store);
            var actual = await handler.ExecuteQueryAsync(new FindReadModelQuery<ReadModelTest>(model => true), CancellationToken.None);
            Assert.That(actual, Is.Not.Null.And.Empty);
            cursor.Dispose();
        }
        
        [Test]
        public async Task Should_execute_query_with_predicate_when_query_handler_is_executed()
        {
            var query = new FindReadModelQuery<ReadModelTest>(model => model.Name.Contains("1"));
            var predicate = query.Predicate.Compile();
            var store = Substitute.For<IMongoDbReadModelStore<ReadModelTest>>();
            var expected = Enumerable.Range(1, 1000)
                                .Select(index => new ReadModelTest() { Name = $"ReadModel number {index}" })
                                .Where(predicate)
                                .ToList();
            var cursor = new AsyncCursorTest(new[] { expected });
            store.FindAsync(Arg.Any<Expression<Func<ReadModelTest, bool>>>(), Arg.Any<FindOptions<ReadModelTest, ReadModelTest>>(), Arg.Any<CancellationToken>())
                .Returns(info => cursor);

            var handler = new MongoDbFindReadModelQueryHandler<ReadModelTest>(store);

            var actual = await handler.ExecuteQueryAsync(query, CancellationToken.None);

            Assert.That(actual.ToList(), Has.Count.EqualTo(272));
            cursor.Dispose();
        }
    }

    public class ReadModelTest : IReadModel
    {
        public string Name { get; set; }
    }

    public class AsyncCursorTest : IAsyncCursor<ReadModelTest>
    {
        private readonly IEnumerator<IEnumerable<ReadModelTest>> expectedEnumerator;

        public AsyncCursorTest(IEnumerable<IEnumerable<ReadModelTest>> expected)
        {
            this.expectedEnumerator = expected?.Where(list => list?.Any() ?? false)?.GetEnumerator() ?? new List<IEnumerable<ReadModelTest>>().GetEnumerator();
        }

        public IEnumerable<ReadModelTest> Current => expectedEnumerator.Current;

        public void Dispose()
        {
            expectedEnumerator.Dispose();
        }

        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            return expectedEnumerator.MoveNext();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MoveNext(cancellationToken));
        }
    }
}
