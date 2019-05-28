import * as React from "react";

import { Callback } from "@utils/types/Callback";

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

class State {}

export class NavBar extends React.Component<Props, State> {
    render() {
        const {
            redirectToLogin,
            redirectToBugList,
            redirectToUserList,
            redirectToNewBug,
            redirectToNewUser,
            logout
        } = this.props;

        return (
            <nav className="navbar navbar-inverse navbar-fixed-top">
                <div className="container">
                    <div className="navbar-header">
                        <button
                            type="button"
                            className="navbar-toggle"
                            data-toggle="collapse"
                            data-target=".navbar-collapse"
                        >
                            <span className="sr-only">Toggle navigation</span>
                            <span className="icon-bar" />
                            <span className="icon-bar" />
                            <span className="icon-bar" />
                        </button>
                    </div>
                    <div className="navbar-collapse collapse">
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav">
                                <li>
                                    <button
                                        className="btn btn-default navbar-btn"
                                        onClick={redirectToLogin}
                                    >
                                        Login
                                    </button>
                                </li>
                                <li>
                                    <button
                                        className="btn btn-default navbar-btn"
                                        onClick={redirectToBugList}
                                    >
                                        Backlog
                                    </button>
                                </li>
                                <li>
                                    <button
                                        className="btn btn-default navbar-btn"
                                        onClick={redirectToNewBug}
                                    >
                                        New bug
                                    </button>
                                </li>
                                <li>
                                    <button
                                        className="btn btn-default navbar-btn"
                                        onClick={redirectToUserList}
                                    >
                                        Users
                                    </button>
                                </li>
                                <li>
                                    <button
                                        className="btn btn-default navbar-btn"
                                        onClick={redirectToNewUser}
                                    >
                                        New user
                                    </button>
                                </li>
                                {/*<li>
                                    <button className="btn btn-default navbar-btn" onClick={logout}>
                                        Sign out
                                    </button>
                                </li>*/}
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
        );
    }
}
