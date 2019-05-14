import React from 'react';
import {BrowserRouter, Route, Link } from 'react-router-dom';
// import logo from './logo.svg';
import './App.css';
import NavBar from "./components/NavBar";
import Home from './components/Home';
import About from './components/About';

function App() {
	return (
		<BrowserRouter>
			<NavBar/>
			<Route path='/' component={Home} />
			<Route path='/about' component={About} />
		</BrowserRouter>
	);
}

export default App;
