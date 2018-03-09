namespace CryptoCompare.Services
{
    partial interface ICryptoCompareServices
    {
        /// <summary>Single Symbol Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> PriceAsync(bool? tryConversion, string fsym, string[] tsyms, string e, string extraParams, bool? sign);

        /// <summary>Single Symbol Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> PriceAsync(bool? tryConversion, string fsym, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken);

        /// <summary>Multiple Symbols Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricemultiAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign);

        /// <summary>Multiple Symbols Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricemultiAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken);

        /// <summary>Multiple Symbols Full Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PriceMultiFull> PricemultifullAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign);

        /// <summary>Multiple Symbols Full Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<PriceMultiFull> PricemultifullAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken);

        /// <summary>Historical Day OHLCV for a timestamp</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="ts">The unix timestamp of interest</param>
        /// <param name="calculationType">Type of average to calculate (Close - a Close of the day close price, MidHighLow - the average between the 24 H high and low, VolFVolT - the total volume to / the total volume from)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricehistoricalAsync(bool? tryConversion, string fsym, string[] tsyms, string e, int? ts, CalculationType? calculationType, string extraParams, bool? sign);

        /// <summary>Historical Day OHLCV for a timestamp</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="ts">The unix timestamp of interest</param>
        /// <param name="calculationType">Type of average to calculate (Close - a Close of the day close price, MidHighLow - the average between the 24 H high and low, VolFVolT - the total volume to / the total volume from)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricehistoricalAsync(bool? tryConversion, string fsym, string[] tsyms, string e, int? ts, CalculationType? calculationType, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken);

        /// <summary>Subs by Pair</summary>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionByPair>> SubsAsync(string fsym, string[] tsyms, string extraParams, bool? sign);

        /// <summary>Subs by Pair</summary>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionByPair>> SubsAsync(string fsym, string[] tsyms, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken);

        /// <summary>Subs Watchlist</summary>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsym">The currency symbol to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionWatchlist>> SubsWatchlistAsync(string[] fsyms, string tsym, string extraParams, string sign);

        /// <summary>Subs Watchlist</summary>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsym">The currency symbol to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionWatchlist>> SubsWatchlistAsync(string[] fsyms, string tsym, string extraParams, string sign, System.Threading.CancellationToken cancellationToken);
    }

    partial class CryptoCompareServices
    {
        #region fields

        private readonly CryptoCompareConfig config;

        #endregion

        public CryptoCompareServices(CryptoCompareConfig config)
            : this(config.ApiUrl)
        {
            this.config = config;
        }

        /// <summary>Single Symbol Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> PriceAsync(bool? tryConversion, string fsym, string[] tsyms, string e, string extraParams, bool? sign)
        {
            return PriceAsync(tryConversion, fsym, BuildList(tsyms), e, extraParams, sign);
        }

        /// <summary>Single Symbol Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, double>> PriceAsync(bool? tryConversion, string fsym, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken)
        {
            return PriceAsync(tryConversion, fsym, BuildList(tsyms), e, extraParams, sign, cancellationToken);
        }

        /// <summary>Multiple Symbols Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricemultiAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign)
        {
            return PricemultiAsync(tryConversion, BuildList(fsyms), BuildList(tsyms), e, extraParams, sign);
        }

        /// <summary>Multiple Symbols Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricemultiAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken)
        {
            return PricemultiAsync(tryConversion, BuildList(fsyms), BuildList(tsyms), e, extraParams, sign, cancellationToken);
        }

        /// <summary>Multiple Symbols Full Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<PriceMultiFull> PricemultifullAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign)
        {
            return PricemultifullAsync(tryConversion, BuildList(fsyms), BuildList(tsyms), e, extraParams, sign);
        }

        /// <summary>Multiple Symbols Full Price</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<PriceMultiFull> PricemultifullAsync(bool? tryConversion, string[] fsyms, string[] tsyms, string e, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken)
        {
            return PricemultifullAsync(tryConversion, BuildList(fsyms), BuildList(tsyms), e, extraParams, sign, cancellationToken);
        }

        /// <summary>Historical Day OHLCV for a timestamp</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="ts">The unix timestamp of interest</param>
        /// <param name="calculationType">Type of average to calculate (Close - a Close of the day close price, MidHighLow - the average between the 24 H high and low, VolFVolT - the total volume to / the total volume from)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricehistoricalAsync(bool? tryConversion, string fsym, string[] tsyms, string e, int? ts, CalculationType? calculationType, string extraParams, bool? sign)
        {
            return PricehistoricalAsync(tryConversion, fsym, BuildList(tsyms), e, ts, calculationType, extraParams, sign);
        }

        /// <summary>Historical Day OHLCV for a timestamp</summary>
        /// <param name="tryConversion">If set to false, it will try to get only direct trading values</param>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="e">The exchange to obtain data from (our aggregated average - CCCAGG - by default)</param>
        /// <param name="ts">The unix timestamp of interest</param>
        /// <param name="calculationType">Type of average to calculate (Close - a Close of the day close price, MidHighLow - the average between the 24 H high and low, VolFVolT - the total volume to / the total volume from)</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>> PricehistoricalAsync(bool? tryConversion, string fsym, string[] tsyms, string e, int? ts, CalculationType? calculationType, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken)
        {
            return PricehistoricalAsync(tryConversion, fsym, BuildList(tsyms), e, ts, calculationType, extraParams, sign, cancellationToken);
        }

        /// <summary>Subs by Pair</summary>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionByPair>> SubsAsync(string fsym, string[] tsyms, string extraParams, bool? sign)
        {
            return SubsAsync(fsym, BuildList(tsyms), extraParams, sign);
        }

        /// <summary>Subs by Pair</summary>
        /// <param name="fsym">The cryptocurrency symbol of interest</param>
        /// <param name="tsyms">Comma separated cryptocurrency symbols list to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionByPair>> SubsAsync(string fsym, string[] tsyms, string extraParams, bool? sign, System.Threading.CancellationToken cancellationToken)
        {
            return SubsAsync(fsym, BuildList(tsyms), extraParams, sign, cancellationToken);
        }

        /// <summary>Subs Watchlist</summary>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsym">The currency symbol to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionWatchlist>> SubsWatchlistAsync(string[] fsyms, string tsym, string extraParams, string sign)
        {
            return SubsWatchlistAsync(BuildList(fsyms), tsym, extraParams, sign);
        }

        /// <summary>Subs Watchlist</summary>
        /// <param name="fsyms">Comma separated cryptocurrency symbols list</param>
        /// <param name="tsym">The currency symbol to convert into</param>
        /// <param name="extraParams">The name of your application (we recommend you send it)</param>
        /// <param name="sign">If set to true, the server will sign the requests (be default we don't sign them), this is useful for usage in smart contracts</param>
        /// <returns>Status 200</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, SubscriptionWatchlist>> SubsWatchlistAsync(string[] fsyms, string tsym, string extraParams, string sign, System.Threading.CancellationToken cancellationToken)
        {
            return SubsWatchlistAsync(BuildList(fsyms), tsym, extraParams, sign, cancellationToken);
        }

        private static string BuildList(string[] items)
        {
            if (items == null)
            {
                return null;
            }
            return Utils.AggregateString(items);
        }
    }
}