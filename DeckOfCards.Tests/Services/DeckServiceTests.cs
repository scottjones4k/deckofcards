using DeckOfCards.Repository;
using DeckOfCards.Repository.Entities;
using DeckOfCards.Services;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DeckOfCards.Tests.Services
{
    public class DeckServiceTests
    {
        private Mock<ISessionService> MockSessionService;
        private Mock<IDeckRepository> MockDeckRepository;
        private DeckService DeckService;

        private const string SESSION_ID = nameof(SESSION_ID);

        [SetUp]
        public void Setup()
        {
            MockSessionService = new Mock<ISessionService>();
            MockDeckRepository = new Mock<IDeckRepository>();

            DeckService = new DeckService(MockSessionService.Object, MockDeckRepository.Object);

            MockSessionService.Setup(s => s.GetSessionId()).Returns(SESSION_ID);
        }

        [Test]
        public void GetDeckExistingReturnsDeck()
        {
            // Given
            var deck = Deck.Create();
            MockDeckRepository.Setup(s => s.GetDeckForSession(SESSION_ID))
                .Returns(deck)
                .Verifiable();

            // When
            var actual = DeckService.GetDeck().Deck;

            // Then
            Assert.AreEqual(deck, actual);
            MockDeckRepository.VerifyAll();
            MockDeckRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void GetDeckNotExistingCreatesDeck()
        {
            // Given
            var deck = Deck.Create();
            MockDeckRepository.Setup(s => s.GetDeckForSession(SESSION_ID))
                .Returns((Deck)null)
                .Verifiable();
            MockDeckRepository.Setup(s => s.CreateDeckForSession(SESSION_ID))
                .Returns(deck)
                .Verifiable();

            // When
            var actual = DeckService.GetDeck().Deck;

            // Then
            Assert.AreEqual(deck, actual);
            MockDeckRepository.VerifyAll();
            MockDeckRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void DealFromDeckReturnsExpected()
        {
            // Given
            var deck = Deck.Create();
            MockDeckRepository.Setup(s => s.GetDeckForSession(SESSION_ID))
                .Returns(deck);
            var expectedCard = deck.Cards.First();

            // When
            var actual = DeckService.DealFromDeck();

            // Then
            Assert.AreEqual(deck, actual.Deck);
            Assert.AreEqual(expectedCard, actual.Card);
        }

        [Test]
        public void ShuffleDeckShuffles()
        {
            // Given
            var deck = Deck.Create();
            MockDeckRepository.Setup(s => s.GetDeckForSession(SESSION_ID))
                .Returns(deck);

            // When
            var actual = DeckService.ShuffleDeck().Deck;

            // Then
            Assert.AreEqual(deck, actual);
            Assert.IsTrue(actual.Shuffled);
        }

        [Test]
        public void ResetDeckDeletesAndCreates()
        {
            // Given
            var deck = Deck.Create();
            MockDeckRepository.Setup(s => s.CreateDeckForSession(SESSION_ID))
                .Returns(deck)
                .Verifiable();
            MockDeckRepository.Setup(s => s.DeleteDeckForSession(SESSION_ID))
                .Verifiable();

            // When
            var actual = DeckService.ResetDeck().Deck;

            // Then
            Assert.AreEqual(deck, actual);

            MockDeckRepository.VerifyAll();
            MockDeckRepository.VerifyNoOtherCalls();
        }
    }
}