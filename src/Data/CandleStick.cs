using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Data
{
    public struct CandleStick
    {
        /// <summary>
        /// Symbol of the stock associated with this candlestick
        /// </summary>
        public string Symbol;

        /// <summary>
        /// Time of the candlestick
        /// </summary>
        public DateTime Time;

        /// <summary>
        /// Opening price of the candlestick
        /// </summary>
        public Decimal Open;

        /// <summary>
        /// Closing price of the candlestick
        /// </summary>
        public Decimal Close;

        /// <summary>
        /// High price of the candlestick
        /// </summary>
        public Decimal High;

        /// <summary>
        /// Low price of the candlestick
        /// </summary>
        public Decimal Low;

        /// <summary>
        /// Volume during the candlestick
        /// </summary>
        public long Volume;
    }
}
