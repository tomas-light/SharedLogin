import * as React from "react";

import { makeStyles, Theme } from "@material-ui/core/styles";
import IconButton from "@material-ui/core/IconButton";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import LockIcon from "@material-ui/icons/LockOpenOutlined";

import { AccountDTO } from "@models/accounts/AccountDTO";
import { AccountItem } from "@app/Layout/NavBar/AccountItem/AccountItem";

const useStyles = makeStyles((theme: Theme) => ({
    root: {}
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
            id="menu-appbar"
            anchorEl={anchorEl}
            anchorOrigin={{
                vertical: "top",
                horizontal: "right"
            }}
            transformOrigin={{
                vertical: "top",
                horizontal: "right"
            }}
            open={isOpen}
            onClose={onClose}
        >
            {accessibleAccounts.map(account => {
                const isActiveAccount = account.id === activeAccount.id;

                return (
                    <MenuItem>
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
