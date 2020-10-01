using DeckOfCards.Models;

namespace DeckOfCards.Services
{
    public interface IDeckService
    {
        DeckResultModel GetDeck();
        DealResultModel DealFromDeck();
        DeckResultModel ShuffleDeck();
        DeckResultModel ResetDeck();
    }
}