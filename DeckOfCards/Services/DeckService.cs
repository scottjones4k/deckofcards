using DeckOfCards.Models;
using DeckOfCards.Repository;

namespace DeckOfCards.Services
{
    public class DeckService : IDeckService
    {
        private readonly ISessionService _sessionService;
        private readonly IDeckRepository _deckRepository;

        public DeckService(ISessionService sessionService, IDeckRepository deckRepository)
        {
            _sessionService = sessionService;
            _deckRepository = deckRepository;
        }

        public DeckResultModel GetDeck()
        {
            var sessionId = _sessionService.GetSessionId();

            return new DeckResultModel(_deckRepository.GetDeckForSession(sessionId) ??
                _deckRepository.CreateDeckForSession(sessionId));
        }

        public DealResultModel DealFromDeck()
        {
            var deck = GetDeck().Deck;

            return new DealResultModel(deck, deck.Pop());
        }

        public DeckResultModel ShuffleDeck()
        {
            var deckResult = GetDeck();

            deckResult.Deck.Shuffle();

            return deckResult;
        }

        public DeckResultModel ResetDeck()
        {
            var sessionId = _sessionService.GetSessionId();

            _deckRepository.DeleteDeckForSession(sessionId);

            return new DeckResultModel(_deckRepository.CreateDeckForSession(sessionId));
        }
    }
}
