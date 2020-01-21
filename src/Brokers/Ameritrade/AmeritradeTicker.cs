using BrokerageSharp.Data;
using BrokerageSharp.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BrokerageSharp.Brokers.Ameritrade
{
    public class AmeritradeTicker : ITicker
    {
        /// <summary>
        /// Http client used to perform network requests
        /// </summary>
        private readonly HttpClient _HttpClient = new HttpClient();

        /// <summary>
        /// Base url endpoint of this secton of the api
        /// </summary>
        private readonly string _BaseApiUrl = "https://api.tdameritrade.com/v1/marketdata";

        /// <summary>
        /// Users OAuth Token
        /// </summary>
        private string _AccountToken;

        /// <summary>
        /// Set the ameritrade oauth token
        /// </summary>
        /// <param name="accountToken">OAuth token of the account used to trade</param>
        internal void SetToken(string accountToken)
        {
            //Store the account token
            _AccountToken = accountToken;

            //Set the autorization on the http client
            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _AccountToken);
        }

        public List<CandleStick> GetHistory(string symbol, DateTime startTime, DateTime endTime, Frequency frequency)
        {

            //Create the target URL
            string targetUrl = _BaseApiUrl + $"/{symbol}/pricehistory";

            //Convert the start and end times to UTC
            startTime = startTime.ToUniversalTime();
            endTime = endTime.ToUniversalTime();

            //Convert the start and endtimes to unix epoch milliseconds
            long startDateMilliseconds = ((DateTimeOffset)startTime).ToUnixTimeMilliseconds();
            long endDateMilliseconds = ((DateTimeOffset)endTime).ToUnixTimeMilliseconds();

            //Add the parameters to the URL
            targetUrl += $"?startDate={startDateMilliseconds}&";
            targetUrl += $"endDate={endDateMilliseconds}&";
            targetUrl += $"frequencyType={frequency.ToString().ToLower()}&";
            targetUrl += $"frequency=1";

            //Send the HTTP Request 
            var result = _HttpClient.GetAsync(targetUrl).Result;

            //Read the result string
            var response = result.Content.ReadAsStringAsync().Result;

            //Parse the main part of the response
            var responseJson = JsonConvert.DeserializeObject<JSON.Ticker.PriceHistory>(response);

            //Create a list of candlesticks to send back
            var candlesticks = new List<CandleStick>();

            //Loop through all JSON and parse it to the universal candlestick type
            foreach (var candlestick in responseJson.candles)
            {

                //Create a new candlestick struct
                var finalCandlestick = new CandleStick
                {
                    Symbol = symbol,
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(candlestick.datetime).UtcDateTime,
                    Open = (Decimal)candlestick.open,
                    Close = (Decimal)candlestick.close,
                    High = (Decimal)candlestick.high,
                    Low = (Decimal)candlestick.low,
                    Volume = candlestick.volume
                };

                //Check that the candlestick is in the specified bounds
                if (finalCandlestick.Time < startTime || finalCandlestick.Time > endTime)
                    continue;

                //Add the candlestick to the candlestick list
                candlesticks.Add(finalCandlestick);
            }

            //Return the candlesticks
            return candlesticks;
        }

        public Quote GetQuote(string symbol)
        {

            //Make a list just containing the one symbol
            var symbolList = new List<string>();

            //Add the symbol to the list
            symbolList.Add(symbol);

            //Get the quote for the symbol
            var quote = GetQuotes(symbolList).First();

            //Return the quote
            return quote;
        }

        public List<Quote> GetQuotes(List<string> symbols)
        {

            // Create the target URL
            string targetUrl = _BaseApiUrl + $"/quotes";

            //Add the parameters to the URL
            targetUrl += $"?symbol=";

            //Loop through each symbol and add it to the URL
            foreach (var symbol in symbols)
            {

                //Add it to the target URL
                targetUrl += symbol + ",";
            }

            //Send the HTTP Request 
            var result = _HttpClient.GetAsync(targetUrl).Result;

            //Read the result string
            var response = result.Content.ReadAsStringAsync().Result;

            //Parse the main part of the response
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, JSON.Ticker.Quote>>(response);

            //Create a list of quotes to send back
            var quotes = new List<Quote>();

            //Loop through all JSON and parse it to the universal quote type
            foreach (var quote in responseJson)
            {

                //Create a new quote struct
                var finalQuote = new Quote
                {
                    Symbol = quote.Key,
                    Description = quote.Value.description,
                    BidPrice = (Decimal)quote.Value.bidPrice,
                    BidSize = quote.Value.bidSize,
                    AskPrice = (Decimal)quote.Value.askPrice,
                    AskSize = quote.Value.askSize,
                    OpenPrice = (Decimal)quote.Value.openPrice,
                    HighPrice = (Decimal)quote.Value.highPrice,
                    LowPrice = (Decimal)quote.Value.lowPrice,
                    ClosePrice = (Decimal)quote.Value.closePrice,
                    Volume = quote.Value.totalVolume,
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(quote.Value.quoteTimeInLong).UtcDateTime,
                    Exchange = quote.Value.exchangeName,
                    Volatility = quote.Value.volatility,
                    Delayed = quote.Value.delayed
                };

                //Add the quote to the list
                quotes.Add(finalQuote);
            }

            //Return the list
            return quotes;
        }
    }
}
