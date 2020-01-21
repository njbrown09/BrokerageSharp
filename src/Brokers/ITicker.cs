using BrokerageSharp.Data;
using BrokerageSharp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Brokers
{
    public interface ITicker
    {
        /// <summary>
        /// Get the current realtime quote for a symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Quote GetQuote(string symbol);

        /// <summary>
        /// Get the current realtime quotes for several stocks in batch.
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        List<Quote> GetQuotes(List<string> symbols);

        /// <summary>
        /// Get the candlestick history for a given symbol
        /// </summary>
        /// <param name="symbol">Market symbol of the stock</param>
        /// <param name="frequency">Candlestick Frequency type</param>
        /// <returns>List of candlesticks</returns>
        List<CandleStick> GetHistory(string symbol, DateTime startTime, DateTime endTime, Frequency frequency);
    }
}
