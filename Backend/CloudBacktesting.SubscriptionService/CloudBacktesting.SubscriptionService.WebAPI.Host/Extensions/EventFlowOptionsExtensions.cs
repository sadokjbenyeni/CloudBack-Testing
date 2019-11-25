using EventFlow;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Host.Extensions
{
    public static class EventFlowOptionsExtensions
    {
        //public static IEventFlowOptions AddEvents(this IEventFlowOptions options, Assembly assemblySource)
        //{
        //    var eventsType = SearchEventByEventFlow(assemblySource).ToArray();
        //    options.AddEvents(eventsType);
        //    return options;
        //}

        //private static IEnumerable<Type> SearchEventByEventFlow(Assembly assemblySource)
        //{
        //    var eventType = typeof(AggregateEvent<,>);
        //    return assemblySource.DefinedTypes.Where(type => eventType.IsAssignableFrom(type));            
        //}
    }
}
