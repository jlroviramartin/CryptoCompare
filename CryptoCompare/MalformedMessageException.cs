using System;

namespace CryptoCompare
{
    /// <summary>
    /// Exception that is throwed when a message is malformed.
    /// </summary>
    public class MalformedMessageException : Exception
    {
        public MalformedMessageException(string packedMessage)
            : base("Message is malformed: " + packedMessage + ".")
        {
        }
    }
}