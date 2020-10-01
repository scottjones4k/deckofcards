using DeckOfCards.Models;
using DeckOfCards.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace DeckOfCards.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        [HttpGet]
        public DeckResultModel Get() =>
            _deckService.GetDeck();

        [HttpPost("deal")]
        public DealResultModel Deal() =>
            _deckService.DealFromDeck();

        [HttpPost("shuffle")]
        public DeckResultModel Shuffle() =>
            _deckService.ShuffleDeck();

        [HttpPost("reset")]
        public DeckResultModel Reset() =>
            _deckService.ResetDeck();
    }
}
