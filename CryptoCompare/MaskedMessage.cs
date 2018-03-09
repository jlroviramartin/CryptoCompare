using System.Collections.Generic;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// Message with a mask.
    /// </summary>
    /// <typeparam name="T">Derived type.</typeparam>
    public abstract class MaskedMessage<T> : MessageBase
        where T : MaskedMessage<T>
    {
        /// <summary>
        /// Current mask.
        /// </summary>
        [Format("X")]
        public int Mask { get; set; }

        /// <summary>
        /// Message key related to this message.
        /// </summary>
        /// <returns>Message key.</returns>
        public MessageKey GetMessageKey()
        {
            return new MessageKey
            {
                Type = this.Type,
                Market = this.Market,
                FromSymbol = this.FromSymbol,
                ToSymbol = this.ToSymbol
            };
        }

        /// <summary>
        /// This method tests if the <code>name</code> property or field is visible (the mask is set to 1).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsVisible(string name)
        {
            PackClass packClass = this.GetPackClass();
            return packClass.IsVisible(this, this.Mask, name);
        }

        /// <summary>
        /// This method gets the visible properties or fields (the mask is set to 1).
        /// Only it return properties and fields affected by <code>PackAttribute</code>.
        /// </summary>
        /// <returns>Visible properties or fields.</returns>
        public IEnumerable<string> GetVisible()
        {
            PackClass packClass = this.GetPackClass();
            return packClass.GetVisible(this, this.Mask);
        }

        /// <summary>
        /// This method gets the hidden properties or fields (the mask is set to 0).
        /// Only it return properties and fields affected by <code>PackAttribute</code>.
        /// </summary>
        /// <returns>Hidden properties or fields.</returns>
        public IEnumerable<string> GetHidden()
        {
            PackClass packClass = this.GetPackClass();
            return packClass.GetHidden(this, this.Mask);
        }

        /// <summary>
        /// This method updates this message with the visible properties or fields of <code>other</code>.
        /// </summary>
        /// <param name="other">Message.</param>
        public virtual void Update(T other)
        {
            PackClass packClass = this.GetPackClass();
            packClass.Update(this, other, other.Mask);
            this.Mask |= other.Mask;
        }

        public override string Pack()
        {
            PackClass packClass = this.GetPackClass();
            return packClass.Pack(this, this.Mask);
        }

        public override void Unpack(string message)
        {
            PackClass packClass = this.GetPackClass();
            int mask = packClass.Unpack(message, this);
            this.Mask = mask;
        }

        protected abstract PackClass GetPackClass();

        public override string ToString()
        {
            return Utils.ReflectionToString(this, this.IsVisible);
        }
    }
}