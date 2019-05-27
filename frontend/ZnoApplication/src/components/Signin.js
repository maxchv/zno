import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Link as RouterLink } from "react-router-dom";
import { Prompt } from "react-router";

import {
    Avatar, Button, CssBaseline, Paper, Typography,
    // FormControl, Input, InputLabel, 
    FormControlLabel, Checkbox,
    Link,
} from '@material-ui/core';

import LockOutlinedIcon from '@material-ui/icons/LockOutlined';

import withStyles from '@material-ui/core/styles/withStyles';

import { links } from '../links';
import { ValidatorForm, TextValidator } from 'react-material-ui-form-validator';

const styles = theme => ({
    main: {
        width: 'auto',
        display: 'block', // Fix IE 11 issue.
        marginLeft: theme.spacing.unit * 3,
        marginRight: theme.spacing.unit * 3,
        [theme.breakpoints.up(400 + theme.spacing.unit * 3 * 2)]: {
            width: 400,
            marginLeft: 'auto',
            marginRight: 'auto',
        },
    },
    paper: {
        marginTop: theme.spacing.unit * 8,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: `${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px ${theme.spacing.unit * 3}px`,
    },
    avatar: {
        margin: theme.spacing.unit,
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing.unit,
    },
    submit: {
        marginTop: theme.spacing.unit * 3,
    },
    link: {
        cursor: 'pointer',
    },
});



class SignIn extends Component {
    constructor(props) {
        super(props);

        this.state = {
            signinUser: {
                login: '',
                password: '',
                remember: true,
            }
        }
        this.shouldBlockNavigation = false;

        ValidatorForm.addValidationRule('isPhoneOrEmail', (value) => {
            const emailPattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            const phonePattern = /^\+?(38)?(0\d{9})$/;
            const login = String(value).toLowerCase();
            return (emailPattern.test(login) || phonePattern.test(login));
        });

    }

    handleSubmit = () => {
        console.log("Submit");
        console.log(this.state);
    }

    handleChange = (event) => {
        // console.dir(event.target);
        const { signinUser } = this.state;
        signinUser[event.target.name] = event.target.type === "checkbox" ? event.target.checked : event.target.value.trim();
        this.setState({ signinUser });
        this.shouldBlockNavigation = signinUser.login !== '' || signinUser.password !== '';
        console.log(this.shouldBlockNavigation);
        // console.log(this.state);
    }

    render() {
        const { classes } = this.props;
        const { signinUser } = this.state;
        return (
            <main className={classes.main}>
                <CssBaseline />
                <Prompt
                    when={this.shouldBlockNavigation}
                    message='You have unsaved changes, are you sure you want to leave?' />
                <Paper className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
        </Typography>
                    <ValidatorForm onSubmit={this.handleSubmit} className={classes.form}>
                        {/* <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="login">Email/Phone</InputLabel>
                            <Input id="login" name="login" autoComplete="email" autoFocus />
                        </FormControl> */}
                        <TextValidator
                            margin="normal"
                            required
                            fullWidth
                            label='Email/Phone'
                            name='login'
                            autoComplete='tel email'
                            onChange={this.handleChange}
                            value={signinUser.login}
                            validators={['required', 'isPhoneOrEmail']}
                            errorMessages={['Field is required', 'This should be Email or Phone']}
                        />
                        {/* <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <Input name="password" type="password" id="password" autoComplete="current-password" />
                        </FormControl> */}
                        <TextValidator
                            margin="normal"
                            required
                            fullWidth
                            label='Password'
                            name='password'
                            type='password'
                            autoComplete='current-password'
                            onChange={this.handleChange}
                            value={signinUser.password}
                            validators={['required']}
                            errorMessages={['Password is required']}
                        />

                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={signinUser.remember}
                                    name='remember'
                                    onChange={this.handleChange}
                                    color="primary" />
                            }
                            label="Remember me"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                        >Sign in</Button>

                    </ValidatorForm>
                    <Link className={classes.link} color='secondary' component={RouterLink} to={links.signup}>Sign up</Link>

                </Paper>
            </main>
        );
    }
}

SignIn.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(SignIn);