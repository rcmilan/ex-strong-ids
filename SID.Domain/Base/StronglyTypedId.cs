using SID.Infra.Converters;
using System.ComponentModel;

namespace SID.Domain.Base
{
    [TypeConverter(typeof(StronglyTypedIdConverter))]
    public abstract record StronglyTypedId<TValue>(TValue Value) where TValue : notnull
    {
        public override string? ToString() => Value.ToString();
    }
}
