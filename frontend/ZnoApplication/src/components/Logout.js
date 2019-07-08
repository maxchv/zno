import React, { Component } from "react";
import { Redirect } from 'react-router-dom';
import { logout } from "../auth";
import { links } from "../links";

class Logout extends Component {

    render() {
        logout();
        return (<Redirect to={links.signin} />);
    }
}

export default Logout;