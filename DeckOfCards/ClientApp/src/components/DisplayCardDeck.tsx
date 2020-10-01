import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CardDeckStore from '../store/CardDeck';
import DeckActions from './DeckActions';
import { Alert, CardDeck } from 'reactstrap';
import DealtCardDetails from './DealtCardDetails';
import DeckSummary from './DeckSummary';

// At runtime, Redux will merge together...
type DisplayCardDeckProps =
    CardDeckStore.CardDeckState // ... state we've requested from the Redux store
    & typeof CardDeckStore.actionCreators; // ... plus action creators we've requested


class DisplayCardDeck extends React.PureComponent<DisplayCardDeckProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1>Card Deck</h1>
                <Alert color="info">
                    You start with a full complement of 52 cards, ordered by suit then by value.<br />
                    Before dealing a card you may shuffle the deck, but once you have dealt one, the deck may no longer be shuffled.<br />
                    You are free to start over at any point.
                </Alert>
                <DeckActions />
                <div>
                    <CardDeck>
                        <DeckSummary />
                        <DealtCardDetails />
                    </CardDeck>
                </div>
                <h3 id="tableLabel">Cards in Deck</h3>
                {this.renderDeck()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestCardDeck();
    }

    private renderDeck() {
        if (this.props.deck) {
            return (
                <table className='table table-striped' aria-labelledby="tableLabel">
                    <thead>
                        <tr>
                            <th>Suit</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.deck.cards.map((card) =>
                            <tr key={`${card.suit}${card.value}`}>
                                <td>{card.suit}</td>
                                <td>{card.value}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        }
        return null;
    }
}

export default connect(
    (state: ApplicationState) => state.cardDeck, // Selects which state properties are merged into the component's props
    CardDeckStore.actionCreators // Selects which action creators are merged into the component's props
)(DisplayCardDeck as any);
