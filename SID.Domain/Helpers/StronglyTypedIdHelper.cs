﻿using SID.Domain.Base;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SID.Infra.Helpers
{
    public static class StronglyTypedIdHelper
    {
        private static readonly ConcurrentDictionary<Type, Delegate> _stronglyTypedIdFactories = new();

        public static Func<TValue, object> GetFactory<TValue>(Type stronglyTypedIdType) where TValue : notnull
        {
            return (Func<TValue, object>)_stronglyTypedIdFactories
                .GetOrAdd(stronglyTypedIdType, CreateFactory<TValue>);
        }

        public static bool IsStronglyTypedId(Type type) => IsStronglyTypedId(type, out _);

        public static bool IsStronglyTypedId(Type type, [NotNullWhen(true)] out Type? idType)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type.BaseType is Type baseType && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(StronglyTypedId<>))
            {
                idType = baseType.GetGenericArguments()[0];
                return true;
            }

            idType = null;
            return false;
        }

        private static Func<TValue, object> CreateFactory<TValue>(Type stronglyTypedIdType) where TValue : notnull
        {
            if (!IsStronglyTypedId(stronglyTypedIdType))
                throw new ArgumentException($"Type '{stronglyTypedIdType}' is not a strongly-typed id type", nameof(stronglyTypedIdType));

            var ctor = stronglyTypedIdType.GetConstructor(new[] { typeof(TValue) });

            if (ctor is null)
                throw new ArgumentException($"Type '{stronglyTypedIdType}' doesn't have a constructor with one parameter of type '{typeof(TValue)}'", nameof(stronglyTypedIdType));

            var param = Expression.Parameter(typeof(TValue), "value");
            var body = Expression.New(ctor, param);
            var lambda = Expression.Lambda<Func<TValue, object>>(body, param);

            return lambda.Compile();
        }
    }
}