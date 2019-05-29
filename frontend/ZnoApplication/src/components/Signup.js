import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Link as RouterLink } from "react-router-dom";
import { Prompt } from "react-router";

import {
    Avatar, Button, CssBaseline, Paper, Typography,
    // FormControl, Input, InputLabel,
    Link, CircularProgress,
    IconButton, Snackbar
} from '@material-ui/core';
import { ValidatorForm, TextValidator } from "react-material-ui-form-validator";

import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import CloseIcon from '@material-ui/icons/Close';

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
    buttonProgress: {
        position: 'absolute',
        top: '50%',
        left: '50%',
        marginTop: -12,
        marginLeft: -12,
    },
});

class SignUp extends Component {
    constructor(props) {
        super(props);
        this.state = {
            signupUser: {
                phone: '',
                password: '',
                confirmPassword: '',
                email: '',
            },
            loading: false,
            isErrorShowing: true,
        }

        this.shouldBlockNavigation = false;

        this.handleSubmit = this.handleSubmit.bind(this);

        // custom rule will have name 'isPasswordMatch'
        ValidatorForm.addValidationRule('isPasswordMatch', (value) => {
            return value === this.state.signupUser.password;
        });
    }

    handleChange = (event) => {
        const { signupUser } = this.state;
        signupUser[event.target.name] = event.target.value;
        this.setState({ signupUser: signupUser, loading: false });

        this.shouldBlockNavigation = signupUser.phone !== '' || signupUser.password !== '';
    }

    handleSubmit() {
        // your submit logic
        console.log("Submit");
        console.log(this.state.signupUser);

        this.setState({ signupUser: this.state.signupUser, loading: true });
        fetch('')
            .then(res => {

                this.setState({ signupUser: this.state.signupUser, loading: false });
            })
            .catch(err => {
                console.error(err);
                this.setState({ signupUser: this.state.signupUser, loading: false });

            })
    }

    handleCloseSnackbar= ()=>
        this.setState({
            signupUser: this.state.signupUser,
            loading: this.state.loading,
            isErrorShowing: false,
        });
    

    render() {

        const { classes } = this.props;
        const { signupUser } = this.state;

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
                            margin="normal"
                            required
                            fullWidth
                            label='Phone'
                            name='phone'
                            autoComplete='tel'
                            onChange={this.handleChange}
                            value={signupUser.phone}
                            validators={['required', 'matchRegexp:^\\+?(38)?(0\\d{9})$']}
                            errorMessages={['Phone is required', 'Phone must be like 0123456789 or +380123456789']}
                        />

                        {/* <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <Input name="password" type="password" id="password" autoComplete="new-password" />
                        </FormControl> */}
                        <TextValidator
                            margin="normal"
                            autoComplete="new-password"
                            required
                            fullWidth
                            label='Password'
                            name='password'
                            type='password'
                            onChange={this.handleChange}
                            value={signupUser.password}
                            validators={['required']}
                            errorMessages={['Password is required']}
                        />
                        {/* 
                        <FormControl margin="normal" required fullWidth>
                            <InputLabel htmlFor="confirmPassword">Confirm Password</InputLabel>
                            <Input name="confirmPassword" type="password" id="confirmPassword" autoComplete="new-password" />
                        </FormControl> */}

                        <TextValidator
                            margin="normal"
                            autoComplete="new-password"
                            required
                            fullWidth
                            label='Confirm Password'
                            name='confirmPassword'
                            type='password'
                            onChange={this.handleChange}
                            value={signupUser.confirmPassword}
                            validators={['isPasswordMatch', 'required']}
                            errorMessages={['Password mismatch', 'This field is required']}
                        />

                        {/* <FormControl margin="normal" fullWidth>
                            <InputLabel htmlFor="email">Email</InputLabel>
                            <Input name="email" type="email" id="email" />
                        </FormControl> */}

                        <TextValidator
                            margin="normal"
                            autoComplete="email"
                            fullWidth
                            label='Email'
                            name='email'
                            validators={['isEmail']}
                            errorMessages={['Email is not valid']}
                            value={signupUser.email}
                            onChange={this.handleChange}
                        />

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            disabled={this.state.loading}
                            className={classes.submit}
                        >Sign Up</Button>
                        {this.state.loading && <CircularProgress size={24} className={classes.buttonProgress} />}


                    </ValidatorForm>
                    {/* <LinearProgress size={24}/> */}
                    <Link className={classes.link} color='secondary' component={RouterLink} to={links.signin}>Sign in</Link>
                </Paper>


                <Snackbar
                    anchorOrigin={{
                        vertical: 'bottom',
                        horizontal: 'left',
                    }}
                    open={this.state.isErrorShowing}
                    autoHideDuration={6000}
                    onClose={this.handleCloseSnackbar}
                    ContentProps={{
                        'aria-describedby': 'message-id',
                    }}
                    message={<span id="message-id">Note archived</span>}
                    action={[
                        <IconButton
                            key="close"
                            aria-label="Close"
                            color="inherit"
                            // className={classes.close}
                            onClick={this.handleCloseSnackbar}
                        >
                            <CloseIcon />
                        </IconButton>,
                    ]}
                />

            </main >
        );
    }
}

SignUp.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(SignUp);