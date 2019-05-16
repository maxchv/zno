import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Link as RouterLink } from "react-router-dom";

import {
    Avatar, Button, CssBaseline, Paper, Typography,
    FormControl, Input, InputLabel,
    Link,
} from '@material-ui/core';
import { ValidatorForm, TextValidator } from "react-material-ui-form-validator";

import LockOutlinedIcon from '@material-ui/icons/LockOutlined';

import withStyles from '@material-ui/core/styles/withStyles';

import { links } from '../links';

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
        marginTop: -(theme.spacing.unit * 2),
    },
    submit: {
        marginTop: theme.spacing.unit * 3,
    },
    link: {
        cursor: 'pointer',
    },
});

class SignUp extends Component {
    constructor(props) {
        super(props);
        this.state = {
            phone: '',
            password: '',
            confirmPassword: '',
            email: '',
        }


        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit() {
        // your submit logic
        // console.log(this);

    }

    render() {

        const { classes } = this.props;
        const { phone, password, confirmPassword, email } = this.state;

        return (
            <main className={classes.main}>
                <CssBaseline />
                <Paper className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign up
        </Typography>
                    <ValidatorForm onSubmit={this.handleSubmit} className={classes.form}>
                        {/* 
                        <FormControl margin="normal" required fullWidth>
                             <InputLabel htmlFor="phone">Phone</InputLabel>
                            <Input id="phone" name="phone" autoComplete="phone" autoFocus /> 

                        </FormControl> 
                        */}
                        <TextValidator
                            required
                            fullWidth
                            label='Phone'
                            name='phone'
                            value={phone}
                            validators={['required']}
                        />

                        <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <Input name="password" type="password" id="password" autoComplete="current-password" />
                        </FormControl>

                        <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="confirmPassword">Confirm Password</InputLabel>
                            <Input name="confirmPassword" type="password" id="confirmPassword" autoComplete="current-password" />
                        </FormControl>

                        <FormControl margin="normal" fullWidth>
                            <InputLabel htmlFor="email">Email</InputLabel>
                            <Input name="email" type="email" id="email" autoComplete="current-password" />
                        </FormControl>

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                        >
                            Sign Up
          </Button>


                    </ValidatorForm>
                    <Link className={classes.link} color='secondary' component={RouterLink} to={links.signin}>Sign in</Link>
                </Paper>
            </main >
        );
    }
}

SignUp.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(SignUp);