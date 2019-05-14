import React, { Component } from 'react';
import { AppBar, Toolbar, Typography, Button } from '@material-ui/core';
// import { AccountCircle } from "@material-ui/icons";
import './../css/navbar.css';

class NavBar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: {
                // role: "user"
            }
        }

    }

    render() {
        let additionalMenuItems = null;
        if (this.state && this.state.user) {
            switch (this.state.user.role) {
                case "admin":
                    additionalMenuItems = <div>
                        <Button color="inherit">
                            Преподаватели
                        </Button>
                        <Button color="inherit">Тесты</Button>
                    </div>;
                    break;
                case "teacher":
                    additionalMenuItems = <div>
                        <Button color="inherit">Пройденные Тесты</Button>
                    </div>;
                    break;
                default:
                    additionalMenuItems = <div>
                        <Button color="inherit">Тесты</Button>
                    </div>;
                    break;

            }
        }
        else {
            additionalMenuItems =
                <Button color="inherit">Login</Button>;

        }

        return (
            <div>
                <AppBar position='static'>
                    <Toolbar>
                        <Typography variant='title' color='inherit' className="title">
                            Zno Tests
                        </Typography>
                        {additionalMenuItems}
                    </Toolbar>
                </AppBar>
            </div>
        );
    }

}

export default NavBar;