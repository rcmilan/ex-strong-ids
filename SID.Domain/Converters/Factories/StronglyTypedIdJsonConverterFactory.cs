﻿using SID.Domain.Helpers;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SID.Domain.Converters.Factories
{
    public class StronglyTypedIdJsonConverterFactory : JsonConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, JsonConverter> Cache = new();

        public override bool CanConvert(Type typeToConvert) => StronglyTypedIdHelper.IsStronglyTypedId(typeToConvert);

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) => Cache.GetOrAdd(typeToConvert, CreateConverter);

        private static JsonConverter CreateConverter(Type typeToConvert)
        {
            if (!StronglyTypedIdHelper.IsStronglyTypedId(typeToConvert, out var valueType))
                throw new InvalidOperationException($"Cannot create converter for '{typeToConvert}'");

            var type = typeof(StronglyTypedIdJsonConverter<,>).MakeGenericType(typeToConvert, valueType);

            return (JsonConverter)Activator.CreateInstance(type);
        }
    }
}
