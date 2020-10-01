export enum DeckAction {
    Shuffle,
    Deal,
    Reset
};

export interface Card {
    suit: string;
    value: string;
}

export interface CardDeck {
    id: string;
    cards: Card[];
    shuffled: boolean;
}

export interface DeckResultModel {
    deck: CardDeck;
}

export interface DealResultModel extends DeckResultModel{
    card: Card;
}