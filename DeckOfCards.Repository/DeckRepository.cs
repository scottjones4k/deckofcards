using DeckOfCards.Repository.Entities;
using DeckOfCards.Repository.Storage;
using System;
using System.Linq;

namespace DeckOfCards.Repository
{
    public class DeckRepository : IDeckRepository
    {
        private readonly IDataContext _dataContext;

        public DeckRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Deck GetDeckForSession(string sessionId) =>
            _dataContext.Decks.FirstOrDefault(d => d.Id ==
                _dataContext.DeckSessions.FirstOrDefault(s => s.SessionId == sessionId)?.DeckId
            );

        public Deck CreateDeckForSession(string sessionId)
        {
            if (_dataContext.DeckSessions.Any(s => s.SessionId == sessionId))
            {
                throw new ArgumentException($"Deck for session {sessionId} already exists", nameof(sessionId));
            }

            var deck = Deck.Create();

            _dataContext.Decks.Add(deck);
            _dataContext.DeckSessions.Add(new DeckSession
            {
                DeckId = deck.Id,
                SessionId = sessionId
            });

            return deck;
        }

        public void DeleteDeckForSession(string sessionId)
        {
            var deckSession = _dataContext.DeckSessions.FirstOrDefault(s => s.SessionId == sessionId);

            if (deckSession == null)
            {
                // No entry to delete
                return;
            }

            _dataContext.DeckSessions.Remove(deckSession);

            // If no more sessions tied to deck, remove deck
            if (!_dataContext.DeckSessions.Any(s => s.DeckId == deckSession.DeckId))
            {
                _dataContext.Decks.Remove(_dataContext.Decks.FirstOrDefault(d => d.Id == deckSession.DeckId));
            }
        }
    }
}
