using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace PowerPlantDataProvider.DataGenerator
{
    internal static class PowerPlantDataGenerator
    {
        private const int NO_ERROR = -1;

        private static readonly Random _random;
        private static readonly List<AssetParameter> _subscribers;
        private static int _errorIndex;

        static PowerPlantDataGenerator()
        {
            _subscribers = new List<AssetParameter>();
            _random = new Random(DateTime.Now.Millisecond);
            _errorIndex = NO_ERROR;

            var generationTimer = new Timer(100);
            generationTimer.Elapsed += GenerateDataForSubscribers;
            generationTimer.Start();

            var errorTimer = new Timer(10 * 1000);
            errorTimer.Elapsed += GenerateError;
            errorTimer.Start();
        }

        public static void Subscribe(AssetParameter subscriber)
        {
            _subscribers.Add(subscriber);
        }

        private static void GenerateDataForSubscribers(object sender, ElapsedEventArgs e)
        {
            for (var i = 0; i < _subscribers.Count; i++)
            {
                var subscriber = _subscribers[i];

                if (i == _errorIndex)
                {
                    subscriber.CurrentValue = subscriber.MaxValue + (_random.NextDouble() *
                                                   (subscriber.MaxValue - subscriber.MinValue));
                }
                else
                {
                    subscriber.CurrentValue = subscriber.MinValue + (_random.NextDouble() *
                                                   (subscriber.MaxValue - subscriber.MinValue));
                }
            }
        }

        private static void GenerateError(object sender, ElapsedEventArgs e)
        {
            _errorIndex = _random.Next(0, 3) == 0 ? _random.Next(_subscribers.Count) : NO_ERROR;
        }
    }
}