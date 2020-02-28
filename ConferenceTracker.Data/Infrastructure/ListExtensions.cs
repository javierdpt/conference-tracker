using System;
using System.Collections.Generic;

namespace ConferenceTracker.Data.Infrastructure
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this List<T> values)
        {
            var random = new Random();
            var res = new List<T>();
            while (values.Count > 0)
            {
                var randomIndex = random.Next(0, values.Count);
                var randomItem = values[randomIndex];
                values.RemoveAt(randomIndex);
                res.Add(randomItem);
            }
            return res;
        }
    }
}