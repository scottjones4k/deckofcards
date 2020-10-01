using DeckOfCards.Repository.Entities;

namespace DeckOfCards.Models
{
    public class DealResultModel : DeckResultModel
    {
        public DealResultModel(Deck deck, Card card) : base (deck)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
