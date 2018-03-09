using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    /// <summary>
    /// This class maps message members into a type (and its properties).
    /// </summary>
    public class PackClass
    {
        #region fields

        private static readonly CultureInfo enUs = CultureInfo.GetCultureInfo("en-US");
        private readonly Dictionary<int, PackInfo> properties = new Dictionary<int, PackInfo>();

        /// <summary>Number of attributes with a zero mask.</summary>
        private int zeroMask;

        /// <summary>True if it is used a mask in the message or false otherwise.</summary>
        private readonly bool useMask;

        #endregion

        public PackClass(Type type, bool useMask)
        {
            this.useMask = useMask;

            this.BuildUsingFieldsFor(type);
        }

        /// <summary>
        /// Pack the <code>message</code> into string using reflection.
        /// </summary>
        /// <param name="obj">Object to pack.</param>
        /// <param name="mask">Current mask.</param>
        /// <returns>String message.</returns>
        public string Pack(object obj, int mask = -1)
        {
            StringBuilder message = new StringBuilder();

            bool first = true;
            foreach (PackInfo d in this.properties.Values)
            {
                if ((d.Mask == 0) || ((mask & d.Mask) != 0))
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        message.Append('~');
                    }

                    message.Append(d.Get(obj));
                }
            }

            if (this.useMask)
            {
                message.Append('~');
                message.Append(mask.ToString("x"));
            }

            return message.ToString();
        }

        /// <summary>
        /// Unpack the string <code>message</code> into the object <code>obj</code> using reflection.
        /// </summary>
        /// <param name="message">String message.</param>
        /// <param name="obj">Object where to unpack.</param>
        /// <returns>Mask.</returns>
        public int Unpack(string message, object obj)
        {
            string[] valuesArray = message.Split('~');
            int length = valuesArray.Length;

            int mask = -1;
            if (this.useMask)
            {
                if (length > this.zeroMask)
                {
                    if (!int.TryParse(valuesArray[valuesArray.Length - 1], NumberStyles.AllowHexSpecifier, null, out mask))
                    {
                        throw new MalformedMessageException(message);
                    }

                    length--;
                }
                else
                {
                }
            }

            int currentField = 0;
            foreach (PackInfo d in this.properties.Values)
            {
                if ((d.Mask == 0) || ((mask & d.Mask) != 0))
                {
                    if (length <= currentField)
                    {
                        throw new MalformedMessageException(message);
                    }

                    d.Set(obj, valuesArray[currentField]);
                    currentField++;
                }
            }

            return mask;
        }

        public void Update(object obj, object other, int otherMask)
        {
            if (obj.GetType() != other.GetType())
            {
                throw new Exception("Update cannot be used with objects of different type");
            }

            foreach (PackInfo d in this.properties.Values)
            {
                if ((d.Mask == 0) || ((otherMask & d.Mask) != 0))
                {
                    d.Set(obj, d.Get(other));
                }
            }
        }

        /// <summary>
        /// This method tests if the <code>name</code> property or field is visible (the mask is set to 1).
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="mask">Current mask.</param>
        /// <param name="name">Name of the property or field.</param>
        /// <returns>It returns true if it is visible. False otherwise.</returns>
        public bool IsVisible(object obj, int mask, string name)
        {
            foreach (PackInfo d in this.properties.Values)
            {
                if (object.Equals(d.Name, name))
                {
                    return ((d.Mask == 0) || ((mask & d.Mask) != 0));
                }
            }

            return true;
        }

        /// <summary>
        /// This method gets the visible properties or fields (the mask is set to 1).
        /// Only it return properties and fields affected by <code>PackAttribute</code>.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="mask">Current mask.</param>
        /// <returns>Visible properties or fields.</returns>
        public IEnumerable<string> GetVisible(object obj, int mask)
        {
            foreach (PackInfo d in this.properties.Values)
            {
                if ((d.Mask == 0) || ((mask & d.Mask) != 0))
                {
                    yield return d.Name;
                }
            }
        }

        /// <summary>
        /// This method gets the hidden properties or fields (the mask is set to 0).
        /// Only it return properties and fields affected by <code>PackAttribute</code>.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="mask">Current mask.</param>
        /// <returns>Hidden properties or fields.</returns>
        public IEnumerable<string> GetHidden(object obj, int mask)
        {
            foreach (PackInfo d in this.properties.Values)
            {
                if ((d.Mask != 0) && ((mask & d.Mask) == 0))
                {
                    yield return d.Name;
                }
            }
        }

        public static decimal ToDecimal(string value)
        {
            return decimal.Parse(value, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, enUs);
        }

        public static int ToInt(string value)
        {
            return int.Parse(value, NumberStyles.Integer, enUs);
        }

        public static DateTime ToDateTime(string value)
        {
            long time = long.Parse(value, NumberStyles.Integer, enUs);
            return new DateTime(time, DateTimeKind.Utc);
            //return DateTime.ParseExact(value, "yyyy MMMM dd HH:mm:ss", enUs);
            //return DateTime.Parse(value, enUs, DateTimeStyles.AssumeUniversal);
        }

        public static bool IsPacked(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<PackAttribute>() != null;
        }

        #region private

        private void BuildUsingFieldsFor(Type type)
        {
            foreach (MemberInfo memberInfo in Utils.GetBottomUpPropertiesAndFieldsFor(type).Where(IsPacked))
            {
                PackAttribute packAttr = memberInfo.GetCustomAttribute<PackAttribute>();
                if (packAttr != null)
                {
                    string name = memberInfo.Name;
                    int mask = packAttr.Mask;
                    if (mask == 0)
                    {
                        this.zeroMask++;
                    }

                    Func<object, string> toString = GetToString(memberInfo, Utils.GetReturnType(memberInfo));

                    // Conversion method: string to object.
                    Func<string, object> parse = GetParse(memberInfo, Utils.GetReturnType(memberInfo));

                    string Getter(object obj)
                    {
                        return toString(Utils.GetValue(obj, memberInfo));
                    }

                    void Setter(object obj, string value)
                    {
                        Utils.SetValue(obj, memberInfo, parse(value));
                    }

                    int index = properties.Count;
                    properties.Add(index, new PackInfo(index, name, mask, Getter, Setter));
                }
            }
        }

        private static Func<object, string> GetToString(MemberInfo info, Type type)
        {
            ConvertAttribute convertAttr = info.GetCustomAttribute<ConvertAttribute>();
            if (convertAttr != null)
            {
                const BindingFlags flags = BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public;
                MethodInfo convertMth = convertAttr.Type.GetMethod(convertAttr.ToStringMethodName,
                                                                   flags,
                                                                   null,
                                                                   CallingConventions.Any,
                                                                   new[] { type },
                                                                   new ParameterModifier[0]);
                if (convertMth != null)
                {
                    return s => (string)convertMth.Invoke(null, new object[] { s });
                }
            }

            Func<object, string> convert = PackConverter.Instance.GetToString(type);
            if (convert != null)
            {
                return convert;
            }

            throw new Exception("Cannot convert from " + type + " to string");
        }

        private static Func<string, object> GetParse(MemberInfo info, Type type)
        {
            ConvertAttribute convertAttr = info.GetCustomAttribute<ConvertAttribute>();
            if (convertAttr != null)
            {
                const BindingFlags flags = BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public;
                MethodInfo convertMth = convertAttr.Type.GetMethod(convertAttr.ParseMethodName,
                                                                   flags,
                                                                   null,
                                                                   CallingConventions.Any,
                                                                   new[] { typeof(string) },
                                                                   new ParameterModifier[0]);
                if (convertMth != null)
                {
                    return s => convertMth.Invoke(null, new object[] { s });
                }
            }

            Func<string, object> convert = PackConverter.Instance.GetParse(type);
            if (convert != null)
            {
                return convert;
            }

            throw new Exception("Cannot convert from string to " + type);
        }

        #endregion

        #region Inner classes

        /// <summary>
        /// This class maps a message member into a property.
        /// </summary>
        private class PackInfo
        {
            public PackInfo(int index, string name, int mask, Func<object, string> getter, Action<object, string> setter)
            {
                this.Index = index;
                this.Name = name;
                this.Mask = mask;
                this.getter = getter;
                this.setter = setter;
            }

            public int Index { get; }
            public string Name { get; }
            public int Mask { get; }

            public string Get(object obj)
            {
                return this.getter(obj);
            }

            public void Set(object obj, string value)
            {
                this.setter(obj, value);
            }

            private readonly Func<object, string> getter;
            private readonly Action<object, string> setter;

            public override string ToString()
            {
                return this.Index + " - " + this.Name + " - " + this.Mask;
            }
        }

        #endregion
    }
}