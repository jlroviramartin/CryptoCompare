namespace CryptoCompare
{
    /// <summary>
    /// Type of the message.
    /// </summary>
    public enum MessageType
    {
        Trade = 0,
        FeedNews = 1,
        Current = 2,
        LoadComplete = 3,
        CoinPairs = 4,
        CurrentAggregation = 5,
        TopList = 6,
        TopListChange = 7,
        OrderBook = 8,
        FullOrderBook = 9,
        Activation = 10,

        TradeCatchup = 100,
        NewsCatchup = 101,

        TradeCatchupComplete = 300,
        NewsCatchupComplete = 301
    }
}