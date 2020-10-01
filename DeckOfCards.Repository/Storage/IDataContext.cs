using DeckOfCards.Repository.Entities;
using System.Collections.Generic;

namespace DeckOfCards.Repository.Storage
{
    public interface IDataContext
    {
        IList<Deck> Decks { get; }
        IList<DeckSession> DeckSessions { get; }
    }
}