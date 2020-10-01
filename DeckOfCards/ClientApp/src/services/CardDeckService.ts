import apiService from './ApiService';
import { CardDeck, DeckResultModel, DealResultModel } from '../models/CardDeck';

class CardDeckService {

    async getCardDeck(): Promise<CardDeck> {
        const response = await apiService.fetch(`deck`);
        const result = (await response.json()) as DeckResultModel;
        return result.deck;
    }

    async resetCardDeck(): Promise<CardDeck> {
        const response = await apiService.post(`deck/reset`);
        const result = (await response.json()) as DeckResultModel;
        return result.deck;
    }

    async shuffleCardDeck(): Promise<CardDeck> {
        const response = await apiService.post(`deck/shuffle`);
        const result = (await response.json()) as DeckResultModel;
        return result.deck;
    }

    async dealCardDeck(): Promise<DealResultModel> {
        const response = await apiService.post(`deck/deal`);
        return (await response.json()) as DealResultModel;
    }
}

const cardDeckService = new CardDeckService();

export default cardDeckService;