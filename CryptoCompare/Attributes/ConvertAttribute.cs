using System;

namespace CryptoCompare.Attributes
{
    /// <summary>
    /// This class is used by <code>PackConverter</code>. It indicates which methods are used to
    /// convert a property from/to a string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ConvertAttribute : Attribute
    {
        public ConvertAttribute(Type type, string toStringMethodName, string parseMethodName)
        {
            this.Type = type;
            this.ToStringMethodName = toStringMethodName;
            this.ParseMethodName = parseMethodName;
        }

        /// <summary>
        /// Type where can be found the 'ToString' and 'Parse' methods.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 'ToString' method.
        /// </summary>
        public string ToStringMethodName { get; }

        /// <summary>
        /// 'Parse' method.
        /// </summary>
        public string ParseMethodName { get; }
    }
}