import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import cardDeckService from '../services/CardDeckService';
import _ from 'lodash';
import { Card, CardDeck, DeckAction } from '../models/CardDeck';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CardDeckState {
    deck?: CardDeck;
    card?: Card;
    lastAction?: DeckAction;
    isLoading: boolean;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCardDeckAction {
    type: 'REQUEST_CARD_DECK';
}

interface ReceiveCardDeckAction {
    type: 'RECEIVE_CARD_DECK';
    deck: CardDeck;
    card?: Card;
}

interface ResetCardDeckAction {
    type: 'RESET_CARD_DECK';
}

interface ShuffleCardDeckAction {
    type: 'SHUFFLE_CARD_DECK';
}

interface DealCardDeckAction {
    type: 'DEAL_CARD_DECK';
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCardDeckAction | ReceiveCardDeckAction | ResetCardDeckAction | ShuffleCardDeckAction | DealCardDeckAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCardDeck: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.cardDeck && !appState.cardDeck.deck && !appState.cardDeck.isLoading) {
            cardDeckService.getCardDeck()
                .then(data => {
                    dispatch({ type: 'RECEIVE_CARD_DECK', deck: data });
                });

            dispatch({ type: 'REQUEST_CARD_DECK' });
        }
    },
    resetCardDeck: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.cardDeck && !appState.cardDeck.isLoading) {
            cardDeckService.resetCardDeck()
                .then(data => {
                    dispatch({ type: 'RECEIVE_CARD_DECK', deck: data });
                });

            dispatch({ type: 'RESET_CARD_DECK' });
        }
    },
    shuffleCardDeck: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.cardDeck && !appState.cardDeck.isLoading) {
            cardDeckService.shuffleCardDeck()
                .then(data => {
                    dispatch({ type: 'RECEIVE_CARD_DECK', deck: data });
                });

            dispatch({ type: 'SHUFFLE_CARD_DECK' });
        }
    },
    dealCardDeck: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.cardDeck && !appState.cardDeck.isLoading) {
            cardDeckService.dealCardDeck()
                .then(data => {
                    dispatch({ type: 'RECEIVE_CARD_DECK', deck: data.deck, card: data.card });
                });

            dispatch({ type: 'DEAL_CARD_DECK' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CardDeckState = { isLoading: false };

export const reducer: Reducer<CardDeckState> = (state: CardDeckState | undefined, incomingAction: Action): CardDeckState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CARD_DECK':
            return _.assign({}, state, {
                isLoading: true
            });
        case 'RESET_CARD_DECK':
            return _.assign({}, state, {
                lastAction: DeckAction.Reset,
                isLoading: true
            });
        case 'SHUFFLE_CARD_DECK':
            return _.assign({}, state, {
                lastAction: DeckAction.Shuffle,
                isLoading: true
            });
        case 'DEAL_CARD_DECK':
            return _.assign({}, state, {
                lastAction: DeckAction.Deal,
                isLoading: true
            });
        case 'RECEIVE_CARD_DECK':
            return _.assign({}, state, {
                deck: action.deck,
                card: action.card,
                isLoading: false
            });
    }

    return state;
};
