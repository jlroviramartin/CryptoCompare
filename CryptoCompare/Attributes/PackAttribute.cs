using System;

namespace CryptoCompare.Attributes
{
    /// <summary>
    /// Packt attribute used by <code>PackClass</code>. It indicates which properties and fields are used in a packet message.
    /// Also it indicates the mask of the property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PackAttribute : Attribute
    {
        public PackAttribute(int mask)
        {
            this.Mask = mask;
        }

        /// <summary>
        /// Property or field mask.
        /// </summary>
        public int Mask { get; }
    }
}