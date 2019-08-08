import React from 'react';
import ReactDOM from 'react-dom';

import Sell from '../sell';

import './app.css';

const App = () => {
	return (
		<div>
			<Sell apiUrl='/api/offer' />
		</div>
	);
};

ReactDOM.render(<App />, document.getElementById('root'));

export default App;
