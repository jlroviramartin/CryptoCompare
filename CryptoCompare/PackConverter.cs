using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptoCompare
{
    /// <summary>
    /// This class converts a message member (string) into a property (object) and vice versa.
    /// </summary>
    public class PackConverter
    {
        public static readonly PackConverter Instance = new PackConverter();

        /// <summary>
        /// This method gets the converter of objects into strings (message member).
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>Converter.</returns>
        public Func<object, string> GetToString(Type type)
        {
            Func<object, string> convert;
            if (!this.toString.TryGetValue(type, out convert))
            {
                if (type.IsEnum)
                {
                    string Convert(object s)
                    {
                        Type underlyingType = Enum.GetUnderlyingType(type);
                        IConvertible underlying = (IConvertible)System.Convert.ChangeType(s, underlyingType);
                        return underlying.ToInt64(null).ToString();
                    }

                    convert = Convert;
                    this.toString.Add(type, convert);
                }
            }

            return convert;
        }

        public string ToString(object obj)
        {
            return this.GetToString(obj.GetType())(obj);
        }

        /// <summary>
        /// This method gets the converter of strings (message member) into objects.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>Converter.</returns>
        public Func<string, object> GetParse(Type type)
        {
            Func<string, object> convert;
            if (!this.parse.TryGetValue(type, out convert))
            {
                if (type.IsEnum)
                {
                    object Convert(string s)
                    {
                        long i = long.Parse(s, NumberStyles.None, enUs);
                        //return Enum.GetValues(type).GetValue(i);
                        return Enum.ToObject(type, i);
                    }

                    convert = Convert;
                    this.parse.Add(type, convert);
                }
            }

            return convert;
        }

        public Func<string, T> GetParse<T>()
        {
            Func<string, object> fun = this.GetParse(typeof(T));
            return (s) => (T)fun(s);
        }

        public T Parse<T>(string str)
        {
            return this.GetParse<T>()(str);
        }

        #region private

        private PackConverter()
        {
            // To string
            this.toString.Add(typeof(string), s => (string)s);

            this.RegisterToString<char>(s => s.ToString(enUs));

            this.RegisterToString<bool>(s => s ? "1" : "0");

            this.RegisterToString<sbyte>(s => s.ToString("D", enUs));
            this.RegisterToString<short>(s => s.ToString("D", enUs));
            this.RegisterToString<int>(s => s.ToString("D", enUs));
            this.RegisterToString<long>(s => s.ToString("D", enUs));

            this.RegisterToString<byte>(s => s.ToString("D", enUs));
            this.RegisterToString<ushort>(s => s.ToString("D", enUs));
            this.RegisterToString<uint>(s => s.ToString("D", enUs));
            this.RegisterToString<ulong>(s => s.ToString("D", enUs));

            this.RegisterToString<float>(s => s.ToString("F", enUs));
            this.RegisterToString<double>(s => s.ToString("F", enUs));
            this.RegisterToString<decimal>(s => s.ToString("G", enUs));

            this.RegisterToString<DateTime>(s =>
            {
                TimeSpan timeSpan = s.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return ((long)timeSpan.TotalMilliseconds).ToString("D");
            });

            // Parse (from string)
            this.parse.Add(typeof(string), s => s);

            this.RegisterParse(s => char.Parse(s));

            this.RegisterParse(s =>
            {
                ushort v = ushort.Parse(s, NumberStyles.None, enUs);
                return v != 0;
            });

            this.RegisterParse(s => sbyte.Parse(s, NumberStyles.AllowLeadingSign, enUs));
            this.RegisterParse(s => short.Parse(s, NumberStyles.AllowLeadingSign, enUs));
            this.RegisterParse(s => int.Parse(s, NumberStyles.AllowLeadingSign, enUs));
            this.RegisterParse(s => long.Parse(s, NumberStyles.AllowLeadingSign, enUs));

            this.RegisterParse(s => byte.Parse(s, NumberStyles.None, enUs));
            this.RegisterParse(s => ushort.Parse(s, NumberStyles.None, enUs));
            this.RegisterParse(s => uint.Parse(s, NumberStyles.None, enUs));
            this.RegisterParse(s => ulong.Parse(s, NumberStyles.None, enUs));

            this.RegisterParse(s => float.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, enUs));
            this.RegisterParse(s => double.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, enUs));
            this.RegisterParse(s => decimal.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, enUs));

            this.RegisterParse(s =>
            {
                long milli = long.Parse(s, NumberStyles.AllowLeadingSign, enUs);
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                time = time.AddMilliseconds(milli);
                return time;

                //long time = long.Parse(s, NumberStyles.Integer, enUs);
                //return new DateTime(time, DateTimeKind.Utc);
            });
        }

        private void RegisterToString<T>(Func<T, string> fun)
        {
            this.toString.Add(typeof(T), x => fun((T)x));
        }

        private void RegisterParse<T>(Func<string, T> fun)
        {
            this.parse.Add(typeof(T), x => fun(x));
        }

        private static readonly CultureInfo enUs = CultureInfo.GetCultureInfo("en-US");
        private readonly Dictionary<Type, Func<object, string>> toString = new Dictionary<Type, Func<object, string>>();
        private readonly Dictionary<Type, Func<string, object>> parse = new Dictionary<Type, Func<string, object>>();

        #endregion
    }
}