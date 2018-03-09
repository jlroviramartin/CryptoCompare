using System;

namespace CryptoCompare.Services
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(ErrorType type, object data)
        {
            this.Type = type;
            this.Data = data;
        }

        public ErrorType Type { get; }

        public object Data { get; }
    }
}