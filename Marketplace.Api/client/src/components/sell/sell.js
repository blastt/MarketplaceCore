// import React from 'react';
// import ReactDOM from 'react-dom';

import React, { Component } from 'react';

import './sell.css';

class Sell extends React.Component {
	constructor(props) {
		super(props);
		this.state = { name: '', price: 0 };

		this.onSubmit = this.onSubmit.bind(this);
		this.onNameChange = this.onNameChange.bind(this);
		this.onPriceChange = this.onPriceChange.bind(this);
	}
	onNameChange(e) {
		this.setState({ name: e.target.value });
	}
	onPriceChange(e) {
		this.setState({ price: e.target.value });
	}
	onSubmit(e) {
		e.preventDefault();
		let phoneName = this.state.name.trim();
		let phonePrice = this.state.price;
		if (!phoneName || phonePrice <= 0) {
			return;
		}

		let data = JSON.stringify({ name: phoneName, price: phonePrice });
		let xhr = new XMLHttpRequest();

		xhr.open('post', this.props.apiUrl, true);
		xhr.setRequestHeader('Content-type', 'application/json');
		xhr.onload = function() {
			if (xhr.status === 200) {
				this.loadData();
			}
		}.bind(this);
		xhr.send(data);
		console.log(data);
		this.setState({ name: '', price: 0 });
	}

	render() {
		return (
			<div className='offer-create'>
				<h2>Create offer</h2>
				<form className='offer-create-form' onSubmit={this.onSubmit}>
					<p className='form-item'>
						<label htmlFor='header'>Name</label>
						{this.props.apiUrl}
						<br />
						{this.state.name}
						<input
							type='text'
							value={this.state.name}
							onChange={this.onNameChange}
							placeholder='Имя'
						/>
					</p>
					<p className='form-item'>
						<label htmlFor='header'>Price</label>
						<input
							type='text'
							value={this.state.price}
							onChange={this.onPriceChange}
							placeholder='Цена'
						/>
					</p>
					<button className='button' type='submit'>
						Go
					</button>
				</form>
			</div>
		);
	}
}

export default Sell;
