using System;

namespace CryptoCompare
{
    [Flags]
    public enum CurrentFags
    {
        PRICEUP = 0x1,
        PRICEDOWN = 0x2,
        PRICEUNCHANGED = 0x4,
        BIDUP = 0x8,
        BIDDOWN = 0x10,
        BIDUNCHANGED = 0x20,
        OFFERUP = 0x40,
        OFFERDOWN = 0x80,
        OFFERUNCHANGED = 0x100,
        AVGUP = 0x200,
        AVGDOWN = 0x400,
        AVGUNCHANGED = 0x800
    }
}