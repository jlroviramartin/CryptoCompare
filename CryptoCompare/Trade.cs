using System;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// Trade message.
    /// </summary>
    public class Trade : MaskedMessage<Trade>
    {
        #region fields

        private static readonly PackClass pack = new PackClass(typeof(Trade), true);

        #endregion

        [Pack(0x0)]
        public TradeFlag Flags { get; set; }

        [Pack(0x1)]
        public string Id { get; set; }

        [Pack(0x2)]
        [Format("yyyy MMMM dd HH:mm:ss")]
        public DateTime TimeStamp { get; set; }

        [Pack(0x4)]
        [Symbol(FROM_SYMBOL)]
        [Format("F2")]
        public decimal Quantity { get; set; }

        [Pack(0x8)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Price { get; set; }

        [Pack(0x10)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Total { get; set; }

        protected override PackClass GetPackClass()
        {
            return pack;
        }
    }
}