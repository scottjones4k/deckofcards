import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CardDeckStore from '../store/CardDeck';
import { Container, Row, Col, Button } from 'reactstrap';

// At runtime, Redux will merge together...
type DeckActionsProps =
    CardDeckStore.CardDeckState // ... state we've requested from the Redux store
    & typeof CardDeckStore.actionCreators; // ... plus action creators we've requested


class CardDeck extends React.PureComponent<DeckActionsProps> {
    public render() {
        if (this.props.deck) {
            return (
                <React.Fragment>
                    <Container>
                        <Row className="text-center">
                            <Col><Button onClick={() => this.props.shuffleCardDeck()} disabled={this.props.deck.cards.length < 52} color="primary">Shuffle Deck</Button></Col>
                            <Col><Button onClick={() => this.props.dealCardDeck()} disabled={this.props.deck.cards.length === 0} color="primary">Deal Card</Button></Col>
                            <Col><Button onClick={() => this.props.resetCardDeck()} color="danger">Start Over</Button></Col>
                        </Row>
                    </Container>
                </React.Fragment>
            );
        }
        return null;
    }
}

export default connect(
    (state: ApplicationState) => state.cardDeck, // Selects which state properties are merged into the component's props
    CardDeckStore.actionCreators // Selects which action creators are merged into the component's props
)(CardDeck as any);
