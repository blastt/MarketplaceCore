import React from 'react';
import ReactDOM from 'react-dom';

import './sell.css';

const Sell = ({ apiUrl }) => {
	return (
		<div className='offer-create'>
			<h2>Create offer</h2>
			<form className='offer-create-form' action='Create' method='post'>
				<p className='form-item'>
					<label htmlFor='header'>Header</label>
					{apiUrl}
					<input type='text' placeholder='губошлёп' />
				</p>
			</form>
		</div>
	);
};

export default Sell;
