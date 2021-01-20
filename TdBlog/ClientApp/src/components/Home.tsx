import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Nice to meet you, %s!</h1>
  </div>
);

export default connect()(Home);
