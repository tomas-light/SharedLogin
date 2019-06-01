import * as React from "react";

import makeStyles from "@material-ui/core/styles/makeStyles";
import { Theme } from "@material-ui/core";
import AppBar from "@material-ui/core/AppBar";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Toolbar from "@material-ui/core/Toolbar";
import MenuIcon from "@material-ui/icons/Menu";

import { Callback } from "@utils/types/Callback";

const useStyles = makeStyles((theme: Theme) => ({
    root: {
        flexGrow: 1
    },
    menuButton: {
        marginRight: theme.spacing(2)
    },
    title: {
        flexGrow: 1
    }
}));

export interface INavBarProps {}

export interface INavBarCallProps {
    redirectToLogin: Callback;
    redirectToNewBug: Callback;
    redirectToBugList: Callback;
    redirectToUserList: Callback;
    redirectToNewUser: Callback;
    logout: Callback;
}

type Props = INavBarProps & INavBarCallProps;

const NavBar: React.FunctionComponent<Props> = props => {
    const { logout } = props;

    const classes = useStyles();

    return (
        <div className={classes.root}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        edge="start"
                        className={classes.menuButton}
                        color="inherit"
                        aria-label="Menu"
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        Shared login
                    </Typography>
                    <Button color="inherit" onClick={logout}>Login</Button>
                </Toolbar>
            </AppBar>
        </div>
    );
};

export { NavBar };
