using DeckOfCards.Repository.Entities;

namespace DeckOfCards.Models
{
    public class DeckResultModel
    {
        public DeckResultModel(Deck deck)
        {
            Deck = deck;
        }

        public Deck Deck { get; }
    }
}
