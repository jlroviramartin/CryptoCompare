//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v9.10.28.0 (Newtonsoft.Json v11.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace CryptoCompare.Services
{
    #pragma warning disable // Disable all warnings

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.28.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Subscription 
    {
        [Newtonsoft.Json.JsonProperty("subs", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.ObjectModel.ObservableCollection<string> Subs { get; set; } = new System.Collections.ObjectModel.ObservableCollection<string>();
    
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        
        public static Subscription FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Subscription>(data);
        }
    }
}