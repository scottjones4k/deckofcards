import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import DisplayCardDeck from './components/DisplayCardDeck';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/card-deck' component={DisplayCardDeck} />
    </Layout>
);
