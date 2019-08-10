import React, { Component } from 'react';

import './sell.css';

class Sell extends Component {
	state = {
		// game: '',
		// header: '',
		// description: '',
		// accountLogin: '',
		// personalAccount: false,
		// createdAccountDate: '',
		// isBanned: false,
		// url: '',
		// price: 0,
		// secureTransactionPayer: '',
		// header: '',
		// description: '',
		// login: '',
		// price: 0,
		offers: [],
	};

	// componentWillMount = () => {
	// 	const xhr = new XMLHttpRequest();
	// 	xhr.open('get', this.props.apiUrl, true);
	// 	xhr.onload = () => {
	// 		const data = JSON.parse(xhr.responseText);
	// 		// console.log(data);

	// 		this.setState({ offers: data });
	// 	};
	// 	xhr.send();
	// 	console.log(this.state.offers);
	// };
	// componentDidMount = () => {
	// 	this.componentWillMount();
	// 	console.log(this.state.offers);
	// };

	componentDidMount = () => {
		fetch(this.props.apiUrl)
			.then((response) => response.json())
			.then((data) => {
				// const get = JSON.parse(data);
				this.setState({ offers: data });
				// console.log(data);
				console.log(this.state.offers);
				console.log(Object.keys(this.state.offers));
			});

		// console.log(Object.keys(this.state.offers));
	};

	render() {
		return 'jopa';
	}
	// onHeaderChange = (e) => {
	// 	this.setState({ header: e.target.value });
	// };
	// onDescriptionChange = (e) => {
	// 	this.setState({ description: e.target.value });
	// };
	// onLoginChange = (e) => {
	// 	this.setState({ login: e.target.value });
	// };
	// onPriceChange = (e) => {
	// 	this.setState({ price: e.target.value });
	// };

	// onSubmit = (e) => {
	// 	e.preventDefault();
	// 	let phoneHeader = this.state.header.trim();
	// 	let phoneDescription = this.state.description.trim();
	// 	let phoneLogin = this.state.login.trim();
	// 	let phonePrice = this.state.price;
	// 	if (!phoneHeader || !phoneDescription || !phoneLogin || phonePrice <= 0) {
	// 		return;
	// 	}

	// 	let data = JSON.stringify({
	// 		header: phoneHeader,
	// 		description: phoneDescription,
	// 		login: phoneLogin,
	// 		price: phonePrice,
	// 	});
	// 	let xhr = new XMLHttpRequest();

	// 	xhr.open('post', this.props.apiUrl, true);
	// 	xhr.setRequestHeader('Content-type', 'application/json');
	// 	xhr.onload = function() {
	// 		if (xhr.status === 200) {
	// 			this.loadData();
	// 		}
	// 	};
	// 	xhr.send(data);
	// 	console.log(data);
	// 	this.setState({ header: '', description: '', login: '', price: 0 });
	// };

	// render() {
	// 	return (
	// 		<div className='sell'>
	// 			<h2>Create offer</h2>
	// 			<form className='offer-create-form' onSubmit={this.onSubmit}>
	// 				<p className='form-item'>
	// 					<label htmlFor='game'>Game</label>
	// 					{/* {this.props.apiUrl}
	// 					<br />
	// 					{this.state.header} */}
	// 					<input
	// 						id='game'
	// 						type='text'
	// 						value={this.state.game}
	// 						onChange={this.onGameChange}
	// 						// placeholder='Имя'
	// 					/>
	// 				</p>
	// 				<p className='form-item'>
	// 					<label htmlFor='header'>Header</label>
	// 					{/* {this.props.apiUrl}
	// 					<br />
	// 					{this.state.header} */}
	// 					<input
	// 						id='header'
	// 						type='text'
	// 						value={this.state.header}
	// 						onChange={this.onHeaderChange}
	// 						// placeholder='Имя'
	// 					/>
	// 				</p>
	// 				<p className='form-item'>
	// 					<label htmlFor='description'>Description</label>
	// 					{/* {this.props.apiUrl}
	// 					<br />
	// 					{this.state.header} */}
	// 					<input
	// 						type='text'
	// 						value={this.state.description}
	// 						onChange={this.onDescriptionChange}
	// 						// placeholder='Имя'
	// 					/>
	// 				</p>
	// 				<p className='form-item'>
	// 					<label htmlFor='login'>Account login</label>
	// 					{/* {this.props.apiUrl}
	// 					<br />
	// 					{this.state.header} */}
	// 					<input
	// 						type='text'
	// 						value={this.state.login}
	// 						onChange={this.onLoginChange}
	// 						// placeholder='Имя'
	// 					/>
	// 				</p>
	// 				<p>
	// 					{/* <label htmlFor='login'>Account login</label> */}
	// 					{/* {this.props.apiUrl}
	// 					<br />
	// 					{this.state.header} */}
	// 					<input
	// 						type='checkbox'
	// 						// value={this.state.login}
	// 						// onChange={this.onLoginChange}
	// 						// placeholder='Имя'
	// 					/>
	// 					<span>Personal Account?</span>
	// 				</p>
	// 				<p className='form-item'>
	// 					<label htmlFor='price'>Price</label>
	// 					<input
	// 						type='text'
	// 						value={this.state.price}
	// 						onChange={this.onPriceChange}
	// 						// placeholder='Цена'
	// 					/>
	// 				</p>
	// 				<button className='button' type='submit'>
	// 					Go
	// 				</button>
	// 			</form>
	// 		</div>
	// 	);
	// }
}

export default Sell;
