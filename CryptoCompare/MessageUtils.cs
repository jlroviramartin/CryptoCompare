namespace CryptoCompare
{
    public static class MessageUtils
    {
        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Message type.</returns>
        public static MessageType GetMessageType(string message)
        {
            int messageType;
            if (!int.TryParse(message.Substring(0, message.IndexOf("~")), out messageType))
            {
                throw new MalformedMessageException(message);
            }

            return (MessageType)messageType;
        }

        /// <summary>
        /// This method gets the message key of a message or null if the method doesn`t have it.
        /// </summary>
        /// <param name="message">Message string.</param>
        /// <returns>Message key or null.</returns>
        public static MessageKey GetMessageKey(string message)
        {
            MessageType messageType = MessageUtils.GetMessageType(message);
            switch (messageType)
            {
                case MessageType.Current:
                case MessageType.CurrentAggregation:
                case MessageType.Trade:
                    MessageKey messageKey = new MessageKey();
                    messageKey.Unpack(message);
                    return messageKey;
            }

            return null;
        }

        /// <summary>
        /// This method converts a message string into a message class.
        /// </summary>
        /// <param name="message">Message string.</param>
        /// <returns>Message class.</returns>
        public static Message GetMessage(string message)
        {
            MessageType messageType = MessageUtils.GetMessageType(message);
            switch (messageType)
            {
                case MessageType.Current:
                case MessageType.CurrentAggregation:
                    Current current = new Current();
                    current.Unpack(message);
                    return current;

                case MessageType.Trade:
                    Trade trade = new Trade();
                    trade.Unpack(message);
                    return trade;

                case MessageType.LoadComplete:
                    LoadComplete loadComplete = new LoadComplete();
                    loadComplete.Unpack(message);
                    return loadComplete;
            }

            MessageBag messageBag = new MessageBag();
            messageBag.Unpack(message);
            return messageBag;
        }
    }
}