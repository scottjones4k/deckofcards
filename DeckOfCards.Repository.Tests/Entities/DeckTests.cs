using DeckOfCards.Repository.Entities;
using DeckOfCards.Repository.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfCards.Repository.Tests
{
    public class DeckTests
    {
        [Test]
        public void CreateDeckCreatesCorrectly()
        {
            // When
            var deck = Deck.Create();

            // Then
            Assert.AreNotEqual(deck.Id, default(Guid));
            Assert.AreEqual(deck.Cards.Count, 52);

            VerifySuit(deck.Cards.Take(13).ToList(), Suit.Hearts);
            VerifySuit(deck.Cards.Skip(13).Take(13).ToList(), Suit.Diamonds);
            VerifySuit(deck.Cards.Skip(26).Take(13).ToList(), Suit.Clubs);
            VerifySuit(deck.Cards.Skip(39).Take(13).ToList(), Suit.Spades);
        }

        private void VerifySuit(IList<Card> cards, Suit suit)
        {
            // All same suit
            Assert.IsTrue(cards.All(c => c.SuitEnum == suit));

            // In ascending order
            for (var i = 1; i < 13; i++)
            {
                Assert.Greater((int)cards[i].ValueEnum, (int)cards[i - 1].ValueEnum);
            }
        }

        [Test]
        public void PopRemovesFirstCard()
        {
            // Given
            var deck = Deck.Create();
            var expected = deck.Cards[0];

            // When
            var card = deck.Pop();

            // Then
            Assert.AreEqual(expected, card);
            CollectionAssert.DoesNotContain(deck.Cards, card);
            Assert.AreEqual(51, deck.Cards.Count);
        }

        [Test]
        public void PopShuffledRemovesFirstCard()
        {
            // Given
            var deck = Deck.Create();
            deck.Shuffle();
            var expected = deck.Cards[0];

            // When
            var card = deck.Pop();

            // Then
            Assert.AreEqual(expected, card);
            CollectionAssert.DoesNotContain(deck.Cards, card);
            Assert.AreEqual(51, deck.Cards.Count);
        }

        [Test]
        public void ShuffleNotFullReturnsFalseAndDoesntShuffle()
        {
            // Given
            var deck = Deck.Create();
            deck.Pop();
            var initialOrder = deck.Cards.ToList();

            // When
            var shuffled = deck.Shuffle();

            // Then
            Assert.IsFalse(shuffled);
            Assert.IsFalse(deck.Shuffled);
            CollectionAssert.AreEqual(initialOrder, deck.Cards);
        }

        [Test]
        public void ShuffleFullReturnsTrueAndShuffles()
        {
            // Given
            var deck = Deck.Create();
            var initialOrder = deck.Cards.ToList();

            // When
            var shuffled = deck.Shuffle();

            // Then
            Assert.IsTrue(shuffled);
            Assert.IsTrue(deck.Shuffled);
            CollectionAssert.AreNotEqual(initialOrder, deck.Cards);
            CollectionAssert.AreEquivalent(initialOrder, deck.Cards);
        }
    }
}