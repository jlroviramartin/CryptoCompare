namespace CryptoCompare.Services
{
    /// <summary>
    /// This class maintains the CryptoCompare configuration.
    /// </summary>
    public class CryptoCompareConfig
    {
        public CryptoCompareConfig()
        {
            this.ApiUrl = "https://min-api.cryptocompare.com";
            this.StreamerUrl = "https://streamer.cryptocompare.com";
        }

        /// <summary>
        /// Base url for the API services.
        /// <example>https://min-api.cryptocompare.com</example>
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Base url for the streamer services.
        /// <example>https://streamer.cryptocompare.com</example>
        /// </summary>
        public string StreamerUrl { get; set; }
    }
}