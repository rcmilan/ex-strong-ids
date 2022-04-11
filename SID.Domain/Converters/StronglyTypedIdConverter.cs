﻿using SID.Domain.Base;
using SID.Domain.Helpers;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;

namespace SID.Domain.Converters
{
    public class StronglyTypedIdConverter : TypeConverter
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> _actualConverters = new();

        private readonly TypeConverter _innerConverter;

        public StronglyTypedIdConverter(Type stronglyTypedIdType)
        {
            _innerConverter = _actualConverters.GetOrAdd(stronglyTypedIdType, CreateActualConverter);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
            _innerConverter.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
            _innerConverter.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) =>
            _innerConverter.ConvertFrom(context, culture, value);

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) =>
            _innerConverter.ConvertTo(context, culture, value, destinationType);

        private static TypeConverter CreateActualConverter(Type stronglyTypedIdType)
        {
            if (!StronglyTypedIdHelper.IsStronglyTypedId(stronglyTypedIdType, out var idType))
                throw new InvalidOperationException($"The type '{stronglyTypedIdType}' is not a strongly typed id");

            var actualConverterType = typeof(StronglyTypedIdConverter<>).MakeGenericType(idType);

            return (TypeConverter)Activator
                .CreateInstance(actualConverterType, stronglyTypedIdType)!;
        }
    }

    public class StronglyTypedIdConverter<TValue> : TypeConverter where TValue : notnull
    {
        private static readonly TypeConverter _idValueConverter = GetIdValueConverter();

        private readonly Type _type;

        public StronglyTypedIdConverter(Type type)
        {
            _type = type;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string)
                || sourceType == typeof(TValue)
                || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            return destinationType == typeof(string)
                || destinationType == typeof(TValue)
                || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string s)
                value = _idValueConverter.ConvertFrom(s);

            if (value is TValue idValue)
            {
                var factory = StronglyTypedIdHelper.GetFactory<TValue>(_type);
                return factory(idValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var stronglyTypedId = (StronglyTypedId<TValue>)value;
            TValue idValue = stronglyTypedId.Value;

            if (destinationType == typeof(string))
                return idValue.ToString()!;

            if (destinationType == typeof(TValue))
                return idValue;

            return base.ConvertTo(context, culture, value, destinationType);
        }

        private static TypeConverter GetIdValueConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(TValue));

            if (converter.CanConvertFrom(typeof(string)))
                return converter;

            throw new InvalidOperationException($"Type '{typeof(TValue)}' doesn't have a converter that can convert from string");
        }
    }
}