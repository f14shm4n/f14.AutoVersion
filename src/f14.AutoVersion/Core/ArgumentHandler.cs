using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AutoVersion.Core
{
    public abstract class ArgumentHandler : IComparable<ArgumentHandler>
    {
        public IEnumerable<string> Aliases { get; protected set; }
        public string Description { get; protected set; }
        public bool HasValue { get; protected set; } = false;
        public object Value { get; protected set; }
        public int Order { get; protected set; }

        public abstract void ParseValue(string value);
        public abstract void DoAction();

        public int CompareTo(ArgumentHandler other)
        {
            if (other == null) return 1;
            return Order.CompareTo(other.Order);
        }
    }
}
