using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Data
{
    public struct Quote
    {
        /// <summary>
        /// Symbol of the stock associated with this quote
        /// </summary>
        public string Symbol;

        /// <summary>
        /// Description of the stock associated with this quote
        /// </summary>
        public string Description;

        /// <summary>
        /// Current bid price of the stock
        /// </summary>
        public Decimal BidPrice;

        /// <summary>
        /// Available shared at the current bid price
        /// </summary>
        public int BidSize;

        /// <summary>
        /// Lowest price the stock is willing to be sold at
        /// </summary>
        public Decimal AskPrice;

        /// <summary>
        /// Amount of shares available to purchase at the ask price
        /// </summary>
        public int AskSize;

        /// <summary>
        /// Opening price of the stock
        /// </summary>
        public Decimal OpenPrice;
        
        /// <summary>
        /// Highest price of the stock
        /// </summary>
        public Decimal HighPrice;

        /// <summary>
        /// Lowest price of the stock
        /// </summary>
        public Decimal LowPrice;

        /// <summary>
        /// Closing price of the stock
        /// </summary>
        public Decimal ClosePrice;

        /// <summary>
        /// Volume of the stock
        /// </summary>
        public int Volume;

        /// <summary>
        /// Time of this quote
        /// </summary>
        public DateTime Time;

        /// <summary>
        /// Exchange this stock is on
        /// </summary>
        public string Exchange;

        /// <summary>
        /// Volatility of the stock
        /// </summary>
        public float Volatility;

        /// <summary>
        /// Is this data delayed (Old)?
        /// </summary>
        public bool Delayed;
    }
}
