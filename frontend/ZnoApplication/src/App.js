import React from 'react';
import { Route } from 'react-router-dom';
// import logo from './logo.svg';
import './App.css';
import NavBar from "./components/NavBar";
import Home from './components/Home';
import About from './components/About';

function App() {
	return (
		<div>
			<NavBar />
			<Route exact path='/' component={Home} />
			<Route path='/Home/' component={Home} />
			<Route path='/About/' component={About} />
		</div>
	);
}

export default App;
