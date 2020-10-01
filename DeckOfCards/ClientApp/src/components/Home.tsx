import * as React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

const Home = () => (
    <div>
        <h1>Hello, Optix!</h1>
        <p>This is my submission for the Deck of Cards technical test. I'd like to take a minute to explain how some of it is set up.</p>
        <ul>
            <li><strong>Persistence</strong>. Decks are persisted using an InMemoryDataContext, essentially using static lists. This is not production safe, but allows for easy running and persistence for the lifetime of a thread</li>
            <li><strong>Logic</strong>. Entities are setup to encapsulate their own logic, exposing methods to interact with them directly from the entity. This allows for strong separation of concerns, and especially given the small scale of the domain will not cause issues with logic living along side "transportation" models</li>
            <li><strong>Testing</strong>. All C# (where appropriate) has been tested. Front end typescript/react code has not been. This layer only contains basic display logic so poses minimal risk. In a production system however, not having front end tests is not an option</li>
        </ul>
        <p>A summary of the features implemented</p>
        <ul>
            <li><strong>Per-user decks</strong>. Multiple users (browser sessions) can have their own deck to work with. Try it with an incognito window</li>
            <li><strong>Easy to use actions</strong>. Buttons are only enabled when you are able to perform the specified action. The info alert at the top provides more information</li>
            <li><strong>Multiple build options</strong>. To allow flexibility of usage of the solution, as well as Visual Studio, you can use the dockerfile (<code>docker build . -t deckofcards -f Build\Dockerfile</code>) or the cake script (<code>.\Build\build.ps1 -Script Build\build.cake -Target BuildAndTests -Configuration Debug</code>) to build it (both commands from the root directory)</li>
        </ul>
        Go to the <Link to="/card-deck">Card Deck</Link> to get started!
    </div>
);

export default connect()(Home);
