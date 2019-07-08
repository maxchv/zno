import React from "react";
import PropTypes from 'prop-types';

import CloseIcon from '@material-ui/icons/Close';
import { IconButton, Snackbar } from '@material-ui/core';

class CustomSnackbar extends React.Component {
    constructor(props) {
        super(props);
        // console.log({props});

        this.state = {
            isSnackbarVisible: !!props.isSnackbarVisible,
            snackbarMessage: props.snackbarMessage || '',
        }

    }

    changeState = (props) => {
        const state = this.state;
        for (const key in props) {
            state[key] = props[key];
        }
        this.setState(state);
    }

    handleCloseSnackbar = () => this.changeState({ isSnackbarVisible: false });

    render() {
        return (<Snackbar
            anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
            }}
            open={this.state.isSnackbarVisible}
            autoHideDuration={6000}
            onClose={this.handleCloseSnackbar}
            ContentProps={{
                'aria-describedby': 'message-id',
            }}
            message={<span id="message-id">{this.state.snackbarMessage}</span>}
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
        />);
    }
}


CustomSnackbar.propTypes = {
    isSnackbarVisible: PropTypes.bool,
    snakbarText: PropTypes.string,
};


export default CustomSnackbar;