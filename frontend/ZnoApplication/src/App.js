import React from 'react';
import { Route, Switch, Redirect } from 'react-router-dom';
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
import Logout from './components/Logout';



function App() {
	// let currentUser =
	// 	{
	// 		login: 'user',
	// 		password: 'pass',
	// 		roles: ['user']
	// 	};
	// currentUser = null;
	// component={isAuthenticated() ? Home : Signin} 

	return (
		<div>
			<NavBar />
			<Switch>
				{/* <Route exact path={links.default} onRender={isAuthenticated() ? (<Home />) : (<Redirect to={links.signin} />)} /> */}
				<Route exact path={links.default} render={() => isAuthenticated() ? (<Home />) : (<Redirect to={links.signin} />)} />
				{/* <Route path={links.home} component={isAuthenticated() ? Home : Signin} /> */}
				<Route exact path={links.home} render={() => isAuthenticated() ? (<Home />) : (<Redirect to={links.signin} />)} />
				<Route path={links.about} component={About} />
				<Route path={links.logout} component={Logout} />
				{!isAuthenticated() && <Route path={links.signin} component={Signin} />}
				{!isAuthenticated() && <Route path={links.signup} component={Signup} />}

				<Route component={Home} />
			</Switch>
		</div>
	);
}

export default App;
