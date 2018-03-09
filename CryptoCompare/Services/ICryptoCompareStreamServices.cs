using System;

namespace CryptoCompare.Services
{
    /// <summary>
    /// CryptoCompare stream services.
    /// </summary>
    public interface ICryptoCompareStreamServices : IBackgroundService
    {
        /// <summary>
        /// This method allows to subscribe to a stream event.
        /// </summary>
        /// <param name="key">Message key of the stream event where to subscribe.</param>
        /// <param name="handler">Handler.</param>
        void Subscribe(MessageKey key, EventHandler<MessageEventArgs> handler);

        /// <summary>
        /// This method allows to unsubscribe to a stream event.
        /// </summary>
        /// <param name="key">Message key of the stream event where to subscribe.</param>
        /// <param name="handler">Handler.</param>
        void Unsubscribe(MessageKey key, EventHandler<MessageEventArgs> handler);

        /// <summary>
        /// This event is send when a message is not handled.
        /// </summary>
        event EventHandler<MessageEventArgs> NonHandledMessage;

        /// <summary>
        /// This event is send when an error is produced.
        /// </summary>
        event EventHandler<ErrorEventArgs> Error;
    }
}