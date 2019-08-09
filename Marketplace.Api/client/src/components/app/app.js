import React from 'react';
import ReactDOM from 'react-dom';

import Sell from '../sell';

import './app.css';

const App = () => {
	return (
		<div>
			{/* <Sell apiUrl='/api/offer' /> */}
			<Sell apiUrl='https://localhost:44348/api/offer/create' />
		</div>
	);
};

ReactDOM.render(<App />, document.getElementById('root'));

export default App;
