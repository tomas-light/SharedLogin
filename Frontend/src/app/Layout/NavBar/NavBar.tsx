import * as React from "react";

import { makeStyles, Theme } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Toolbar from "@material-ui/core/Toolbar";
import MenuIcon from "@material-ui/icons/Menu";
import { ExpandMore } from "@material-ui/icons";

import { Callback } from "@utils/types/Callback";
import { AccountDTO } from "@models/accounts/AccountDTO";
import { AccountItem } from "@app/Layout/NavBar/AccountItem/AccountItem";
import {NavBarMenuContainer} from "@app/Layout/NavBar/NavBarMenu/NavBarMenuContainer";

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

export interface INavBarProps {
    authenticatedAccount: AccountDTO;
    activeAccount: AccountDTO;
}

export interface INavBarCallProps {
    logout: Callback;
}

type Props = INavBarProps & INavBarCallProps;

const NavBar: React.FunctionComponent<Props> = props => {
    const { authenticatedAccount, activeAccount, logout } = props;

    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    function handleMenu(event: React.MouseEvent<HTMLElement>) {
        setAnchorEl(event.currentTarget.parentElement);
    }

    function handleClose() {
        setAnchorEl(null);
    }

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

                    <AccountItem
                        account={authenticatedAccount}
                        icon={
                            <IconButton onClick={handleMenu}>
                                <ExpandMore />
                            </IconButton>
                        }
                    />

                    <NavBarMenuContainer
                        anchorEl={anchorEl}
                        isOpen={open}
                        onClose={handleClose}
                    />

                    <Button color="inherit" onClick={logout}>
                        Logout
                    </Button>
                </Toolbar>
            </AppBar>
        </div>
    );
};

export { NavBar };
