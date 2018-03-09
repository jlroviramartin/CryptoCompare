using System;

namespace CryptoCompare
{
    [Flags]
    public enum TradeFlag
    {
        SELL = 0x1,
        BUY = 0x2,
        UNKNOWN = 0x4
    }
}