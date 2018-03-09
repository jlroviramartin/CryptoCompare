using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using CryptoCompare.Attributes;

namespace CryptoCompare
{
    public class Utils
    {
        #region fields

        private static readonly CultureInfo enUs = CultureInfo.GetCultureInfo("en-US");

        #endregion

        /// <summary>
        /// This methods implements 'ToString' for the object <code>obj</code> using reflection.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>ToString.</returns>
        public static string ReflectionToString(object obj)
        {
            StringBuilder buff = new StringBuilder();

            Type type = obj.GetType();
            foreach (MemberInfo memberInfo in GetBottomUpPropertiesAndFieldsFor(type))
            {
                object value = Utils.GetValue(obj, memberInfo);

                FormatAttribute formatAttr = memberInfo.GetCustomAttribute<FormatAttribute>();
                string format = (formatAttr != null) ? formatAttr.Format : "";

                buff.AppendFormat("{0} = {1}", memberInfo.Name, ToString(value, format));

                SymbolAttribute symbolAttr = memberInfo.GetCustomAttribute<SymbolAttribute>();
                if ((symbolAttr != null) && (symbolAttr.SymbolProperty != null))
                {
                    object symbol = GetValue(obj, symbolAttr.SymbolProperty);
                    if (symbol != null)
                    {
                        buff.AppendFormat(" {0}", ToString(symbol, ""));
                    }
                }

                buff.AppendLine();
            }

            return buff.ToString();
        }

        /// <summary>
        /// This methods implements 'ToString' for the object <code>obj</code> using reflection.
        /// <code>Filter</code> allows you to filter properties or fields: which properties are visibles.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="filter">Filter properties: which properties are visibles.</param>
        /// <returns>ToString.</returns>
        public static string ReflectionToString(object obj, Func<string, bool> filter)
        {
            StringBuilder buff = new StringBuilder();

            Type type = obj.GetType();
            foreach (MemberInfo memberInfo in GetBottomUpPropertiesAndFieldsFor(type))
            {
                if (filter(memberInfo.Name))
                {
                    object value = Utils.GetValue(obj, memberInfo);

                    FormatAttribute formatAttr = memberInfo.GetCustomAttribute<FormatAttribute>();
                    string format = (formatAttr != null) ? formatAttr.Format : "";

                    buff.AppendFormat("{0} = {1}", memberInfo.Name, ToString(value, format));

                    SymbolAttribute attr = memberInfo.GetCustomAttribute<SymbolAttribute>();
                    if (attr != null && attr.SymbolProperty != null)
                    {
                        object symbol = GetValue(obj, attr.SymbolProperty);
                        if (symbol != null)
                        {
                            buff.AppendFormat(" {0}", ToString(symbol, ""));
                        }
                    }

                    buff.AppendLine();
                }
            }

            return buff.ToString();
        }

        /// <summary>
        /// This methods implements 'GetHashCode' for the object <code>obj</code> using reflection.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>GetHashCode.</returns>
        public static int ReflectionGetHashCode(object obj)
        {
            int hashCode = 7;

            Type type = obj.GetType();
            foreach (MemberInfo memberInfo in GetBottomUpPropertiesAndFieldsFor(type))
            {
                object value = Utils.GetValue(obj, memberInfo);
                hashCode = 31 * hashCode + ((value != null) ? value.GetHashCode() : 0);
            }

            return hashCode;
        }

        /// <summary>
        /// This methods implements 'Equals' for the objects <code>obj</code> and <code>other</code> using reflection.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="other">Other object.</param>
        /// <returns>Equals.</returns>
        public static bool ReflectionEquals(object obj, object other)
        {
            if (obj == other)
            {
                return true;
            }

            if (obj == null || other == null)
            {
                return false;
            }

            if (obj.GetType() != other.GetType())
            {
                return false;
            }

            Type type = obj.GetType();
            foreach (MemberInfo memberInfo in GetBottomUpPropertiesAndFieldsFor(type))
            {
                object value = Utils.GetValue(obj, memberInfo);
                object otherValue = Utils.GetValue(other, memberInfo);
                if (!Object.Equals(value, otherValue))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This methods implements 'Clone' for the object <code>memberwiseCloned</code> using reflection.
        /// The object <code>obj</code> must be the object returned by <code>memberwiseCloned</code>.
        /// This method searchs properties derived from <code>ICloneable</code> and it clones them.
        /// </summary>
        /// <param name="memberwiseCloned">Object.</param>
        /// <returns>Clone.</returns>
        public static void ReflectionClone(object memberwiseCloned)
        {
            Type type = memberwiseCloned.GetType();
            foreach (MemberInfo memberInfo in GetBottomUpPropertiesAndFieldsFor(type))
            {
                object value = Utils.GetValue(memberwiseCloned, memberInfo);
                if (value is ICloneable && !IsReadonly(memberInfo))
                {
                    Utils.SetValue(memberwiseCloned, memberInfo, ((ICloneable)value).Clone());
                }
            }
        }

        /// <summary>
        /// This method gets all the public properties and fields of the class <code>type</code> in order: from the derived classes to the
        /// current class.
        /// Similar to <code>GetMembers(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)</code>
        /// but in bottom-up order.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetBottomUpPropertiesAndFieldsFor(Type type)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public;

            Type baseType = type.BaseType;
            if (baseType != null)
            {
                return GetBottomUpPropertiesAndFieldsFor(baseType)
                    .Concat(type.GetMembers(flags).Where(x => x is PropertyInfo | x is FieldInfo));
            }
            else
            {
                return type.GetMembers(flags).Where(x => x is PropertyInfo | x is FieldInfo);
            }
        }

        /// <summary>
        /// This method gets (using reflection) the value of the <code>name</code> property or field of the object <code>obj</code>.
        /// The property must be public and can be static o instance property.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="name">Name of the property or field.</param>
        /// <returns></returns>
        public static object GetValue(object obj, string name)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public;

            Type type = obj.GetType();

            PropertyInfo propertyInfo = type.GetProperty(name, flags);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(obj);
            }

            FieldInfo fieldInfo = type.GetField(name, flags);
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(fieldInfo.IsStatic ? null : obj);
            }

            throw new Exception("Must be a property or field: " + name);
        }

        /// <summary>
        /// This method gets (using reflection) the value of the <code>memberInfo</code> property or field of the object <code>obj</code>.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="memberInfo">Property or field.</param>
        /// <returns>Value.</returns>
        public static object GetValue(object obj, MemberInfo memberInfo)
        {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (memberInfo is PropertyInfo)
            {
                return propertyInfo.GetValue(obj);
            }

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(fieldInfo.IsStatic ? null : obj);
            }

            throw new Exception("MemberInfo must be a property or field: " + memberInfo);
        }

        /// <summary>
        /// This method sets (using reflection) the <code>value</code> of the <code>name</code> property or field of the object <code>obj</code>.
        /// The property must be public and can be static o instance property.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="name">Property or field.</param>
        /// <param name="value">Value.</param>
        public static void SetValue(object obj, string name, object value)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public;

            Type type = obj.GetType();

            PropertyInfo propertyInfo = type.GetProperty(name, flags);
            if (propertyInfo != null)
            {
                if (!propertyInfo.CanWrite)
                {
                    throw new Exception("It is a readonly property or field: " + name);
                }
                propertyInfo.SetValue(obj, value);
                return;
            }

            FieldInfo fieldInfo = type.GetField(name, flags);
            if (fieldInfo != null)
            {
                if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral)
                {
                    throw new Exception("It is a readonly property or field: " + name);
                }
                fieldInfo.SetValue(fieldInfo.IsStatic ? null : obj, value);
                return;
            }

            throw new Exception("Must be a property or field: " + name);
        }

        /// <summary>
        /// This method sets (using reflection) the <code>value</code> of the <code>memberInfo</code> property or field of the object <code>obj</code>.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="memberInfo">Property or field.</param>
        /// <param name="value">Value.</param>
        public static void SetValue(object obj, MemberInfo memberInfo, object value)
        {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                if (!propertyInfo.CanWrite)
                {
                    throw new Exception("It is a readonly property or field: " + memberInfo);
                }
                propertyInfo.SetValue(obj, value);
                return;
            }

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral)
                {
                    throw new Exception("It is a readonly property or field: " + memberInfo);
                }
                fieldInfo.SetValue(fieldInfo.IsStatic ? null : obj, value);
                return;
            }

            throw new Exception("MemberInfo must be a property or field: " + memberInfo);
        }

        /// <summary>
        /// This method gets (using reflection) the 'return' type of the <code>memberInfo</code> property or field.
        /// </summary>
        /// <param name="memberInfo">Property or field</param>
        /// <returns>'Return' type.</returns>
        public static Type GetReturnType(MemberInfo memberInfo)
        {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            throw new Exception("MemberInfo must be a property or field: " + memberInfo);
        }

        /// <summary>
        /// This method tests if the property or field is readonly.
        /// </summary>
        /// <param name="memberInfo">Property or field.</param>
        /// <returns>True if the property or field is readonly. False otherwise.</returns>
        public static bool IsReadonly(MemberInfo memberInfo)
        {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return !propertyInfo.CanWrite;
            }

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.IsInitOnly || fieldInfo.IsLiteral;
            }

            throw new Exception("MemberInfo must be a property or field: " + memberInfo);
        }

        /// <summary>
        /// This method converts the <code>items</code> enumerable into a string.
        /// </summary>
        /// <param name="items">Enumerable.</param>
        /// <param name="sep">Separator.</param>
        /// <returns>String.</returns>
        public static string AggregateString(IEnumerable<object> items, string sep = ",")
        {
            return items.Aggregate(new StringBuilder(), (a, b) => (a.Length == 0) ? a.Append(b) : a.Append(sep).Append(b)).ToString();
        }

        #region private

        private static string ToString(object value, string format)
        {
            if (value is IFormattable)
            {
                return ((IFormattable)value).ToString(format, enUs);
            }
            else if (value != null)
            {
                return value.ToString();
            }

            return "";
        }

        #endregion
    }
}