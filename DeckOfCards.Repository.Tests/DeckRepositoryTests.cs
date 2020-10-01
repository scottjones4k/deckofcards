using DeckOfCards.Repository.Entities;
using DeckOfCards.Repository.Storage;
using NUnit.Framework;
using System;
using System.Linq;

namespace DeckOfCards.Repository.Tests
{
    public class Tests
    {
        private InMemoryDataContext DataContext;
        private DeckRepository Repository;

        [SetUp]
        public void Setup()
        {
            DataContext = new InMemoryDataContext();
            Repository = new DeckRepository(DataContext);
        }

        [Test]
        public void GetDeckForSessionNoSessionReturnsNull()
        {
            // Given
            var session = "SESSION_ID";

            // When
            var deck = Repository.GetDeckForSession(session);

            // Then
            Assert.IsNull(deck);
        }

        [Test]
        public void GetDeckForSessionNoDeckReturnsNull()
        {
            // Given
            var session = "SESSION_ID";
            DataContext.DeckSessions.Add(new DeckSession { DeckId = Guid.NewGuid(), SessionId = session });

            // When
            var deck = Repository.GetDeckForSession(session);

            // Then
            Assert.IsNull(deck);
        }

        [Test]
        public void GetDeckForSessionReturnsDeck()
        {
            // Given
            var session = "SESSION_ID";
            var deck = Deck.Create();
            DataContext.DeckSessions.Add(new DeckSession { DeckId = deck.Id, SessionId = session });
            DataContext.Decks.Add(deck);

            // When
            var actual = Repository.GetDeckForSession(session);

            // Then
            Assert.AreEqual(deck, actual);
        }

        [Test]
        public void CreateDeckForSession()
        {
            // Given
            var session = "SESSION_ID";

            // When
            var deck = Repository.CreateDeckForSession(session);

            // Then
            Assert.IsNotNull(deck);
            Assert.IsAssignableFrom<Deck>(deck);
            CollectionAssert.Contains(DataContext.Decks, deck);
            Assert.IsTrue(DataContext.DeckSessions.Any(s => s.SessionId == session));
        }

        [Test]
        public void CreateDeckForSessionDuplicateSession()
        {
            // Given
            var session = "SESSION_ID";
            DataContext.DeckSessions.Add(new DeckSession { SessionId = session });

            // When
            TestDelegate action = () => Repository.CreateDeckForSession(session);

            // Then
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void DeleteDeckForSessionExists()
        {
            // Given
            var session = "SESSION_ID";
            var deck = Deck.Create();
            DataContext.DeckSessions.Add(new DeckSession { DeckId = deck.Id, SessionId = session });
            DataContext.Decks.Add(deck);

            // When
            Repository.DeleteDeckForSession(session);

            // Then
            Assert.IsFalse(DataContext.DeckSessions.Any(s => s.SessionId == session));
            Assert.IsFalse(DataContext.Decks.Any(s => s.Id == deck.Id));
        }

        [Test]
        public void DeleteDeckForSessionExistsSharedDeck()
        {
            // Given
            var session = "SESSION_ID";
            var deck = Deck.Create();
            DataContext.DeckSessions.Add(new DeckSession { DeckId = deck.Id, SessionId = session });
            DataContext.DeckSessions.Add(new DeckSession { DeckId = deck.Id, SessionId = "ANOTHER_SESSION" });
            DataContext.Decks.Add(deck);

            // When
            Repository.DeleteDeckForSession(session);

            // Then
            Assert.IsFalse(DataContext.DeckSessions.Any(s => s.SessionId == session));
            Assert.IsTrue(DataContext.DeckSessions.Any(s => s.SessionId == "ANOTHER_SESSION"));
            Assert.IsTrue(DataContext.Decks.Any(s => s.Id == deck.Id));
        }

        [Test]
        public void DeleteDeckForSessionDoesntExist()
        {
            // Given
            var session = "SESSION_ID";
            var deck = Deck.Create();
            DataContext.DeckSessions.Add(new DeckSession { DeckId = deck.Id, SessionId = "ANOTHER_SESSION" });
            DataContext.Decks.Add(deck);
            // When
            Repository.DeleteDeckForSession(session);

            // Then
            Assert.IsTrue(DataContext.DeckSessions.Any(s => s.SessionId == "ANOTHER_SESSION"));
            Assert.IsTrue(DataContext.Decks.Any(s => s.Id == deck.Id));
        }
    }
}