import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CardDeckStore from '../store/CardDeck';
import { Card, CardHeader, CardBody, CardText, Container, Row, Col } from 'reactstrap';

type DeckSummaryProps = CardDeckStore.CardDeckState;

class DeckSummary extends React.PureComponent<DeckSummaryProps> {
    public render() {
        return (
            <Card>
                <CardHeader>Deck Summary</CardHeader>
                <CardBody>
                    <CardText tag="div">{this.renderCardDetails()}</CardText>
                </CardBody>
            </Card>
        );
    }

    private renderCardDetails() {
        if (this.props.deck) {
            return (
                <Container>
                    <Row>
                        <Col>Deck Id</Col>
                        <Col>{this.props.deck.id}</Col>
                    </Row>
                    <Row>
                        <Col>Cards left</Col>
                        <Col>{this.props.deck.cards.length}</Col>
                    </Row>
                    <Row>
                        <Col>Shuffled?</Col>
                        <Col>{this.props.deck.shuffled? "Yes" : "No"}</Col>
                    </Row>
                </Container>
            );
        }
        return null;
    }
}

export default connect(
    (state: ApplicationState) => state.cardDeck // Selects which state properties are merged into the component's props
)(DeckSummary as any);
