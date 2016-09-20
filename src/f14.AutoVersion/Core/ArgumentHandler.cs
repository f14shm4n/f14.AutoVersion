using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AutoVersion.Core
{
    public abstract class ArgumentHandler : IComparable<ArgumentHandler>
    {
        /// <summary>
        /// Command aliases.
        /// </summary>
        public IEnumerable<string> Aliases { get; protected set; }
        /// <summary>
        /// Command description.
        /// </summary>
        public string Description { get; protected set; }
        /// <summary>
        /// Mark handler, if has command value. Sample: -t 1.0.0-b{0} , -t - aliase; 1.0.0-b{0} - value.
        /// </summary>
        public bool HasValue { get; protected set; } = false;
        /// <summary>
        /// Command value.
        /// </summary>
        public object Value { get; protected set; }
        /// <summary>
        /// The order of execution of the handler.
        /// </summary>
        public int Order { get; protected set; }

        /// <summary>
        /// Logic for parse value.
        /// </summary>
        /// <param name="value"></param>
        public abstract void ParseValue(string value);
        /// <summary>
        /// Execution logic.
        /// </summary>
        public abstract void Execute();

        public int CompareTo(ArgumentHandler other)
        {
            if (other == null) return 1;
            return Order.CompareTo(other.Order);
        }
    }
}
