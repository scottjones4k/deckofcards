using DeckOfCards.Repository.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DeckOfCards.Repository.Storage
{
    [ExcludeFromCodeCoverage]
    public class InMemoryDataContext : IDataContext
    {
        public IList<Deck> Decks { get; } = new List<Deck>();
        public IList<DeckSession> DeckSessions { get; } = new List<DeckSession>();
    }
}
