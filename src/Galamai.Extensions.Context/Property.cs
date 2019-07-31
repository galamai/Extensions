
namespace Galamai.Extensions.Context
{
    public class Property
    {
        public string Name { get; set; }
    }

    public class Property<TValue> : Property
    {
        public TValue Value { get; set; }
    }
}
