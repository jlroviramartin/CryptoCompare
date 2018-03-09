using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoCompare.Services;

namespace CryptoCompare
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //ShowCoins();
            //ShowSubscriptions();
            TestWebSocket();
        }

        public static void ShowCoins()
        {
            ICryptoCompareServices services = new CryptoCompareServices(new CryptoCompareConfig());
            CoinList coinList = services.CoinlistAsync(null, null).Result;
            Debug.WriteLine(coinList);
            foreach (KeyValuePair<string, CoinData> keyValuePair in coinList.Data)
            {
                Debug.WriteLine(keyValuePair.Key + " : " + keyValuePair.Value.FullName);
            }
        }

        public static void ShowSubscriptions()
        {
            try
            {
                ICryptoCompareServices services = new CryptoCompareServices(new CryptoCompareConfig());
                foreach (KeyValuePair<string, SubscriptionByPair> keyValuePair in services.SubsAsync("BTC", new[] { "EUR" }, null, null).Result)
                {
                    Debug.WriteLine(keyValuePair.Key);
                    Debug.Indent();
                    {
                        Debug.WriteLine("TRADES");
                        Debug.Indent();
                        foreach (string s in keyValuePair.Value.TRADES)
                        {
                            Debug.WriteLine(s);
                        }
                        Debug.Unindent();

                        Debug.WriteLine("Current");
                        Debug.Indent();
                        foreach (string s in keyValuePair.Value.CURRENT)
                        {
                            Debug.WriteLine(s);
                        }
                        Debug.Unindent();

                        Debug.WriteLine("CURRENTAGG");
                        Debug.Indent();
                        Debug.WriteLine(keyValuePair.Value.CURRENTAGG);
                        Debug.Unindent();
                    }
                    Debug.Unindent();
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static void TestWebSocket()
        {
            CryptoCompareConfig config = new CryptoCompareConfig();
            ICryptoCompareServices services = new CryptoCompareServices(config);
            ICryptoCompareStreamServices streamServices = new CryptoCompareStreamServices(services, config);

            MessageKey keyCurrent = new MessageKey
            {
                Type = MessageType.Current,
                Market = "Cryptsy",
                FromSymbol = "BTC",
                ToSymbol = "EUR"
            };

            MessageKey keyCurrentAgg = new MessageKey
            {
                Type = MessageType.CurrentAggregation,
                Market = "CCCAGG",
                FromSymbol = "BTC",
                ToSymbol = "EUR"
            };

            MessageKey keyTrade = new MessageKey
            {
                Type = MessageType.Trade,
                Market = "Cryptsy",
                FromSymbol = "BTC",
                ToSymbol = "EUR"
            };

            /*Current current = new Current();
            streamServices.Subscribe(keyCurrent, (sender, args) =>
            {
                Current update = args.Message as Current;
                current.Update(update);
                Debug.WriteLine(current);
            });*/

            Trade trade = new Trade();
            streamServices.Subscribe(keyTrade, (sender, args) =>
            {
                Trade update = args.Message as Trade;
                trade.Update(update);
                Debug.WriteLine(trade);
            });

            streamServices.Error += (sender, args) =>
            {
                Debug.WriteLine(args.Type + " : " + args.Data);
            };

            Task task = Task.Run(() => streamServices.Run());
            task.Wait();
        }
    }
}