import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CardDeckStore from '../store/CardDeck';
import { Card, CardHeader, CardBody, CardText } from 'reactstrap';

type DealtCardDetailsProps = CardDeckStore.CardDeckState;

class DealtCardDetails extends React.PureComponent<DealtCardDetailsProps> {
    public render() {
        return (
            <Card>
                <CardHeader>Last Dealt Card</CardHeader>
                <CardBody>
                    <CardText tag="div">{this.renderCardDetails()}</CardText>
                </CardBody>
            </Card>
        );
    }

    private renderCardDetails() {
        if (this.props.card) {
            return (
                <>
                    {this.props.card.value} of {this.props.card.suit}
                </>
            );
        }
        return (
            <>
                No card has been dealt.
            </>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.cardDeck // Selects which state properties are merged into the component's props
)(DealtCardDetails as any);
