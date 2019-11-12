using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Infra.Reflection.Dynamic
{
    public static class PrivateReflectionDynamicObjectExtensions
    {
        public static dynamic AsDynamic (this object @object)
        {
            return WrapObjectIfNeeded(@object);
        }


        private static object WrapObjectIfNeeded(object o)
        {
            if (o == null || o.GetType().IsPrimitive || o is string)
                return o;

            return new PrivateReflectionDynamicObject(o);
        }
    }
}
