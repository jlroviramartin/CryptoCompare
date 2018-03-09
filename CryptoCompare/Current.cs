using System;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// Current and CurrentAggregation message.
    /// </summary>
    public class Current : MaskedMessage<Current>
    {
        #region fields

        private static readonly PackClass pack = new PackClass(typeof(Current), true);

        #endregion

        [Pack(0x0)]
        public CurrentFags Flags { get; set; }

        [Pack(0x1)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Price { get; set; }

        [Pack(0x2)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Bid { get; set; }

        [Pack(0x4)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Offer { get; set; }

        [Pack(0x8)]
        [Format("yyyy MMMM dd HH:mm:ss")]
        public DateTime LastUpdate { get; set; }

        [Pack(0x10)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Avg { get; set; }

        [Pack(0x20)]
        [Symbol(FROM_SYMBOL)]
        [Format("F2")]
        public decimal LastVolume { get; set; }

        [Pack(0x40)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal LastVolumeTo { get; set; }

        [Pack(0x80)]
        public string LastTradeId { get; set; }

        [Pack(0x100)]
        [Symbol(FROM_SYMBOL)]
        [Format("F2")]
        public decimal VolumeHour { get; set; }

        [Pack(0x200)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal VolumeHourTo { get; set; }

        [Pack(0x400)]
        [Symbol(FROM_SYMBOL)]
        [Format("F2")]
        public decimal Volume24Hour { get; set; }

        [Pack(0x800)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Volume24HourTo { get; set; }

        [Pack(0x1000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal OpenHour { get; set; }

        [Pack(0x2000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal HighHour { get; set; }

        [Pack(0x4000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal LowHour { get; set; }

        [Pack(0x8000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Open24Hour { get; set; }

        [Pack(0x10000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal High24Hour { get; set; }

        [Pack(0x20000)]
        [Symbol(TO_SYMBOL)]
        [Format("F2")]
        public decimal Low24Hour { get; set; }

        [Pack(0x40000)]
        public string LastMarket { get; set; }

        protected override PackClass GetPackClass()
        {
            return pack;
        }
    }
}