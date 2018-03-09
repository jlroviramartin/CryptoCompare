using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace CryptoCompare.Services
{
    /// <summary>
    /// Implementation of <code>ICryptoCompareStreamServices</code>.
    /// </summary>
    public class CryptoCompareStreamServices : ICryptoCompareStreamServices
    {
        #region fields

        private readonly ICryptoCompareServices cryptoCompareServices;
        private readonly CryptoCompareConfig config;

        private Socket socket;

        private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);

        private readonly object handlersLock = new object();
        private readonly Dictionary<MessageKey, EventHandler<MessageEventArgs>> handlers = new Dictionary<MessageKey, EventHandler<MessageEventArgs>>();
        private bool conected = false;

        #endregion

        public CryptoCompareStreamServices(ICryptoCompareServices cryptoCompareServices, CryptoCompareConfig config)
        {
            this.cryptoCompareServices = cryptoCompareServices;
            this.config = config;
        }

        #region IBackgroundService

        public void Run()
        {
            this.socket = IO.Socket(this.config.StreamerUrl);

            this.socket.On(Socket.EVENT_CONNECT, this.OnConnect);
            this.socket.On(Socket.EVENT_DISCONNECT, this.OnDisconnect);

            this.socket.On("m", this.OnMessage);

            this.socket.On(Socket.EVENT_ERROR, (data) =>
            {
                this.OnError(new ErrorEventArgs(ErrorType.Error, data));
                Debug.WriteLine("Error: " + data);
            });
            this.socket.On(Socket.EVENT_CONNECT_ERROR, (data) =>
            {
                this.OnError(new ErrorEventArgs(ErrorType.ConnectError, data));
                Debug.WriteLine("ConnectError: " + data);
            });
            this.socket.On(Socket.EVENT_CONNECT_TIMEOUT, (data) =>
            {
                this.OnError(new ErrorEventArgs(ErrorType.ConnectTimeOut, data));
                Debug.WriteLine("ConnectTimeOut: " + data);
            });
            this.socket.On(Socket.EVENT_RECONNECT_ERROR, (data) =>
            {
                this.OnError(new ErrorEventArgs(ErrorType.ReconnectError, data));
                Debug.WriteLine("ReconnectError: " + data);
            });
            this.socket.On(Socket.EVENT_RECONNECT_FAILED, (data) =>
            {
                this.OnError(new ErrorEventArgs(ErrorType.ReconnectFailed, data));
                Debug.WriteLine("ReconnectFailed: " + data);
            });

            this.resetEvent.WaitOne();

            this.socket.Close();
        }

        public void Stop()
        {
            this.resetEvent.Set();
        }

        #endregion

        #region ICryptoCompareStreamServices

        public void Subscribe(MessageKey key, EventHandler<MessageEventArgs> handler)
        {
            lock (this.handlersLock)
            {
                if (this.handlers.ContainsKey(key))
                {
                    this.handlers[key] += handler;
                }
                else
                {
                    this.handlers.Add(key, handler);

                    if (this.conected)
                    {
                        Subscription sub = new Subscription();
                        sub.Subs.Add(key.Pack());
                        this.socket.Emit("SubAdd", JObject.FromObject(sub));
                    }
                }
            }
        }

        public void Unsubscribe(MessageKey key, EventHandler<MessageEventArgs> handler)
        {
            lock (this.handlersLock)
            {
                if (this.handlers.ContainsKey(key))
                {
                    this.handlers[key] -= handler;
                    if (this.handlers[key] == null)
                    {
                        this.handlers.Remove(key);

                        if (this.conected)
                        {
                            Subscription sub = new Subscription();
                            sub.Subs.Add(key.Pack());
                            this.socket.Emit("SubRemove", JObject.FromObject(sub));
                        }
                    }
                }
            }
        }

        public event EventHandler<MessageEventArgs> NonHandledMessage;

        public event EventHandler<ErrorEventArgs> Error;

        #endregion

        #region private

        private void OnNonHandledMessage(MessageEventArgs args)
        {
            if (this.NonHandledMessage != null)
            {
                this.NonHandledMessage(this, args);
            }
        }

        private void OnError(ErrorEventArgs args)
        {
            if (this.Error != null)
            {
                this.Error(this, args);
            }
        }

        private void OnConnect(object data)
        {
            Debug.WriteLine("Connect");

            lock (this.handlersLock)
            {
                foreach (MessageKey key in this.handlers.Keys)
                {
                    Subscription sub = new Subscription();
                    sub.Subs.Add(key.Pack());
                    this.socket.Emit("SubAdd", JObject.FromObject(sub));
                    //this.socket.Emit("SubAdd", JObject.FromObject(new { subs = new[] { key.Pack() } }));
                }

                this.conected = true;
            }
        }

        private void OnDisconnect(object data)
        {
            Debug.WriteLine("Disconnect");

            lock (this.handlersLock)
            {
                this.conected = false;
            }

            this.Stop();
        }

        private void OnMessage(object data)
        {
            Debug.WriteLine("Message");

            string packedMessage = data as string;
            if (packedMessage != null)
            {
                try
                {
                    bool handled = false;
                    Message message = MessageUtils.GetMessage(packedMessage);

                    MessageKey messageKey = MessageUtils.GetMessageKey(packedMessage);
                    if (messageKey != null)
                    {
                        lock (this.handlersLock)
                        {
                            EventHandler<MessageEventArgs> handler;
                            if (this.handlers.TryGetValue(messageKey, out handler))
                            {
                                handler(this, new MessageEventArgs((Message)message.Clone()));
                                handled = true;
                            }
                        }
                    }

                    if (!handled)
                    {
                        this.OnNonHandledMessage(new MessageEventArgs((Message)message.Clone()));
                        Debug.WriteLine(message);
                    }
                }
                catch (MalformedMessageException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        #endregion
    }
}