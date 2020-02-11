using CloudBacktesting.SubscriptionService.WebAPI.Models;
using System;
using System.Runtime.Serialization;

namespace CloudBacktesting.SubscriptionService.WebAPI.Exceptions
{
    [Serializable]
    internal class NoBindingFoundException : Exception
    {
        public NoBindingFoundException() : base()
        {
        }

        public NoBindingFoundException(string message) : base(message)
        {
        }
        public NoBindingFoundException(SubscriptionState value) : base($"Not supported value: '{value}'")
        {

        }

        public NoBindingFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoBindingFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}