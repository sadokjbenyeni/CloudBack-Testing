using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CloudBacktesting.SubscriptionService.Infra.Reflection.Dynamic
{
    /// <summary>
    /// QuantHouse provides class. It is allows dynamic execution private method based querying.
    /// Very handy when, at compile time, you don't know the private method of queries that will be written.
    /// </summary>
    public class PrivateReflectionDynamicObject : DynamicObject
    {

        private static IDictionary<Type, IDictionary<string, IProperty>> propertiesOnType = new ConcurrentDictionary<Type, IDictionary<string, IProperty>>();

        private object RealObject { get; set; }

        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public PrivateReflectionDynamicObject(object realObject)
        {
            RealObject = realObject;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = GetProperty(binder.Name);
            result = prop.GetValue(RealObject, index: null)
                         .AsDynamic();
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var prop = GetProperty(binder.Name);
            prop.SetValue(RealObject, value, index: null);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var prop = GetIndexProperty();
            result = prop.GetValue(RealObject, indexes)
                         .AsDynamic();

            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            var prop = GetIndexProperty();
            prop.SetValue(RealObject, value, indexes);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = InvokeMemberOnType(RealObject.GetType(), RealObject, binder.Name, args)
                        .AsDynamic();
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = Convert.ChangeType(RealObject, binder.Type);
            return true;
        }

        public override string ToString()
        {
            return RealObject.ToString();
        }

        private IProperty GetIndexProperty()
        {
            return GetProperty("Item");
        }

        private IProperty GetProperty(string propertyName)
        {
            var typeProperties = GetTypeProperties(RealObject.GetType());
            // Look for the one we want
            if (typeProperties.TryGetValue(propertyName, out var property))
            {
                return property;
            }
            var propNames = typeProperties.Keys.Where(name => name[0] != '<').OrderBy(name => name);
            var message = $"The property {propertyName} doesn't exist on type {RealObject.GetType().Name}. Supported properties are: {string.Join(", ", propNames)}";
            throw new ArgumentException(message);
        }

        private static IDictionary<string, IProperty> GetTypeProperties(Type type)
        {
            if (propertiesOnType.TryGetValue(type, out var typeProperties))
            {
                return typeProperties;
            }
            typeProperties = new ConcurrentDictionary<string, IProperty>();
            foreach (PropertyInfo prop in type.GetProperties(bindingFlags).Where(p => p.DeclaringType == type))
            {
                typeProperties[prop.Name] = new Property() { PropertyInfo = prop };
            }
            foreach (FieldInfo field in type.GetFields(bindingFlags).Where(p => p.DeclaringType == type))
            {
                typeProperties[field.Name] = new Field() { FieldInfo = field };
            }
            if (type.BaseType != null)
            {
                foreach (IProperty prop in GetTypeProperties(type.BaseType).Values)
                {
                    typeProperties[prop.Name] = prop;
                }
            }
            propertiesOnType[type] = typeProperties;
            return typeProperties;
        }

        private static object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            try
            {
                return type.InvokeMember(name, BindingFlags.InvokeMethod | bindingFlags, null, target, args);
            }
            catch (MissingMethodException)
            {
                return type.BaseType == null ? null : InvokeMemberOnType(type.BaseType, target, name, args);
            }
        }

        
        private interface IProperty
        {
            string Name { get; }
            object GetValue(object obj, object[] index);
            void SetValue(object obj, object val, object[] index);
        }

        private class Property : IProperty
        {
            internal PropertyInfo PropertyInfo { get; set; }

            public string Name
            {
                get
                {
                    return PropertyInfo.Name;
                }
            }

            public object GetValue(object obj, object[] index)
            {
                return PropertyInfo.GetValue(obj, index);
            }

            public void SetValue(object obj, object val, object[] index)
            {
                PropertyInfo.SetValue(obj, val, index);
            }
        }

        private class Field : IProperty
        {
            internal FieldInfo FieldInfo { get; set; }

            public string Name
            {
                get
                {
                    return FieldInfo.Name;
                }
            }


            public object GetValue(object obj, object[] index)
            {
                return FieldInfo.GetValue(obj);
            }

            public void SetValue(object obj, object val, object[] index)
            {
                FieldInfo.SetValue(obj, val);
            }
        }
    }
}
