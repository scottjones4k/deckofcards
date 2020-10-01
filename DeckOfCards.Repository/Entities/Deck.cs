using DeckOfCards.Repository.Enums;
using System;
using System.Collections.Generic;

namespace DeckOfCards.Repository.Entities
{
    public class Deck
    {
        private Deck() { }

        public Guid Id { get; private set;  }
        public bool Shuffled { get; private set; }

        public IReadOnlyList<Card> Cards { get => InternalCards; }
        private List<Card> InternalCards { get; } = new List<Card>();

        public static Deck Create()
        {
            var deck = new Deck
            {
                Id = Guid.NewGuid(),
                Shuffled = false
            };

            foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                for(int i = 2; i < 15; i++)
                {
                    deck.InternalCards.Add(new Card(suit, i));
                }
            }
            return deck;
        }

        public bool Shuffle()
        {
            if (InternalCards.Count < 52)
            {
                return false;
            }

            InternalCards.Shuffle();
            Shuffled = true;
            return true;
        }

        public Card Pop() => InternalCards.Pop();
    }
}
