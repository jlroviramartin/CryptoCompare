using System;

namespace CryptoCompare.Attributes
{
    /// <summary>
    /// Format attribute to use in <code>ToString</code>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FormatAttribute : Attribute
    {
        public FormatAttribute(string format)
        {
            this.Format = format;
        }

        /// <summary>
        /// Format.
        /// </summary>
        public string Format { get; }
    }
}