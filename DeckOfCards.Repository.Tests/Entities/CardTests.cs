using DeckOfCards.Repository.Entities;
using DeckOfCards.Repository.Enums;
using NUnit.Framework;
using System;

namespace DeckOfCards.Repository.Tests
{
    public class CardTests
    {
        [Test]
        public void CreateCardValidInput()
        {
            // Given
            var value = 8;
            var suit = Suit.Hearts;

            // When
            var card = new Card(suit, value);

            // Then
            Assert.AreEqual(Value.Eight.ToString(), card.Value);
            Assert.AreEqual("Hearts", card.Suit);
        }

        [Test]
        public void CreateCardInvalidInput()
        {
            // Given
            var value = 18;
            var suit = Suit.Hearts;

            // When
            TestDelegate testDelegate = () => new Card(suit, value);

            // Then
            Assert.Throws<ArgumentException>(testDelegate, "18 is invalid for a card value");
        }
    }
}