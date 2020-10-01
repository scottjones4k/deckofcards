using DeckOfCards.Repository.Entities;
using System;

namespace DeckOfCards.Repository
{
    public interface IDeckRepository
    {
        Deck GetDeckForSession(string sessionId);
        Deck CreateDeckForSession(string sessionId);
        void DeleteDeckForSession(string sessionId);
    }
}