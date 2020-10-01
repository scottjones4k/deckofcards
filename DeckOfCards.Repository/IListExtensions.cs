using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DeckOfCards.Repository
{
    [ExcludeFromCodeCoverage]
    public static class ListExtensions
    {
        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;

                // Generate index to swap with
                var k = random.Next(n + 1);

                // Swap values
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T Pop<T>(this IList<T> list)
        {
            if (!list.Any())
            {
                // List is empty
                return default;
            }

            // Pick first item
            var item = list[0];
            list.RemoveAt(0);
            return item;
        }
    }
}
