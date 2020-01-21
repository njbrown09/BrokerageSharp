using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Brokers.Ameritrade.JSON.Ticker
{
    class Candlestick
    {
        public double close { get; set; }

        public long datetime { get; set; }

        public double high { get; set; }

        public double low { get; set; }

        public double open { get; set; }

        public int volume { get; set; }
    }
}
