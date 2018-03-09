namespace CryptoCompare
{
    /// <summary>
    /// Message key to identify stream events.
    /// </summary>
    public class MessageKey : MessageBase
    {
        #region fields

        private static readonly PackClass pack = new PackClass(typeof(MessageKey), false);

        #endregion

        public override string Pack()
        {
            return pack.Pack(this);
        }

        public override void Unpack(string message)
        {
            pack.Unpack(message, this);
        }
    }
}