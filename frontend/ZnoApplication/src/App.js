import React from 'react';
import { Route, Switch } from 'react-router-dom';
import './auth';

// Components
import NavBar from "./components/NavBar";
import Home from './components/Home';
import About from './components/About';
import Signup from './components/Signup';
import Signin from './components/Signin';

// Resources
// import logo from './logo.svg';
import './App.css';
import { isAuthenticated } from './auth';
import { links } from "./links";



function App() {
	let currentUser =
	{
		login: 'user',
		password: 'pass',
		roles: ['user']
	};
	currentUser = null;
	return (
		<div>
			<NavBar />
			<Switch>
				<Route exact path={links.default} component={Home} />
				<Route path={links.home} component={Home} />
				<Route path={links.about} component={About} />
				{!isAuthenticated(currentUser) && <Route path={links.signin} component={Signin} />}
				{!isAuthenticated(currentUser) && <Route path={links.signup} component={Signup} />}
				<Route component={Home} />
			</Switch>
		</div>
	);
}

export default App;
