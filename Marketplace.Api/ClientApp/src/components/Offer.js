import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class Offer extends Component {

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };

        fetch('api/Offer/Get')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    static renderForecastsTable(forecasts) {
        return (  
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.id}</td>
                            <td>{forecast.header}</td>
                            <td>{forecast.discription}</td>
                            <td>{forecast.accountLogin}</td>
                            <td>{forecast.price}</td>                            
                        </tr>
                    )}
                </tbody>
            </table>
            
            
        );
    }

    render() {


        return (
            <div>
                <h1>Weather forecast</h1>
                <p>This component demonstrates slavik loh</p>
                {Offer.renderForecastsTable(this.state.forecasts)}
            </div>
        );
    }
}