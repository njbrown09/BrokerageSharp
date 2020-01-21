using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageSharp.Brokers.Ameritrade
{
    public class AmeritradeBrokerage : IBrokerage
    {

        public ITicker Ticker { get; set; }

        /// <summary>
        /// OAuth2 Key of the account being used to trade
        /// </summary>
        private string _AccountToken;

        /// <summary>
        /// Log into td ameritrade
        /// </summary>
        /// <param name="token">OAuth2 Account Token</param>
        /// <returns>Is Successful</returns>
        public bool Login(string token)
        {

            //Store the token
            _AccountToken = token;

            //Setup Subclasses
            Ticker = new AmeritradeTicker();
            ((AmeritradeTicker)Ticker).SetToken(_AccountToken);

            return true;
        }
    }
}
