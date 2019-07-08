import React, { Component } from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';

import { Link as RouterLink, withRouter } from 'react-router-dom';

// Material Components

import { withStyles } from '@material-ui/core/styles';
import {
    AppBar, Toolbar, Typography, Button,
    IconButton, Drawer, Divider,
    List, ListItem, ListItemIcon, ListItemText, Link
} from '@material-ui/core';


// Material Icons
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import InfoIcon from "@material-ui/icons/InfoOutlined";
import HomeIcon from "@material-ui/icons/HomeOutlined";

// Other resources
import { links } from "../links";
import { isAuthenticated } from '../auth';



const drawerWidth = 250;
const styles = theme => ({
    root: {
        flexGrow: 1,
        position:'static'
    },
    grow: {
        flexGrow: 1,
    },
    menuButton: {
        marginLeft: -12,
        marginRight: 20,
    },
    // button: {
    //     // background: 'linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)',
    //     // borderRadius: 3,
    //     border: 0,
    //     // color: 'white',
    //     width: '100%',
    //     margin: 0,
    //     display: 'flex',
    //     padding: '5px 30px',
    //     // boxShadow: '0 3px 5px 2px rgba(255, 105, 135, .3)',
    // },
    label: {
        textTransform: 'capitalize',
    },
    hide: {
        display: 'none',
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
    },
    drawerPaper: {
        width: drawerWidth,
    },
    drawerHeader: {
        display: 'flex',
        alignItems: 'center',
        padding: '0 8px',
        // ...theme.mixins.toolbar,
        justifyContent: 'flex-end',
    },
    link: {
        textDecoration: 'none',
        fontWeight: 'bold'
    },
    currentLink: {
        border: '1px solid gray'
    },
    icon: {
        // backgroundColor: theme.palette.secondary.main,
        // color: '#fafafa',
        // padding: '5px',
        // borderRadius: '50%'
    },
});

class NavBar extends Component {

    constructor(props) {
        super(props);
        this.state = { open: false };

        this.handleDrawerOpen = this.handleDrawerOpen.bind(this);
        this.handleDrawerClose = this.handleDrawerClose.bind(this);
    }

    handleDrawerOpen() {
        this.setState({ open: true });
    };

    handleDrawerClose() {
        this.setState({ open: false });
    };

    render() {

        const { pathname } = this.props.location;
        // console.log({pathname});


        const { classes, theme } = this.props;
        const { open } = this.state;

        const pages = [{
            text: 'Home',
            link: links.home,
            iconComponent: <HomeIcon className={classes.icon} />
        }, {
            text: 'About',
            link: links.about,
            iconComponent: <InfoIcon className={classes.icon} />
        }];

        const authOrExit = {
            link: isAuthenticated() ? links.logout : links.signin,
            displayTitle: isAuthenticated() ? "Exit" : "Sign in / Sing up",
        };


        return (
            <div className={classes.root}>
                <AppBar position="static">
                    <Toolbar >
                        <IconButton
                            onClick={this.handleDrawerOpen}
                            className={classNames(classes.menuButton, open && classes.hide)} color="inherit" aria-label="Menu">
                            <MenuIcon />
                        </IconButton>

                        <Typography variant="h6" className={classes.grow} color="inherit" >
                            <Link to={links.default} component={RouterLink} underline='none' color="inherit" >
                                ZNO
                            </Link>
                        </Typography>
                        <Button color="inherit">
                            <Link to={authOrExit.link} component={RouterLink} underline='none' color="inherit" >
                            {authOrExit.displayTitle}
                            </Link>
                        </Button>

                    </Toolbar>
                </AppBar>

                <Drawer
                    className={classes.drawer}
                    variant="persistent"
                    anchor="left"
                    open={open}
                    classes={{
                        paper: classes.drawerPaper,
                    }}
                >
                    <div className={classes.drawerHeader}>
                        <Typography variant="h6" color="inherit" className={classes.grow}>
                            ZNO
                        </Typography>
                        <IconButton onClick={this.handleDrawerClose}>
                            {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                        </IconButton>
                    </div>
                    <Divider />
                    <List>

                        {pages.map((page, index) => (
                            <Link underline='none' className={classes.link} key={page.text} component={RouterLink} to={page.link}>
                                <ListItem button selected={page.link === pathname || (pathname === links.default && page.link === links.home)} >
                                    <ListItemIcon>{page.iconComponent}</ListItemIcon>
                                    <ListItemText primary={page.text} />

                                </ListItem>
                            </Link>
                        ))}
                    </List>
                    {/* <Divider />
                    <List>
                        {['All mail', 'Trash', 'Spam'].map((text, index) => (
                            <ListItem button key={text}>
                                <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
                                <ListItemText primary={text} />
                            </ListItem>
                        ))}
                    </List> */}
                </Drawer>
            </div>
        );
    }
}

NavBar.propTypes = {
    classes: PropTypes.object.isRequired,
    theme: PropTypes.object.isRequired,
};

export default withRouter(withStyles(styles, { withTheme: true })(NavBar));
