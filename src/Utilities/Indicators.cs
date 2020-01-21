using BrokerageSharp.Data;
using System;
using System.Collections.Generic;

namespace BrokerageSharp.Utilities
{
    public static class Indicators
    {

        /// <summary>
        /// Calculate the simple moving average for the given candlesticks
        /// </summary>
        /// <param name="candlesticks">Candlestick history to be used to calculate SMA</param>
        /// <returns>Simple moving average of the given candlesticks.</returns>
        public static Decimal SimpleMovingAverage(List<CandleStick> candlesticks)
        {

            //SMA is literally average so lets make a variable to hold the total
            Decimal priceTotal = 0m;

            //Loop through the candlesticks and add them all to the price total
            foreach(var candlestick in candlesticks)
            {
                //Add the price to the price total
                priceTotal += candlestick.Close;
            }


            //Calculate and return the average
            return priceTotal / candlesticks.Count;
        }

        /// <summary>
        /// Calculate the latest exponential moving average for the given candlesticks
        /// </summary>
        /// <param name="candlesticks">Candlestick history used to calculate EMA</param>
        /// <returns>Exponential moving average of the given candlesticks.</returns>
        public static Decimal ExponentialMovingAverage(List<CandleStick> candlesticks)
        {

            //EMA requires previous EMA, but since we dont have one, we can start it off with SMA
            Decimal currentEMA = SimpleMovingAverage(candlesticks);

            //Loop through the candlesticks and calculate the EMA for each
            foreach(var candlestick in candlesticks)
            {

                //Calculate the smoothing constant
                Decimal smoothingConstant = 2.0m / (candlesticks.Count + 1.0m);

                //Calculate the EMA for the candlestick
                currentEMA = (candlestick.Close - currentEMA) * smoothingConstant + currentEMA;
            }

            //Return the final EMA
            return currentEMA;
        }

        /// <summary>
        /// Calculate the standard deviation for the given candlesticks
        /// </summary>
        /// <param name="candlesticks">Candlestick history used to calculate standard deviation.</param>
        /// <returns>Standard deviation of the given candlesticks</returns>
        public static float StandardDeviation(List<CandleStick> candlesticks)
        {

            //Calculate the average price of the given candlesticks
            Decimal meanPrice = SimpleMovingAverage(candlesticks);

            //Hold the sum of the squared deviations
            Decimal squaredDeviationTotal = 0m;

            //Loop through each candlestick and calculate deviation, then square it and add it to the sum
            foreach(var candlestick in candlesticks)
            {

                //Calculate deviation
                Decimal deviation = candlestick.Close - meanPrice;

                //Square the deviation and add it to the total
                squaredDeviationTotal += (Decimal)Math.Pow((double)deviation, 2);
            }

            //Divide the squared total by the number of observations
            Decimal averageSquaredDeviation = squaredDeviationTotal / candlesticks.Count;

            //Return the square root of the average squared deviation
            return (float)Math.Sqrt((double)averageSquaredDeviation);
        }

        /// <summary>
        /// Calculate the latest bollinger bands from the given candlesticks
        /// </summary>
        /// <param name="candlesticks">Candlestick history used to calculate bollinger bands</param>
        /// <returns>Bollinger Bands</returns>
        public static (Decimal lower, Decimal middle, Decimal Upper) BollingerBands(List<CandleStick> candlesticks)
        {

            //Calculate the SMA
            Decimal simpleMovingAverage = SimpleMovingAverage(candlesticks);

            //Calculate the standard deviation of price
            float standardDeviation = StandardDeviation(candlesticks);

            //Calculate the lower band
            Decimal lowerBand = simpleMovingAverage - ((Decimal)standardDeviation * 2m);

            //Calculate the middle band
            Decimal middleBand = simpleMovingAverage;

            //Calculate the upper band
            Decimal upperBand = simpleMovingAverage + ((Decimal)standardDeviation * 2m);

            //Return the bollinger bands
            return (lowerBand, middleBand, upperBand);
        }

        /// <summary>
        /// Calculate the latest RSI from the iven candlesticks
        /// </summary>
        /// <param name="candlesticks">Candlestick history used to calculate the RSI. Using more than 14 candles is not reccomended for accuracy.</param>
        /// <returns>Relative Strength Index</returns>
        public static double RelativeStrengthIndex(List<CandleStick> candlesticks)
        {

            //Hold the total for calculating average up and average down seperately
            double gainTotal = 0d;
            int gainCount = 0;

            double lossTotal = 0d;
            int lossCount = 0;

            //Loop through all candlesticks and add them to their respective average
            foreach(var candlestick in candlesticks)
            {
                //Calculate the gain from this candlestick
                double gain = (double)candlestick.Close - (double)candlestick.Open;

                //Check if the candlestick gained money
                if (gain > 0.0)
                {

                    //Add the delta to the total
                    gainTotal += gain;

                    //Increment the up total
                    gainCount++;
                }

                //Check if the candlestick lost money
                if (gain < 0.0)
                {

                    //Add the delta to the total
                    lossTotal += gain;

                    //Increment the down total
                    lossCount++;
                }
            }

            //Calculate average gain
            double averageGain = gainTotal / gainCount;

            //Calculate average loss and remove negative sign
            double averageLoss = Math.Abs(lossTotal / lossCount);

            //Calculate relative strength
            double relativeStrength = averageGain / averageLoss;

            //Calculate and return RSI
            return 100.0 - (100 / (1 + relativeStrength));
        }
    }
}
