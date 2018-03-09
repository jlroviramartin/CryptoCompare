using System;

namespace CryptoCompare.Attributes
{
    /// <summary>
    /// This attribute points to the property or field which contains the symbol.
    /// </summary>
    /// <example>
    /// public class Current
    /// {
    ///   public string ToSymbol { get; set; }
    ///   
    ///   [Symbol("ToSymbol")]
    ///   public decimal Price { get; set; }
    /// }
    /// 
    /// The symbol of 'Price' property is 'ToSymbol'.
    /// 
    /// new Current ( ToSymbol = "EUR", Price = 7000 }
    /// 
    /// This refers to: 7000 EUR
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SymbolAttribute : Attribute
    {
        public SymbolAttribute(string symbol)
        {
            this.SymbolProperty = symbol;
        }

        public string SymbolProperty { get; }
    }
}