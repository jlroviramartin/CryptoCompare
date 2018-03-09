using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// Base class of all messages.
    /// </summary>
    public abstract class Message : ICloneable
    {
        [Pack(0x0)]
        public MessageType Type { get; set; }

        /// <summary>
        /// This methods packs the message into a message string.
        /// </summary>
        /// <returns>Message string.</returns>
        public abstract string Pack();

        /// <summary>
        /// This methods unpacks the message from a message string.
        /// </summary>
        /// <returns>Message string.</returns>
        public abstract void Unpack(string message);

        public virtual object Clone()
        {
            Message message = (Message)this.MemberwiseClone();
            Utils.ReflectionClone(message);
            return message;
        }

        public override string ToString()
        {
            return Utils.ReflectionToString(this);
        }

        public override int GetHashCode()
        {
            return Utils.ReflectionGetHashCode(this);
        }

        public override bool Equals(object other)
        {
            return Utils.ReflectionEquals(this, other);
        }
    }

    public class LoadComplete : Message
    {
        #region fields

        private static readonly PackClass pack = new PackClass(typeof(LoadComplete), false);

        #endregion

        [Pack(0x0)]
        public string Text { get; set; }

        public override string Pack()
        {
            return pack.Pack(this);
        }

        public override void Unpack(string message)
        {
            pack.Unpack(message, this);
        }
    }

    public class MessageBag : Message
    {
        #region fields

        private static readonly PackClass pack = new PackClass(typeof(MessageBag), false);

        #endregion

        public MessageBag()
        {
            this.Items = new List<string>();
        }

        public List<string> Items { get; }

        public override string Pack()
        {
            StringBuilder buff = new StringBuilder();
            buff.Append(PackConverter.Instance.ToString(this.Type));
            foreach (string item in this.Items)
            {
                buff.Append('~');
                buff.Append(item);
            }
            return buff.ToString();
        }

        public override void Unpack(string message)
        {
            string[] valuesArray = message.Split('~');

            this.Type = PackConverter.Instance.Parse<MessageType>(valuesArray[0]);
            this.Items.AddRange(valuesArray.Skip(1));
        }

        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            buff.AppendFormat("{0} = {1}", "Type", this.Type).AppendLine();
            buff.AppendFormat("{0} = {1}", "Items", Utils.AggregateString(this.Items));

            return buff.ToString();
        }
    }
}