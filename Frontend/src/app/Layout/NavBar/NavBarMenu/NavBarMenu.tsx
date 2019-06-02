import * as React from "react";

import { makeStyles, Theme } from "@material-ui/core/styles";
import IconButton from "@material-ui/core/IconButton";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import LockIcon from "@material-ui/icons/LockOpenOutlined";

import { AccountDTO } from "@models/accounts/AccountDTO";
import { AccountItem } from "@app/Layout/NavBar/AccountItem/AccountItem";

const useStyles = makeStyles((theme: Theme) => ({
    root: {},
    menuItem: {

    }
}));

export interface INavBarMenuProps {
    activeAccount: AccountDTO;
    accessibleAccounts: AccountDTO[];
}

export interface INavBarMenuOwnProps {
    anchorEl: HTMLElement | null;
    isOpen: boolean;
    onClose: () => void;
}

export interface INavBarMenuCallProps {
    onAccountClick: (accountId: string) => void;
}

type Props = INavBarMenuProps & INavBarMenuOwnProps & INavBarMenuCallProps;

const NavBarMenu: React.FunctionComponent<Props> = props => {
    const {
        anchorEl,
        isOpen,
        activeAccount,
        accessibleAccounts,
        onClose,
        onAccountClick
    } = props;

    const classes = useStyles();

    return (
        <Menu
            anchorEl={anchorEl}
            open={isOpen}
            onClose={onClose}
        >
            {accessibleAccounts.map(account => {
                const isActiveAccount = account.id === activeAccount.id;

                return (
                    <MenuItem key={"menu-account-" + account.id} className={classes.menuItem}>
                        <AccountItem
                            account={account}
                            isActiveAccount={isActiveAccount}
                            icon={
                                <IconButton
                                    onClick={() => onAccountClick(account.id)}
                                >
                                    <LockIcon />
                                </IconButton>
                            }
                        />
                    </MenuItem>
                );
            })}
        </Menu>
    );
};

export { NavBarMenu };
