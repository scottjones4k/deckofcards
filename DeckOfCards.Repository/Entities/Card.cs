using DeckOfCards.Repository.Enums;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DeckOfCards.Repository.Tests")]
namespace DeckOfCards.Repository.Entities
{
    public class Card
    {
        public Card(Suit suit, int value)
        {
            if (!Enum.IsDefined(typeof(Value), value))
            {
                throw new ArgumentException($"{value} is invalid for a card value", nameof(value));
            }

            SuitEnum = suit;
            ValueEnum = (Value)value;
        }

        internal Suit SuitEnum { get; }
        internal Value ValueEnum { get; }

        public string Suit => SuitEnum.ToString();
        public string Value => ValueEnum.ToString();
    }
}
