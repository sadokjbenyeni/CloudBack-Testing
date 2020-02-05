using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;

namespace CloudBacktesting.Infra.EventFlow.Extensions
{
    public static class CommandBusExtensions
    {
        public static Task<TExecutionResult> SafePublishAsync<TAggregate, TIdentity, TExecutionResult>(this ICommandBus bus, ICommand<TAggregate, TIdentity, TExecutionResult> command, CancellationToken cancellationToken)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TExecutionResult : IExecutionResult
        {
             try
            {
                return bus.PublishAsync(command, CancellationToken.None);
            }
            catch (AggregateException aggregateEx)
            {
                var result = (IExecutionResult) new FailedExecutionResult(new[] { aggregateEx.Message }.Union(aggregateEx.InnerExceptions.Select(ex => ex.Message)));
                return Task.FromResult((TExecutionResult)result);
            }
            catch (Exception ex)
            {
                var result = (IExecutionResult) new FailedExecutionResult(new[] { ex.Message });
                return Task.FromResult((TExecutionResult)result);
            }
        }
    }
}
