using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Brokers
{
    public interface IBrokerage
    {

        /// <summary>
        /// Brokerages Ticker Data
        /// </summary>
        ITicker Ticker { get; set; }

        /// <summary>
        /// Login to the brokerage
        /// </summary>
        /// <param name="token">OAuth token of the account to use with the brokerage</param>
        /// <returns>True if successful</returns>
        bool Login(string token);
    }
}
