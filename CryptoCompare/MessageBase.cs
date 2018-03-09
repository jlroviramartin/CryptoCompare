using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// Helper class for messages with properties: 'market', 'from fymbol' and 'to symbol'.
    /// Nota: bad name.
    /// </summary>
    public abstract class MessageBase : Message
    {
        public const string FROM_SYMBOL = "FromSymbol";
        public const string TO_SYMBOL = "ToSymbol";

        [Pack(0x0)]
        public string Market { get; set; }

        [Pack(0x0)]
        public string FromSymbol { get; set; }

        [Pack(0x0)]
        public string ToSymbol { get; set; }
    }
}