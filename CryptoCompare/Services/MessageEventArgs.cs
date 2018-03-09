using System;

namespace CryptoCompare.Services
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(Message message)
        {
            this.Message = message;
        }

        public Message Message { get; }
    }
}