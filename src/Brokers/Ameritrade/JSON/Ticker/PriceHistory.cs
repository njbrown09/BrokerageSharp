using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Brokers.Ameritrade.JSON.Ticker
{
    class PriceHistory
    {
        public List<Candlestick> candles { get; set; }
    }
}
