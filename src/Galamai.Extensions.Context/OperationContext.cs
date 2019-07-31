using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Galamai.Extensions.Context
{
    public static class OperationContext
    {
        private static readonly AsyncLocal<ImmutableStack<Property>> _data = new AsyncLocal<ImmutableStack<Property>>();

        public static IDisposable Set<TValue>(string name, TValue value)
        {
            var properties = _data.Value ?? ImmutableStack<Property>.Empty;
            var bookmark = new Bookmark(properties);
            _data.Value = properties.Push(new Property<TValue>() { Name = name, Value = value });

            return bookmark;
        }

        public static TValue Get<TValue>(string name)
        {
            var property = _data.Value?.Where(x => x.Name == name).FirstOrDefault();

            if (property == null)
                return default;

            return ((Property<TValue>)property).Value;
        }

        private class Bookmark : IDisposable
        {
            private readonly ImmutableStack<Property> _properties;

            public Bookmark(ImmutableStack<Property> properties)
            {
                _properties = properties;
            }

            public void Dispose()
            {
                _data.Value = _properties;
            }
        }
    }
}
